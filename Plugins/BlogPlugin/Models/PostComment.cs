using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPlugin.Models
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا نام خود را وارد نمایید")]
        public string Author { get; set; }

        [Required(ErrorMessage = "لطفا ایمیل خود را وارد نمایید"), DataType(DataType.EmailAddress, ErrorMessage = "لطفا ایمیل خود را درست وارد نمایید")]
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey("PostID")]
        public BlogPost Post { get; set; }

        public int? PostID { get; set; }

        [Required(ErrorMessage = "لطفا نظر خود را وارد نمایید")]
        public string Body { get; set; }

        public bool IsPublished { get; set; }

        [ForeignKey("ParentId")]
        public PostComment Parent { get; set; }

        public int? ParentId { get; set; }
        public ICollection<PostComment> Childeren { get; set; }
    }
}