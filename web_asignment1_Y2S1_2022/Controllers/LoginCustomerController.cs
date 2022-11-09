using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using web_asignment1_Y2S1_2022.Models;
using System.Text.Json;
using System.Web; 

//This controller is done by Li Zhe Yun

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class LoginCustomerController : Controller
    {
        public LoginCustomerController() {  }

        //All roles are able to access the login page DO NOT REDIRECT TO THE Forbidden.cshtml page from here!!!
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                HttpContext.Session.SetString("GenericRole", "Customer"); 
                return View();
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home"); 
                }
                else if(HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager"); 
                }
                else if(HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager"); 
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Home"); 
                }
                return null; 
            }
        }

        [HttpPost]
        public ActionResult ChangeUserRole()
        {
            if (HttpContext.Session.GetString("GenericRole").Equals("Customer"))
            {
                HttpContext.Session.SetString("GenericRole", "Staff");
            }
            else if (HttpContext.Session.GetString("GenericRole").Equals("Staff"))
            {
                HttpContext.Session.SetString("GenericRole", "Customer"); 
            }
            return RedirectToAction("Login", "LoginCustomer"); 
        }

        public IActionResult VerificationCodeRetrievalPortal()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //This portal can only be accessed when the user has not signed into the portal 
                return View(); 
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Market Manager");
                }
                return null;
            }
        }

        public IActionResult VerificationCodeRetrievalPortalInterface()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                if (HttpContext.Session.GetString("SendOver") != null)
                {
                    List<DataStructureRESTDBRecoveryEmail> a = JsonSerializer.Deserialize<List<DataStructureRESTDBRecoveryEmail>>(API.GetAll());
                    foreach (DataStructureRESTDBRecoveryEmail email in a)
                    {
                        if (email.TemporaryAccessCode.Equals(HttpContext.Session.GetString("SendOver")))
                        {
                            ViewBag.VerificationCode =  email.VerificationCode;   
                        }
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("VerificationCodeRetrievalPortalInterface", "LoginCustomer");
                }
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Market Manager");
                }
                return null;
            }
        }

        //Check if the user has went past
        public IActionResult LoginVerification()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                if (HttpContext.Session.GetString("TempEmailAddressHold") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
                }
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Market Manager");
                }
                return null; 
            }
        }

        public IActionResult SetNewPassword()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                if (HttpContext.Session.GetString("TempEmailAddressHold") != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
                }
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Home");
                }
                return null; 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePasswordAfterVerificationCodeAuthentication(IFormCollection formData)
        {
            ActionResult result = null; 
            string newPassword = formData["NewPassword"].ToString();
            string confirmNewPassword = formData["ConfirmNewPassword"].ToString();
            if (newPassword.Length > 0 && confirmNewPassword.Length > 0)
            {
                if (newPassword.Equals(confirmNewPassword))
                {
                    //Because the user haven't signed in, we cannot use new LoginCustomerController().GetCustomerByEmailAddress()
                    string emailAddr = HttpContext.Session.GetString("TempEmailAddressHold"); //a copy of the email address is still retained here
                    Customer c = new Customer
                    {
                        MemberID = GetCustomerByEmailAddress(emailAddr).MemberID,
                        MName = GetCustomerByEmailAddress(emailAddr).MName,
                        MAddress = GetCustomerByEmailAddress(emailAddr).MAddress, 
                        MBirthDate = GetCustomerByEmailAddress(emailAddr).MBirthDate, 
                        MCountry = GetCustomerByEmailAddress(emailAddr).MCountry, 
                        MEmailAddr = GetCustomerByEmailAddress(emailAddr).MEmailAddr, 
                        MGender = GetCustomerByEmailAddress(emailAddr).MGender,   
                        MTelNo = GetCustomerByEmailAddress(emailAddr).MTelNo, 
                        Password = newPassword 
                    }; 
                    new CustomerController().UpdatePersonalParticulars(c);   
                    result = RedirectToAction("Login", "LoginCustomer"); 
                }
                else
                {
                    TempData["ErrorMsg"] = "These password do not match";
                    result = RedirectToAction("SetNewPassword", "LoginCustomer"); 
                }
            }
            else
            {
                TempData["ErrorMsg"] = "Please enter a password in both fields";
                result = RedirectToAction("SetNewPassword", "LoginCustomer"); 
            }
            return result; 
        }

        public IActionResult LoginVerificationPromptEmail()
        {
            //The user can just simply reset his or her password in the settings page..
            if (HttpContext.Session.GetString("Role") == null)
            {
                return View();
            }
            else
            {
                //Go back to the home page based on the current role of the user... 
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Go to the Customer index page
                    return RedirectToAction("Index", "Home");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Product Manager"))
                {
                    //Go to the marketing manager index page
                    return RedirectToAction("Index", "ProductManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Sales Personnel"))
                {
                    //Go to the sales personnel index page
                    return RedirectToAction("Index", "SalesManager");
                }
                else if (HttpContext.Session.GetString("Role").Equals("Marketing Personnel"))
                {
                    //Go to the marketing personnel index page
                    return RedirectToAction("Index", "Market Manager");
                }
                return null; 
            }
        } 

        //This method will bee able to process the information required for 
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult SubmitVerificationCode(IFormCollection emailAddressField)
        {
            ActionResult result = null; 
            string box1 = emailAddressField["Box0"].ToString();
            string box2 = emailAddressField["Box1"].ToString();
            string box3 = emailAddressField["Box2"].ToString();
            string box4 = emailAddressField["Box3"].ToString();
            string box5 = emailAddressField["Box4"].ToString();
            string box6 = emailAddressField["Box5"].ToString(); 
            string final = box1 + box2 + box3 + box4 + box5 + box6;
            List<DataStructureRESTDBRecoveryEmail> a = JsonSerializer.Deserialize<List<DataStructureRESTDBRecoveryEmail>>(API.GetAll());
            List<TemporaryAccessCodeForVerificationCode> b = JsonSerializer.Deserialize<List<TemporaryAccessCodeForVerificationCode>>(API.GetAllTemporaryAccessCodeRecords());
            foreach (DataStructureRESTDBRecoveryEmail entry in a)
            {
                if (entry.VerificationCode.Equals(final))
                {
                    //Check if its the corresponding email address, or else it will return an error message back to the user
                    if (entry.EmailAddr.Equals(HttpContext.Session.GetString("TempEmailAddressHold")))
                    {
                        //Delete the record from the APIs when the user has successfully input the verification code
                        string id1 = entry._id; 
                        API.Delete(id1); //Delete away the verification code
                        foreach (TemporaryAccessCodeForVerificationCode record in b)
                        {
                            if (record.EmailAddr.Equals(HttpContext.Session.GetString("TempEmailAddressHold")))
                            {
                                string id2 = record._id;
                                API.DeleteTemporaryAccessCodeRecord(id2); 
                            }
                        }
                        TempData["VerificationSucceeded"] = "Verification successful!"; 
                        result = RedirectToAction("SetNewPassword", "LoginCustomer"); 
                    }
                    else //not the verification code for that particular email address
                    {
                        TempData["VerificationFailed"] = "Verification has failed as the specified email address was not found"; 
                        result = RedirectToAction("LoginVerification", "LoginCustomer"); 
                    }
                }
                else //No matching verification code is found within the database.
                {
                    TempData["VerificationFailed"] = "Verification has failed as the verification code is invalid"; 
                    result = RedirectToAction("LoginVerification", "LoginCustomer"); 
                }
            }
            return result; 
        }

        //Delete the verification code every 60 seconds if unused by the user
        public void CheckIfUnused()
        {

        }
        
        //SendVerificationCodeToEmailAddr
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult SendVerificationCodeToEmailAddr(IFormCollection emailAddressField)
        {
            ActionResult result = null; 
            string emailAddress = emailAddressField["RecoveryEmailAddress"].ToString();
            if (emailAddress.Length != 0)
            {
                if (new LoginCustomerController().GetCustomerByEmailAddress(emailAddress) != null)
                {
                    if (emailAddress.Contains("@") && emailAddress.Contains("."))
                    {
                        string verificationCode = String.Empty;
                        List<DataStructureRESTDBRecoveryEmail> a = JsonSerializer.Deserialize<List<DataStructureRESTDBRecoveryEmail>>(API.GetAll());
                        foreach (DataStructureRESTDBRecoveryEmail entry in a)
                        {
                            if (entry.EmailAddr.Equals(emailAddress))
                            {
                                TempData["Invalid"] = "You have just recently requested for a verification code. Please wait for 60 seconds before requesting again";
                                return RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
                            }
                        }
                        for (; ; )
                        {
                        label:
                            verificationCode = new Random().Next(0, 999999).ToString().Length == 5
                                                    ? "0" + new Random().Next(0, 999999).ToString()
                                                    : new Random().Next(0, 999999).ToString().Length == 4
                                                        ? "00" + new Random().Next(0, 999999).ToString()
                                                        : new Random().Next(0, 999999).ToString().Length == 3
                                                            ? "000" + new Random().Next(0, 999999).ToString()
                                                            : new Random().Next(0, 999999).ToString().Length == 2
                                                                ? "0000" + new Random().Next(0, 999999).ToString()
                                                                : new Random().Next(0, 999999).ToString().Length == 1
                                                                    ? "00000" + new Random().Next(0, 999999).ToString()
                                                                    : new Random().Next(0, 999999).ToString();
                            foreach (DataStructureRESTDBRecoveryEmail e in a)
                            {
                                if (e.VerificationCode == verificationCode)
                                {
                                    goto label;
                                }
                                else
                                {
                                    break; //break from the foreach
                                }
                            }
                            break; //break from for(;;) 
                        }

                        //Generate another temporary access code... 
                        string temporaryAccessCode = string.Empty;
                        List<string> STRING_POOL = new List<string> { "ABCDEFGHIJKLMNOPQRSTUVEXYZ", "abcdefghijklmnopqrstuvwxyz", "0123456789" };
                        List<TemporaryAccessCodeForVerificationCode> b = JsonSerializer.Deserialize<List<TemporaryAccessCodeForVerificationCode>>(API.GetAllTemporaryAccessCodeRecords());
                        for (; ; )
                        {
                        labelB:
                            //Generate the temporary access code
                            do
                            {
                                temporaryAccessCode += STRING_POOL[new Random().Next(2)][new Random().Next(STRING_POOL[new Random().Next(2)].Length - 1)];
                            }
                            while (temporaryAccessCode.Length < 4);
                            foreach (TemporaryAccessCodeForVerificationCode t in b)
                            {
                                if (t.TemporaryAccessCode == temporaryAccessCode)
                                {
                                    goto labelB;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        }

                        string dataStructure = "{" +
                                               $"\"VerificationCode\": \"{verificationCode}\"," +
                                               $"\"GenerationDateTime\": \"{DateTime.Now.ToString()}\"," +
                                               $"\"EmailAddr\": \"{emailAddress}\"," +
                                               $"\"TemporaryAccessCode\": \"{temporaryAccessCode}\"" +
                                                "}";
                        string dataStructure2 = "{" +
                                               $"\"EmailAddr\": \"{emailAddress}\"," +
                                               $"\"TemporaryAccessCode\": \"{temporaryAccessCode}\"" +
                                                "}";
                        API.Post(dataStructure);
                        API.PostTemporaryAccessCodeRecord(dataStructure2);
                        System.Threading.Thread.Sleep(5000);
                        HttpContext.Session.SetString("TempEmailAddressHold", emailAddress);
                        HttpContext.Session.SetString("TempAccessCode", temporaryAccessCode);
                        result = RedirectToAction("LoginVerification", "LoginCustomer");
                    }
                    else
                    {
                        TempData["Invalid"] = "Input email address is not in the correct format";
                        result = RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
                    }
                }
                else
                {
                    TempData["Invalid"] = "Email address was not found in the database";
                    result = RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
                }
            } 
            else
            {
                TempData["Invalid"] = "Please enter a email address";
                result = RedirectToAction("LoginVerificationPromptEmail", "LoginCustomer");
            }
            return result; 
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult SignIntoVerificationCodeRetrievalPortal(IFormCollection formData)
        {
            ActionResult result = null; 
            List<TemporaryAccessCodeForVerificationCode> b = JsonSerializer.Deserialize<List<TemporaryAccessCodeForVerificationCode>>(API.GetAllTemporaryAccessCodeRecords());
            string emailAddress = formData["EmailAddr"].ToString();
            string tempAccessCode = formData["TempAccessCode"].ToString();
            string memberID = formData["MemberID"].ToString();
            if (emailAddress.Length != 0 || tempAccessCode.Length != 0 || memberID.Length != 0)
            {
                foreach (TemporaryAccessCodeForVerificationCode code in b)
                {
                    if (code.TemporaryAccessCode.Equals(tempAccessCode) && code.EmailAddr.Equals(emailAddress))
                    {
                        if (new LoginCustomerController().GetCustomerById(memberID) != null)
                        {
                            if (new LoginCustomerController().GetCustomerById(memberID).MEmailAddr.Equals(emailAddress))
                            {
                                HttpContext.Session.SetString("SendOver", tempAccessCode);
                                return RedirectToAction("VerificationCodeRetrievalPortalInterface", "LoginCustomer");
                            }
                            else
                            {
                                TempData["Fail"] = "Incorrect MemberID / EmailAddr / TemporaryAccessCode (ERR 1)";
                                return RedirectToAction("VerificationCodeRetrievalPortal", "LoginCustomer");
                            }
                        }
                        else
                        {
                            TempData["Fail"] = "Incorrect MemberID / EmailAddr / TemporaryAccessCode (ERR 2)";
                            return RedirectToAction("VerificationCodeRetrievalPortal", "LoginCustomer");
                        }
                    }
                }
                TempData["Fail"] = "Incorrect MemberID / EmailAddr / TemporaryAccessCode (ERR 3)";
                result = RedirectToAction("VerificationCodeRetrievalPortal", "LoginCustomer");
            }
            else
            {
                TempData["Fail"] = "Certain fields are left blank in this document";
                result =  RedirectToAction("VerificationCodeRetrievalPortal", "LoginCustomer");
            }
            return result; 
        }

        private void OpenConnection() { SQL.conn.Open(); }

        private void CloseConnection() { SQL.conn.Close(); }

        public List<Staff> GetStaffCredentials()
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Staff";
            OpenConnection(); 
            SqlDataReader reader = command.ExecuteReader();
            List<Staff> final = new List<Staff>();
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        final.Add(
                            new Staff
                            {
                                StaffID = reader.GetString(0),
                                StoreID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                SName = reader.GetString(2),
                                SGender = reader.GetString(3),
                                SAppt = reader.GetString(4),
                                STelNo = reader.GetString(5),
                                SEmailAddr = reader.GetString(6),
                                SPassword = reader.GetString(7)
                            }
                        );
                    }
                }
            }
            reader.Close(); 
            CloseConnection(); 
            return final; 
        }

        public List<Customer> GetCustomerCredentials()
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Customer";
            OpenConnection(); 
            SqlDataReader reader = command.ExecuteReader();
            List<Customer> final = new List<Customer>();
            if (reader != null)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //0, 1, 2, 3, 5, 8
                        final.Add(
                            new Customer
                            {
                            //SAppt property stores the role of the user
                                MemberID = reader.GetString(0),
                                MName = reader.GetString(1),
                                MGender = reader.GetString(2),
                                MBirthDate = reader.GetDateTime(3),
                                MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                MCountry = reader.GetString(5),
                                MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Password = reader.GetString(8)
                            }
                        );
                    }
                } 
            }
            reader.Close(); 
            CloseConnection(); 
            return final; 
        }

        public Customer GetCustomer(string memberID, string password)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Customer WHERE MemberID='{memberID}' AND MPassword='{password}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Customer customer = null; 
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        //SAppt property stores the role of the user
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2),
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Password = reader.GetString(8)
                    }; 
                }
            }
            reader.Close(); 
            CloseConnection();
            return customer; 
        } 

        public Staff GetStaff(string staffID, string password)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Staff WHERE StaffID='{staffID}' AND SPassword='{password}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader(); 
            Staff staff = null; 
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    staff = new Staff
                    {
                        StaffID = reader.GetString(0),
                        StoreID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SName = reader.GetString(2),
                        SGender = reader.GetString(3),
                        SAppt = reader.GetString(4),
                        STelNo = reader.GetString(5),
                        SEmailAddr = reader.GetString(6),
                        SPassword = reader.GetString(7)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return staff; 
        }

        public Staff GetStaffById(string id)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Staff WHERE StaffID='{id}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Staff staff = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    staff = new Staff
                    {
                        StaffID = reader.GetString(0),
                        StoreID = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        SName = reader.GetString(2),
                        SGender = reader.GetString(3),
                        SAppt = reader.GetString(4),
                        STelNo = reader.GetString(5),
                        SEmailAddr = reader.GetString(6),
                        SPassword = reader.GetString(7)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return staff;
        }

        public Customer GetCustomerById(string id)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Customer WHERE MemberID='{id}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Customer customer = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        //SAppt property stores the role of the user
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2),
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Password = reader.GetString(8)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return customer;
        }

        public Customer GetCustomerByEmailAddress(string updatedEmailAddress)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Customer WHERE MEmailAddr='{updatedEmailAddress}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Customer customer = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        //SAppt property stores the role of the user
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2),
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Password = reader.GetString(8)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return customer; 
        }

        public Customer GetCustomerByTelNo(string telNo)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Customer WHERE MTelNo='{telNo}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Customer customer = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        //SAppt property stores the role of the user
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2),
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Password = reader.GetString(8)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return customer;
        }

        public Customer GetCustomerByResidentialAddr(string residentialAddr)
        {
            //establish a connection to the SQL Server Database
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Customer WHERE MAddress='{residentialAddr}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Customer customer = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        //SAppt property stores the role of the user
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2),
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Password = reader.GetString(8)
                    };
                }
            }
            reader.Close();
            CloseConnection();
            return customer;
        }

        //This method will be responsible for signing the user out 
        public void SignOut()
        {
            HttpContext.Session.Clear();  
        }

        //Create another method that validates the form LOL 
        //We need a method to take in the data from the form that has been created in Login.cshtml 
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Verification(IFormCollection loginFieldData)
        {
            //Read from the input fields
            string userIDField = loginFieldData["custMemberID"].ToString(); 
            string passwordField = loginFieldData["custPassword"].ToString();

            ActionResult result = null;

            //Test for whether both the emailAddrField and the passwordField is null or contains an empty string
            if (userIDField.Length > 0 && passwordField.Length > 0)
            {
                if (GetCustomerCredentials().Count > 0 && GetStaffCredentials().Count > 0) //if GetCustomerCredentials() do not return anything it means that the code cannot communicate with the database
                {
                    if (GetCustomer(userIDField, passwordField) != null)
                    {
                        //Prepare the data to be sent back to the view
                        HttpContext.Session.SetString("Role", "Customer");
                        HttpContext.Session.SetString("Name", GetCustomer(userIDField, passwordField).MName);
                        HttpContext.Session.SetString("ID", userIDField); 
                        HttpContext.Session.SetString("Email", GetCustomer(userIDField, passwordField).MEmailAddr);  
                        //c.MBirthDate is stored as a DateTime object MM/DD/YYYY (CORRECT DATETIME FORMAT)
                        if (DateTime.Now.Month.Equals(GetCustomer(userIDField, passwordField).MBirthDate.Month))
                        {
                            HttpContext.Session.SetString("isOpened", 1.ToString()); //send an object 
                            result = RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            result = RedirectToAction("Index", "Home"); //do not send
                        }
                    }
                    else
                    {
                        if (GetStaff(userIDField, passwordField) != null)
                        {
                            //Go on and check the role of the staff by accessing the 'MAppt' property of each Staff object
                            if (GetStaff(userIDField, passwordField).SAppt.Equals("Sales Personnel")) //Sales Personnel
                            {
                                HttpContext.Session.SetString("Role", "Sales Personnel");
                                HttpContext.Session.SetString("Name", GetStaff(userIDField, passwordField).SName);
                                HttpContext.Session.SetString("ID", userIDField);
                                result = RedirectToAction("Index", "Home"); //Redirect to the SalesPersonnel
                            }
                            else if(GetStaff(userIDField, passwordField).SAppt.Equals("Product Manager")) //Product Manager
                            {
                                HttpContext.Session.SetString("Role", "Product Manager");
                                HttpContext.Session.SetString("Name", GetStaff(userIDField, passwordField).SName);
                                HttpContext.Session.SetString("ID", userIDField);
                                result = RedirectToAction("Index", "ProductManager");
                            }
                            else if(GetStaff(userIDField, passwordField).SAppt.Equals("Marketing Personnel")) //Marketing Personnel
                            {
                                HttpContext.Session.SetString("Role", "Marketing Manager");
                                HttpContext.Session.SetString("Name", GetStaff(userIDField, passwordField).SName);
                                HttpContext.Session.SetString("ID", userIDField);
                                result = RedirectToAction("Index", "MarketManager");
                            }
                        }
                        //either the username or the password was incorrect
                        //Sign in process should fail
                        if (result is null)
                        {
                            TempData["Login Failed"] = "Username / Password is invalid";
                            result = RedirectToAction("Login", "LoginCustomer");
                        }
                    }
                } 
                else
                {
                    //Sign in process should fail
                    TempData["Login Failed"] = "Failed to communicate with the database server"; 
                    result = RedirectToAction("Login", "LoginCustomer");
                }
            }
            else
            {
                //If the user has not input anything within either the username or the password field
                //Sign in process should fail 
                TempData["Login Failed"] = "Please enter your username and/or password"; 
                result = RedirectToAction("Login", "LoginCustomer"); 
            }
            return result; 
        }

        //Reset the god damn password when the user uses the in-account password changing utility
        [HttpPost]
        [AutoValidateAntiforgeryToken]  
        public ActionResult VerifyEmailAndSetPassword(IFormCollection formData)
        {
            ActionResult result = null; 
            string recoveryEmailAddress = formData["EmailAddress"].ToString();
            string oldPassword = formData["AccountOldPassword"].ToString();
            string newPassword = formData["AccountNewPassword"].ToString();
            string confirmNewPassword = formData["ConfirmNewPassword"].ToString(); 
            if (GetCustomerByEmailAddress(recoveryEmailAddress) != null)
            {
                if ((!oldPassword.Equals(string.Empty) && !newPassword.Equals(string.Empty)) && !confirmNewPassword.Equals(string.Empty))
                {
                    //Must confirm that the password that has been input by the user for the 'oldPassword' matches with the password that is tied to the specified user
                    if (GetCustomerByEmailAddress(recoveryEmailAddress).Password.Equals(oldPassword))
                    {
                        if (confirmNewPassword.Equals(newPassword))
                        {
                            Customer c = new Customer
                            {
                                MemberID = GetCustomerByEmailAddress(recoveryEmailAddress).MemberID,
                                MAddress = GetCustomerByEmailAddress(recoveryEmailAddress).MAddress,
                                MEmailAddr = GetCustomerByEmailAddress(recoveryEmailAddress).MEmailAddr,
                                MBirthDate = GetCustomerByEmailAddress(recoveryEmailAddress).MBirthDate,
                                MCountry = GetCustomerByEmailAddress(recoveryEmailAddress).MCountry,
                                MGender = GetCustomerByEmailAddress(recoveryEmailAddress).MGender,
                                MName = GetCustomerByEmailAddress(recoveryEmailAddress).MName,
                                MTelNo = GetCustomerByEmailAddress(recoveryEmailAddress).MTelNo,
                                Password = newPassword
                            };
                            new CustomerController().UpdatePersonalParticulars(c);
                            System.Threading.Thread.Sleep(5000);
                            //Once the system has successfully updated the database, it should redirect the user back to the login page which prompts for the user to try and sign in again
                            HttpContext.Session.SetString("Successful", "Successfully updated your password");
                            result = RedirectToAction("LoginVerificationOldNewPassword", "Customer");
                        }
                        else
                        {
                            HttpContext.Session.SetString("ResetError", "The passwords that you have entered under the confirm new password field and the enter new password fields do not match");
                            result = RedirectToAction("LoginVerificationOldNewPassword", "Customer");
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("ResetError", "The previous password that you have used for your account was not correct. Please try again");
                        result = RedirectToAction("LoginVerificationOldNewPassword", "Customer"); 
                    }
                } 
                else
                {
                    HttpContext.Session.SetString("ResetError", "Please enter a password and / or email account in the correct field(s)");
                    result = RedirectToAction("LoginVerificationOldNewPassword", "Customer");
                }
            }
            else
            {
                TempData["ResetError"] = "The specified email address was not found";
                result = RedirectToAction("LoginVerificationOldNewPassword", "Customer");
            }
            return result; 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acknowledgement()
        {
            HttpContext.Session.Remove("ResetError");
            HttpContext.Session.Remove("Successful"); 
            //Get the current URL of the user that he or she is currently on, then redirect to that page accordingly
            return RedirectToAction("Profile", "Customer"); 
        }
    }
}