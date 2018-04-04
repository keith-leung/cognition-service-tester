using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CognitionTesterWeb.Models
{
    public class TextClassificationResultDto
    {
        public string Id { get; set; }

        [Display(Name = "文章标题")]
        public string Title { get; set; }

        [Display(Name = "A厂商结果")]
        public string AResult { get; set; }
        [Display(Name = "B厂商结果")]
        public string BResult { get; set; }
        [Display(Name = "C厂商结果")]
        public string CResult { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "数据生成时间")]
        public DateTime Ctime { get; set; }
    }
}
