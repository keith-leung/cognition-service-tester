using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CognitionTesterWeb.Models
{
    public class TextClassificationResult
    {
        public ObjectId Id { get; set; }
        [Display(Name = "文章标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "数据生成时间")]
        public DateTime Ctime { get; set; }

        public List<TextClassificationResultItem> ResultItems { get; set; }
    }
}
