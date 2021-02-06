using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Data
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }        

        [Required]
        [ForeignKey(nameof(Comment))]
        public int CommentId { get; set; }


        [Required]
        public Guid Author { get; set; }


        [Required]
        public string Text { get; set; }


        public virtual Comment Comment { get; set; }                     
        

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
