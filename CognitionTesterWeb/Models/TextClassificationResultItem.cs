using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitionTesterWeb.Models
{
    public class TextClassificationResultItem
    {
        public string CognitionServiceVendor { get; set; }
        public string VendorLabel { get; set; }
        public string VendorResult { get; set; }
    }
}
