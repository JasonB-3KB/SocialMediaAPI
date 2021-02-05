using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class CommentCreate
    {
        [Required]
        public int Id { get; set; }
        [Required]        
        [MaxLength(8000)]
        public string Text { get; set; }
    }
}
