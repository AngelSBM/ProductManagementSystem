using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Shared.DTOs.Report
{
    public class CreateCustomerItemRangeReportDto
    {
        public int ItemNumberFrom { get; set; }
        public int ItemNumberTo { get; set; }

    }
}
