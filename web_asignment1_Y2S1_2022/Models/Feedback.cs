using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace web_asignment1_Y2S1_2022.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        [StringLength(9, ErrorMessage = "Member ID cannot be longer than 9 characer!")]
        public string MemberID { get; set; }
        [Display(Name = "Post Date and Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateTimePosted { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters!")]
        public string Title { get; set; }
        public string Text { get; set; }
        [Display(Name = "Image")]
        public string ImageFileName { get; set; }

    }
}
