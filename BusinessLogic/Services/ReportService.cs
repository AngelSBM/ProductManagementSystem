using AutoMapper;
using DataAccess.UnitOfWork;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
using ProductManagementSystem.Shared.DTOs.Report;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuestPDF.Helpers.Colors;

namespace BusinessLogic.Services
{
    public interface IReportService
    {
        Task<byte[]> GenerateRangeItemsReportAsync(CreateCustomerItemRangeReportDto range);
        Task<byte[]> GenerateExpensiveItemsReport();
    }

    public class ReportService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReportService
    {
        public async Task<byte[]> GenerateExpensiveItemsReport()
        {
            var mostExpensiveItemsPerCustomerList = new List<CustomerExpensiveItemsDto>();
            var customersitems = await _unitOfWork.customerItemRepository.GetCustomerItemsAsync();
            

            var groupedCustomerItems = customersitems.GroupBy(x => x.Customer).ToList();

            foreach (var itemsGroupedByCustomer in groupedCustomerItems)
            {
                var customerExpensiveItemsDto = new CustomerExpensiveItemsDto();

                var mostExpensivePrice = itemsGroupedByCustomer.Max(x => x.Item!.Price);
                var mostExpensiveItems = itemsGroupedByCustomer.Where(x => x.Item!.Price == mostExpensivePrice).Select(x => x.Item);

                var customerDto = _mapper.Map<CustomerDto>(itemsGroupedByCustomer.Key);
                var mostExpensiveItemsDtos = _mapper.Map<IEnumerable<ItemDto>>(mostExpensiveItems);

                customerExpensiveItemsDto.Customer = customerDto;
                customerExpensiveItemsDto.MostExpensiveItems.AddRange(mostExpensiveItemsDtos);

                mostExpensiveItemsPerCustomerList.Add(customerExpensiveItemsDto);

            }
            return await GenerateExpensiveItemsPerCustomerPdf(mostExpensiveItemsPerCustomerList);

        }


        public async Task<byte[]> GenerateRangeItemsReportAsync(CreateCustomerItemRangeReportDto range)
        {

            var itemsRange = new List<CustomerItemRangeItemDto>();

            if (range.ItemNumberFrom > range.ItemNumberTo)
                throw new InvalidOperationException();

            var customerItemRangeCollection = await _unitOfWork.customerItemRepository.GetItemsWithinRangeAsync(range.ItemNumberFrom, range.ItemNumberTo);

            foreach (var customerItemRangeUnit in customerItemRangeCollection)
            {
                var customerItemRangeItemDto = new CustomerItemRangeItemDto();
                customerItemRangeItemDto.Item = _mapper.Map<ItemDto>(customerItemRangeUnit.Item);
                customerItemRangeItemDto.Customer = _mapper.Map<CustomerDto>(customerItemRangeUnit.Customer);

                itemsRange.Add(customerItemRangeItemDto);
            }
                    
            return await GenerateItemRangePdfAsync(range, itemsRange);

        }


        private async Task<byte[]> GenerateItemRangePdfAsync(CreateCustomerItemRangeReportDto range, List<CustomerItemRangeItemDto> items)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().ShowOnce().Row(row =>
                    {

                        var imageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "vslogo.jpeg");
                        byte[] imageData = System.IO.File.ReadAllBytes(imageUrl);

                        row.ConstantItem(60).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Product Management").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("San Juan de la Maguana").FontSize(9);
                            col.Item().AlignCenter().Text("829 574 7432").FontSize(9);
                            col.Item().AlignCenter().Text("pms@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {

                            col.Item().BorderColor("#257272").AlignCenter().Text($"Date: {DateTime.Now.ToString().Substring(0, 10)}");
                            col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text($"Items #{range.ItemNumberFrom} - #{range.ItemNumberTo}").FontColor("#fff");
                            
                        });


                    });

                    page.Content().PaddingVertical(10).Column(firstCol =>
                    {
                        var itemsNumber = items.GroupBy(x => x.Item.Number).Count();
                        var customersNumber = items.GroupBy(x => x.Customer.Id).Count();

                        firstCol.Item().Text("Info:").Underline().Bold();
                        
                        firstCol.Item().Text(txt =>
                        {
                            txt.Span("Qty items: ").SemiBold().FontSize(10);
                            txt.Span(itemsNumber.ToString()).FontSize(10);
                        });

                        firstCol.Item().Text(txt =>
                        {
                            txt.Span("Qty customers: ").SemiBold().FontSize(10);
                            txt.Span(customersNumber.ToString()).FontSize(10);
                        });


                        firstCol.Item().Padding(10).LineHorizontal(0.5f);

                        firstCol.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Item name").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Item Price").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Item Category").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Customer").FontColor("#fff");

                            });

                            var groupedItems = items.GroupBy(item => item.Item.Name);

                            foreach (var group in groupedItems)
                            {
                                var first = group.First();
                                var count = group.Count();

                                table.Cell()
                                    .RowSpan((uint)count)                                    
                                    .BorderBottom(0.5f)
                                    .BorderColor("#D9D9D9")
                                    .Padding(2)
                                    .Text(first.Item.Name)
                                    .FontSize(10);

                                table.Cell()
                                    .RowSpan((uint)count)
                                    .BorderBottom(0.5f)
                                    .BorderColor("#D9D9D9")
                                    .Padding(2)
                                    .Text(first.Item.Price)
                                    .FontSize(10);

                                table.Cell()
                                    .RowSpan((uint)count)
                                    .BorderBottom(0.5f)
                                    .BorderColor("#D9D9D9")
                                    .Padding(2)
                                    .Text(first.Item.Category.Name)
                                    .FontSize(10);


                                foreach (var item in group)
                                {
                                    table.Cell()
                                    .BorderBottom(0.5f)
                                    .BorderColor("#D9D9D9")
                                    .Padding(2)
                                    .Text(item.Customer.Name)
                                    .FontSize(10);
                                }
                            }


                        });

                    });

                });
            }).GeneratePdf();

            return pdf;
    }

        private async Task<byte[]> GenerateExpensiveItemsPerCustomerPdf(IEnumerable<CustomerExpensiveItemsDto> mostExpensiveItemsPerCustomer)
        {
            var pdf = Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().ShowOnce().Row(row =>
                    {
                        var imageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", "vslogo.jpeg");
                        byte[] imageData = System.IO.File.ReadAllBytes(imageUrl);

                        row.ConstantItem(150).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Product Management").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("San Juan de la Maguana").FontSize(9);
                            col.Item().AlignCenter().Text("829 574 7432").FontSize(9);
                            col.Item().AlignCenter().Text("pms@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {

                            col.Item().BorderColor("#257272").AlignCenter().Text($"Date: {DateTime.Now.ToString().Substring(0, 10)}");
                            col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text($"Most expesive items").FontColor("#fff");
                        });
                    });

                    page.Content().PaddingVertical(10).Column(firstCol =>
                    {
                        var itemsList = new List<ItemDto>();
                        foreach (var customerItems in mostExpensiveItemsPerCustomer)
                            itemsList.AddRange(customerItems.MostExpensiveItems);

                        var itemsNumber = itemsList.GroupBy(x => x.Number).Count();

                        firstCol.Item().Text("Info:").Underline().Bold();

                        firstCol.Item().Text(txt =>
                        {
                            txt.Span("Qty customers: ").SemiBold().FontSize(10);
                            txt.Span(mostExpensiveItemsPerCustomer.Count().ToString()).FontSize(10);
                        });

                        firstCol.Item().Text(txt =>
                        {
                            txt.Span("Qty items: ").SemiBold().FontSize(10);
                            txt.Span(itemsNumber.ToString()).FontSize(10);
                        });


                        firstCol.Item().Padding(10).LineHorizontal(0.5f);

                        firstCol.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
         
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Customer").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Item name").FontColor("#fff");
                                header.Cell().Background("#257272").Padding(2).Text("Item Price").FontColor("#fff");

                            });

                            foreach (var customerGroup in mostExpensiveItemsPerCustomer)
                            {
                                bool isFirstRow = true;
                                var customer = customerGroup.Customer.Name;

                                foreach (var item in customerGroup.MostExpensiveItems)
                                {
                                    if (isFirstRow)
                                    {
                                        table.Cell().RowSpan((uint)customerGroup.MostExpensiveItems.Count())
                                            .BorderBottom(0.5f)
                                            .BorderColor("#D9D9D9")
                                            .Padding(2)
                                            .Text(customer)
                                            .FontSize(10);                       
                                    }

                                    table.Cell()
                                        .BorderBottom(0.5f)
                                        .BorderColor("#D9D9D9")
                                        .Padding(2)
                                        .Text(item.Name)
                                        .FontSize(10);

                                    if (isFirstRow) 
                                    {
                                        table.Cell().RowSpan((uint)customerGroup.MostExpensiveItems.Count())
                                           .BorderBottom(0.5f)
                                           .BorderColor("#D9D9D9")
                                           .Padding(2)
                                           .Text(item.Price)
                                           .FontSize(10);
                                        isFirstRow = false;
                                    }
                                }
                            }

                        });

                    });

                });

            }).GeneratePdf();

            return pdf;
        }

    }



}
