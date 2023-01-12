using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Dtos.Charts
{
    public class PieChartDto
    {
        public string HexCode { get; set; }
        public string Label { get; set; }
        public int Count { get; set; }
    }
}
