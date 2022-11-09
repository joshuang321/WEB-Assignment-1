using System.ComponentModel.DataAnnotations;

namespace web_asignment1_Y2S1_2022.Models
{
    //Done by Li Zhe Yun
    public class Staff
    {
        [Display(Name="StaffID")]
        [StringLength(20, ErrorMessage="The StaffID must be less than 20 characters")]
        public string StaffID { get; set; }

        [Display(Name="StoreID")]
        [StringLength(25, ErrorMessage="The StoreID must be less than 25 characters")]
        public string StoreID { get; set; }

        [Display(Name="SName")]
        [StringLength(50, ErrorMessage="The SName must be less than 50 characters")]
        public string SName { get; set; }

        [Display(Name="SGender")]
        public string SGender { get; set; }

        [Display(Name="SAppt")]
        [StringLength(50, ErrorMessage="SAppt must be less than 50 characters in total")]
        public string SAppt { get; set; }

        [Display(Name="STelNo")]
        [StringLength(20, ErrorMessage="Your telephone number is too long. Please find a telephone number that has less than 20 digits and characters")]
        [Required(ErrorMessage="This field cannot be left blank!")]
        public string STelNo { get; set; }

        [Display(Name="SEmailAddr")]
        [StringLength(50, ErrorMessage="Your email address is too long, please try to find an email address that contains less than 50 characters")]
        [Required(ErrorMessage="This field cannot be left blank!")]
        public string SEmailAddr { get; set; }

        [Display(Name ="SPassword")]
        [StringLength(20, ErrorMessage = "Your password length must be between 8 and 20 (inclusive)", MinimumLength = 8)]
        [Required(ErrorMessage="This field cannot be left blank!")]
        public string SPassword { get; set; }
    }
}