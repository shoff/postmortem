using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostMortem.Web.Dtos
{
    public class UpdateCommentDto
    {
        [Required]
        public string CommentText { get; set; }
    }
}
