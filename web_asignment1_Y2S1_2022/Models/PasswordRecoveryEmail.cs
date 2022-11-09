using System; 

namespace web_asignment1_Y2S1_2022.Models
{
    public class PasswordRecoveryEmail
    {
        //Don't even need to do any data annotations not part of assignment requirement
        public string RecoveryEmail { get; set; }
    }

    public class PasswordRecoverySecondMethod
    {
        public string RecoveryEmail { get; set; }    
        public string OldPassword { get; set; } 
        public string NewPassword { get; set; }  
    }

    //This object 
    public class DataStructureRESTDBRecoveryEmail
    {
        public string _id { get; set; }
        public string VerificationCode { get; set; }
        public string EmailAddr { get; set; }
        public string TemporaryAccessCode { get; set; }
        public string GenerationDateTime { get; set; }
    }

    public class TemporaryAccessCodeForVerificationCode
    {
        public string _id { get; set; } 
        public string EmailAddr { get; set; }   
        public string TemporaryAccessCode { get; set; }  
    }
}
