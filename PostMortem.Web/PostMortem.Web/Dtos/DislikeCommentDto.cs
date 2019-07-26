using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostMortem.Web.Dtos
{
    public class DislikeCommentDto
    {
        [Required]
        [Range(1,1)]
        public int Count { get; set; } = 1;
    }
}
