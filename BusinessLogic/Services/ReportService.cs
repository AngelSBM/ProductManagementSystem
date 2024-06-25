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

namespace BusinessLogic.Services
{
    public interface IReportService
    {
        Task<byte[]> GenerateRangeItemsReportAsync(CreateCustomerItemRangeReportDto range);
        //Task<IEnumerable<>> GenerateRangeItemsReportAsync(CreateCustomerItemRangeReportDto range);
    }

    public class ReportService(IUnitOfWork _unitOfWork, IMapper _mapper) : IReportService
    {
        public async Task<byte[]> GenerateRangeItemsReportAsync(CreateCustomerItemRangeReportDto range)
        {
            try
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
                    
                return await GeneratePdfAsync(range, itemsRange);

            }
            catch (Exception ex)
            {

                throw;
            }

        }


        private async Task<byte[]> GeneratePdfAsync(CreateCustomerItemRangeReportDto range, List<CustomerItemRangeItemDto> items)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header().ShowOnce().Row(row =>
                    {
                        row.ConstantItem(140).Height(60).Placeholder();

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Produc Management Sys").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("San Juan de la Maguana").FontSize(9);
                            col.Item().AlignCenter().Text("829 574 7432").FontSize(9);
                            col.Item().AlignCenter().Text("pms@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {

                            col.Item().BorderColor("#257272").AlignCenter().Text($"Date: {DateTime.Now.ToString().Substring(0, 10)}");
                            col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text($"Items #{range.ItemNumberFrom} - #{range.ItemNumberTo}").FontColor("#fff");
                            col.Item().BorderColor("#257272").AlignCenter().Text($".................");
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

    }



}
