using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CognitionTesterWeb.Models
{
    public class TextClassificationModel
    {
        [StringLength(100)]
        [Display(Name = "标题", Prompt = "不超过100个字符")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "内容", Prompt = "必须输入内容", Description = "文章内容")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
