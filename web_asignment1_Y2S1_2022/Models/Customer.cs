using System.ComponentModel.DataAnnotations;
using System; 

namespace web_asignment1_Y2S1_2022.Models
{
    public class Customer
    {
        //MemberID: A unique identifier that each member will get upon registering themselves as members under ZZFashion 
        [Display(Name = "MemberID")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [RegularExpression("M[0-9]", ErrorMessage = "Invalid member ID. Please re-enter another memberID, thank you.")]
        [StringLength(15, ErrorMessage = "Your member ID is too long!")]
        public string MemberID { get; set; } 

        //MemberName: The name of each member registered under ZZFashion
        [Display(Name = "MemberName")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(50, ErrorMessage = "Your member name is too long!")]
        public string MName { get; set; }    

        //MemberGender: The gender of each member registered under ZZFashion
        [Display(Name = "MemberGender")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [StringLength(1, ErrorMessage = "Sorry, please specify your gender!")]
        public string MGender { get; set; }  

        //MemberBirthDate: The actual date of birth of each member registered under ZZFashion
        [Display(Name = "MemberBirthDate")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
        public DateTime MBirthDate { get; set; }   

        //MemberAddress: The actual geographical address of the member registered under ZZFashion
        [Display(Name = "MemberAddress")]
        [StringLength(50, ErrorMessage = "Address name is too long!")]
        public string MAddress { get; set; } 

        //MemberCountry: The actual country that the member registered under ZZFashion is residing in
        [Display(Name = "MemberCountry")]
        [StringLength (50, ErrorMessage="Country name is too long!")]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        public string MCountry { get; set; }     

        //Member Telephone Number: The actual telephone number that the member registered under ZZFashionn owns
        [Display(Name = "MemberTelNo")]
        [StringLength(20, ErrorMessage= "Your telephone number is too long. Please find a telephone number that has less than 20 digits and characters")]
        [DataType(DataType.PhoneNumber)]
        public string MTelNo { get; set; }   
        
        //Member Email Address: The email address of each member that has been registered under ZZFashion
        [Display(Name = "MemberEmailAddress")]
        [StringLength(50, ErrorMessage= "Your email address is too long, Please find a telephone number that has less than 20 digits and characters")]
        [DataType(DataType.EmailAddress)]
        public string MEmailAddr { get; set; }  

        //Password means password (Obviously), of each member that has been registered under ZZFashion
        [Display(Name = "MemberPassword")]
        [DataType(DataType.Password)] 
        [StringLength(20, ErrorMessage="Your password length must be between 8 and 20 (inclusive)", MinimumLength = 8)]
        [Required(ErrorMessage = "This field cannot be left blank!")]
        public string Password { get; set; }     
    }
}
