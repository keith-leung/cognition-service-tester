using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CognitionTesterWeb.Models
{
    public class TextClassificationResultDetail
    {
        public ObjectId Id { get; set; }
        [Display(Name = "文章标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "数据生成时间")]
        public DateTime Ctime { get; set; }

        [Display(Name = "A厂商结果")]
        public string AResult { get; set; }
        [Display(Name = "B厂商结果")]
        public string BResult { get; set; }
        [Display(Name = "C厂商结果")]
        public string CResult { get; set; }
    }
}
