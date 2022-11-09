using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Collections.Generic;
using web_asignment1_Y2S1_2022.Models;
using System;
using System.IO;
using MimeKit; 
 
//Done by: Li Zhe Yun 

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController() {  }

        public IActionResult LoginVerificationOldNewPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword()
        {
            return RedirectToAction("LoginVerificationOldNewPassword", "Customer"); 
        }

        //Only the customer can access the following views...Other user roles cannot access the following views...
        public IActionResult ShoppingCart()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Post the feedback into the view via a ViewBag
                    ViewBag.Feedback = GetCustomerFeedback();
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public Product GetSpecificProduct(string productImage, string productName, decimal price)
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Product WHERE ProductImage='{productImage}' AND ProductTitle='{productName}'";
            OpenConnection();
            SqlDataReader reader = command.ExecuteReader();
            Product p = null; 
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    p = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductImage = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        EffectiveDate = reader.GetDateTime(4),
                        isObsolete = "1" == reader.GetString(5) ? true : false
                    }; 
                }
            }
            CloseConnection();
            return p; 
        }

        public static List<Product> productList = new ShoppingCart().CartItems;

        //Retrieve the index of the product
        public ActionResult GetProductInfo(int id)
        {
            //Form a new product object... to be sent to the shopping cart to be displayed.. 
            //Try and get the index of the button that the user has clicked... 
            //Find the corresponding product from the database. 
            //The GetProducts() method returns a list of products, we will need to access it

            Product p = GetSpecificProduct(
                            GetProducts()[id].ProductImage,
                            GetProducts()[id].ProductName,
                            GetProducts()[id].Price
                        );

            HttpContext.Session.SetString("SelectedProductImage", p.ProductImage);
            HttpContext.Session.SetString("SelectedProductName", p.ProductName); 
            HttpContext.Session.SetString("SelectedProductPrice", p.Price.ToString());

            return RedirectToAction("Product", "Customer"); 
        }

        public ActionResult ClearSelection()
        {
            HttpContext.Session.Remove("SelectedProductImage");
            HttpContext.Session.Remove("SelectedProductName");
            HttpContext.Session.Remove("SelectedProductPrice");

            return RedirectToAction("Product", "Customer"); 
        }

        public List<Feedback> GetCustomerFeedbackByCustomerId(string id)
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Feedback WHERE MemberID='{id}'";
            List<Feedback> final = new List<Feedback> { };
            OpenConnection();
            SqlDataReader scanner = command.ExecuteReader();
            if (scanner != null)
            {
                if (scanner.HasRows)
                {
                    while (scanner.Read())
                    {
                        //Remember to check for whether the values are null
                        //Check for text and image file name 
                        final.Add(new Feedback
                        {
                            FeedbackID = scanner.GetInt32(0),
                            MemberID = scanner.GetString(1),
                            DateTimePosted = scanner.GetDateTime(2),
                            Title = scanner.GetString(3),
                            Text = scanner.IsDBNull(4) ? "" : scanner.GetString(4),
                            ImageFileName = scanner.IsDBNull(5) ? "" : scanner.GetString(5),
                        }); 
                    }
                }
            }
            CloseConnection();
            return final;
        }

        public Feedback GetCustomerFeedbackById(int id)
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Feedback WHERE FeedbackID={id}";
            Feedback final = null;
            OpenConnection();
            SqlDataReader scanner = command.ExecuteReader();
            if (scanner != null)
            {
                if (scanner.HasRows)
                {
                    while (scanner.Read())
                    {
                        //Remember to check for whether the values are null
                        //Check for text and image file name 
                        final = new Feedback
                        {
                            FeedbackID = scanner.GetInt32(0),
                            MemberID = scanner.GetString(1),
                            DateTimePosted = scanner.GetDateTime(2),
                            Title = scanner.GetString(3),
                            Text = scanner.IsDBNull(4) ? "" : scanner.GetString(4),
                            ImageFileName = scanner.IsDBNull(5) ? "" : scanner.GetString(5),
                        };
                    }
                }
            }
            CloseConnection();
            return final;
        }

        //This method is responsible for getting the responses that is associated with a particular feedback. Store it in a list.
        //List<Response> is called a thread, number of responses under a thread
        public List<Response> GetResponseByFeedbackId(int feedbackid) 
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Response WHERE FeedbackID={feedbackid}";
            List<Response> final = new List<Response> { };
            OpenConnection();
            SqlDataReader scanner = command.ExecuteReader();
            if (scanner != null)
            {
                if (scanner.HasRows)
                {
                    while (scanner.Read())
                    {
                        //Remember to check for whether the values are null
                        //Check for text and image file name 
                        final.Add(new Response
                        {
                            ResponseID = scanner.GetInt32(0),
                            FeedbackID = scanner.GetInt32(1),
                            MemberID = scanner.IsDBNull(2) ? "" : scanner.GetString(2),
                            StaffID = scanner.IsDBNull(3) ? "" : scanner.GetString(3),
                            DateTimePosted = scanner.GetDateTime(4),
                            Text = scanner.IsDBNull(5) ? "" : scanner.GetString(5),
                        });
                    }
                }
            }
            CloseConnection();
            return final;
        }

        //Retrieve the id of the feedback
        public ActionResult GetFeedbackDetails(int id)
        {
            //try and retrieve the target id from the database.... 
            HttpContext.Session.SetString("FeedbackTitle", GetCustomerFeedbackById(id).Title);
            HttpContext.Session.SetString("FeedbackText", GetCustomerFeedbackById(id).Text);
            HttpContext.Session.SetString("FeedbackImage", GetCustomerFeedbackById(id).ImageFileName);

            List<Response> result = (GetResponseByFeedbackId(id).Count > 0 ? GetResponseByFeedbackId(id) : null);

            string text = string.Empty;
            string memberids = string.Empty; 
            string staffids = string.Empty;
            string responseDates = string.Empty;

            if (result != null)
            {
                foreach (Response r in result)
                {
                    text = text + (result.IndexOf(r) == 0 ? "" : "`") + r.Text;
                    staffids = staffids + (result.IndexOf(r) == 0 ? "" : "`") + (r.StaffID == null ? "" : new LoginCustomerController().GetStaffById(r.StaffID) != null ? new LoginCustomerController().GetStaffById(r.StaffID).SName : "");
                    memberids = memberids + (result.IndexOf(r) == 0 ? "" : "`") + (r.MemberID == null ? "" : new LoginCustomerController().GetCustomerById(r.MemberID) != null ? new LoginCustomerController().GetCustomerById(r.MemberID).MName : "");
                    responseDates = responseDates + (result.IndexOf(r) == 0 ? "" : "`") + r.DateTimePosted;
                }
            } 

            HttpContext.Session.SetString("Test", text);
            HttpContext.Session.SetString("MemberIDS", memberids);
            HttpContext.Session.SetString("StaffIDS", staffids); 
            HttpContext.Session.SetString("ResponseDates", responseDates);  

            return RedirectToAction("Feedback", "Customer");  
        }

        public ActionResult CloseFeedback()
        {
            HttpContext.Session.Remove("FeedbackTitle");
            HttpContext.Session.Remove("FeedbackText");
            HttpContext.Session.Remove("FeedbackImage");

            return RedirectToAction("Feedback", "Customer");  
        }

        //This method would be responsible for the handling of the XMLHTTPRequest that can be found within _Layout.cshtml 
        public ActionResult Test()
        {
            HttpContext.Session.SetString("doNotShowAgain", "Message");
            return View(); 
        }

        public IActionResult Feedback()
        {
            List<string> emoticons = new List<string> { "\\(^Д^)/", "(≥o≤)", "(;-;)", "(T_T)", "(˚Δ˚)b", "(^_^)b", "(O_O)", "(^-^*)", "(>_<)", "(·_·)", "(-_-)", "¯\\_(ツ)_/¯", "\\(o_o)/", "(o^^)o", "(='X'=)", "(·.·)" }; 
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Post the feedback into the view via a ViewBag 
                    //A thread of responses

                    foreach (Feedback f in GetCustomerFeedbackByCustomerId(HttpContext.Session.GetString("ID")))
                    {
                        ViewBag.IsAnswered = (GetResponseByFeedbackId(f.FeedbackID).Count > 0 ? GetResponseByFeedbackId(f.FeedbackID) : null);
                    } 
                    ViewBag.Feedback = GetCustomerFeedbackByCustomerId(HttpContext.Session.GetString("ID"));
                    HttpContext.Session.SetString("Mood", emoticons[new Random().Next(emoticons.Count - 1)]); 
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult CreateFeedback()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View(); 
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        //This part will list out all the customer who are 
        public IActionResult Pending()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Deleted()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Bookmarked()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Starred()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Recents()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Profile()
        {
            Customer customer = null;
            Staff staff = null; 
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Load the profile of the user!
                    //Check whether the username exists in Staff or Customer
                    customer = new Customer
                    {
                        MName       = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MName, 
                        MAddress    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MAddress,
                        MGender     = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MGender,
                        MBirthDate  = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MBirthDate,
                        MCountry    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MCountry,
                        MEmailAddr  = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MEmailAddr, 
                        MTelNo      = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MTelNo, 
                        MemberID    = HttpContext.Session.GetString("ID"),
                        Password    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).Password,
                    }; 
                    ViewBag.Message = customer;
                    return View();
                }
                else //If the role is not a customer
                {
                    staff = new Staff
                    {
                        StaffID     = HttpContext.Session.GetString("ID"), 
                        SName       = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).SName, 
                        SAppt       = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).SAppt,
                        SEmailAddr  = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).SEmailAddr,
                        SGender     = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).SGender,
                        SPassword   = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).SPassword, 
                        STelNo      = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).STelNo, 
                        StoreID     = new LoginCustomerController().GetStaffById(HttpContext.Session.GetString("ID")).StoreID 
                    };
                    ViewBag.Message = staff; 
                    return View();
                }
            }
        }

        public IActionResult Product()
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    ViewBag.PassProducts = GetProducts();
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public IActionResult Change()
        {
            Customer customer = null;
            Staff staff = null;
            if (HttpContext.Session.GetString("Role") == null)
            {
                //Prompt the user to log in to verify that the user is a Product Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Customer"))
                {
                    //Load the profile of the user!
                    //Check whether the username exists in Staff or Customer
                    LoginCustomerController lc = new LoginCustomerController();
                    foreach (Customer c in lc.GetCustomerCredentials())
                    {
                        if (c.MemberID.Equals(HttpContext.Session.GetString("ID").ToString()))
                        {
                            customer = new Customer
                            {
                                MName = c.MName,
                                MEmailAddr = c.MEmailAddr,
                                MAddress = c.MAddress,
                                MCountry = c.MCountry,
                                MTelNo = c.MTelNo,
                                MBirthDate = c.MBirthDate,
                                MGender = c.MGender,
                            };
                        }
                        else
                        {
                            foreach (Staff s in lc.GetStaffCredentials())
                            {
                                if (s.SName.Equals(HttpContext.Session.GetString("Name").ToString()))
                                {
                                    staff = new Staff
                                    {
                                        SName = s.SName,
                                        SEmailAddr = s.SEmailAddr,
                                        STelNo = s.STelNo,
                                        SGender = s.SGender
                                    };
                                }
                            }
                        }
                    }
                    if (customer != null)
                    {
                        ViewBag.Message = customer;
                    }
                    else
                    {
                        if (staff != null)
                        {
                            ViewBag.Message = staff;
                        }
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }
        }

        public ActionResult RemoveItem()
        {
            HttpContext.Session.Remove("Error"); 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalParticulars()
        {
            return RedirectToAction("Change", "Customer"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quit()
        {
            return RedirectToAction("Profile", "Customer"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear()
        {
            return RedirectToAction("Change", "Customer"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedirectToProductsPage()
        {
            return RedirectToAction("Product", "Customer"); 
        }

        private void OpenConnection() { SQL.conn.Open(); }
        private void CloseConnection() { SQL.conn.Close(); }

        public List<Feedback> GetCustomerFeedback()
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Feedback";
            OpenConnection();
            SqlDataReader scanner = command.ExecuteReader();
            List<Feedback> final = new List<Feedback> { }; 
            if (scanner != null)
            {
                if (scanner.HasRows)
                {
                    while (scanner.Read())
                    {
                        //Remember to check for whether the values are null
                        //Check for text and image file name 
                        final.Add(
                            new Feedback
                            {
                                FeedbackID      = scanner.GetInt32(0), 
                                MemberID        = scanner.GetString(1),
                                DateTimePosted  = scanner.GetDateTime(2), 
                                Title           = scanner.GetString(3), 
                                Text            = scanner.IsDBNull(4) ? "" : scanner.GetString(4), 
                                ImageFileName   = scanner.IsDBNull(5) ? "" : scanner.GetString(5),
                            }
                        ); 
                    }
                }
            }
            CloseConnection(); 
            return final; 
        }

        //Create a method that will retrieve the 
        public List<Product> GetProducts()
        {
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = @"SELECT * FROM Product";
            OpenConnection();
            SqlDataReader scanner = command.ExecuteReader(); 
            List<Product> products = new List<Product> { }; 
            if (scanner != null)
            {
                if (scanner.HasRows)
                {
                    while (scanner.Read())
                    {
                        //Check if the user is celebrating his or her birthday today, make the appropriate price adjustments
                        //ProductId, ProductName, ProductImage, Price, EffectiveDate, isObsolete
                        products.Add(new Product
                        {
                            ProductId = scanner.GetInt32(0),
                            ProductName = scanner.GetString(1),
                            ProductImage = scanner.IsDBNull(2) ? "" : scanner.GetString(2),
                            Price = scanner.GetDecimal(3),
                            EffectiveDate = scanner.GetDateTime(4),
                            isObsolete = "1" == scanner.GetString(5) ? true : false
                        }); 
                    }
                } 
            }
            CloseConnection();
            return products; 
        }

        public int GetLastFeedbackID()
        {
            //A list that would be responsible for storing all the 
            List<int> list = new List<int> { };
            foreach (Feedback f in GetCustomerFeedback())
            {
                list.Add(f.FeedbackID);
            }
            return list.Count + 1; 
        }

        public ActionResult SubmitFeedbackAnyway()
        {
            string subject = HttpContext.Session.GetString("SubjectText");
            string feedback = HttpContext.Session.GetString("FeedbackText");
            string imagePath = HttpContext.Session.GetString("ImagePathText");
            HttpContext.Session.Remove("ErrorMessagesDelimitedString");

            //Now form a new feedback object.. 
            Feedback f = new Feedback
            {
                FeedbackID = GetLastFeedbackID(),
                MemberID = HttpContext.Session.GetString("ID"),
                DateTimePosted = DateTime.Now,
                Title = subject,
                Text = feedback,
                ImageFileName = imagePath //By default if there is no image path, it will be 'download.png' 
            };

            //Post this feedback object to the database... 
            PostCustomerFeedback(f); 

            HttpContext.Session.SetString("FeedbackPostingSuccessful", "Your feedback has been posted successfully");
            return RedirectToAction("Feedback", "Customer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeedbackHandling(IFormCollection formData)
        {
            ActionResult result = null; 
            List<string> errorList = new List<string> { }; 
            //Main textarea that the customer can use to input their feedback can be stored under the Text property under the Feedback model
            //Store them in a Feedback object... 
            //Call the PostCustomerFeedback() method and then pass the feedback object inside. 
            //Once the posting is done it will then call RedirectToAction("Feedback", "Customer")
            //call the GetCustomerFeedback() database
            string subject = formData["FeedbackSubject"].ToString();     //Get the title 
            string feedback = formData["FeedbackContent"].ToString();    //Get the feedback text 
            string imagePath = formData["ImageFileName"].ToString();  //Get the path of the string

            //Post the above 3 variables into another method
            HttpContext.Session.SetString("SubjectText", subject); 
            HttpContext.Session.SetString("FeedbackText", feedback);    
            HttpContext.Session.SetString("ImagePathText", imagePath);

            //Set acceptable file formats to .jpeg, .jpg, .png only
            if (imagePath.Contains(".jpeg") || imagePath.Contains(".png") || imagePath.Contains("jpg"))
            {
                int recurrence = 0; 
                foreach (string s in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\wwwroot\\Images"))
                {
                    if (s.Contains(imagePath))
                    {
                        ++recurrence; 
                    }
                }
                string combined = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", imagePath);
                using (var fileSteam = new FileStream(
                    combined + (recurrence > 0 ? imagePath + $"({recurrence})" : imagePath), FileMode.Create))
                { }
            }
            else
            {
                errorList.Add("Invalid file format. We only accept image file formats of type .jpeg, .jpg, and lastly, .png"); 
            }


            //Need to handle another error (subject, feedback and imagePath) cannot contain the ` character 
            if (feedback.Length == 0)
            {
                errorList.Add("You have not inserted any text into the feedback textbox");
            }
            else
            {
                if (feedback.Contains("`"))
                {
                    errorList.Add("There is an illegal character in your feedback. It should not contain the backtick character anywhere in your feedback.");
                }
            }

            if (imagePath.Length == 0)
            {
                errorList.Add("You have not inserted any images into your feedback"); //Only this error is optional 
            }
            else
            {
                if (imagePath.Contains("`"))
                {
                    errorList.Add("There is an illegal character in your image path. It should not contain the backtick character anywhere in your image path.");
                }
            }

            if (subject.Length == 0)
            {
                errorList.Add("You have not inserted any text into the subject portion");
            }
            else
            {
                if (subject.Contains("`"))
                {
                    errorList.Add("There is an illegal character in your feedback title. It should not contain the backtick character anywhere in your feedback title.");
                }
            }

            //Check if the errorList length is more than 0 (contains errors), or else there is no errors and therefore we will be able to post the data into the SQL database
            if (errorList.Count > 0)
            {
                string sender = string.Empty; 
                foreach (string s in errorList)
                {
                    sender = sender + (errorList.IndexOf(s).Equals(0) ? "" : "`") + s; 
                }
                //Send the backtick delimited string back to the CreateFeedback view
                HttpContext.Session.SetString("ErrorMessagesDelimitedString", sender); 
                result = RedirectToAction("CreateFeedback", "Customer"); 
            }
            else
            {
                HttpContext.Session.Remove("ErrorMessagesDelimitedString"); 
                //Now form a new feedback object.. 
                Feedback f = new Feedback
                {
                    FeedbackID = GetLastFeedbackID(),
                    MemberID = HttpContext.Session.GetString("ID"),
                    DateTimePosted = DateTime.Now,
                    Title = subject,
                    Text = feedback,
                    ImageFileName = imagePath //By default if there is no image path, it will be 'download.png' 
                };

                //Post this feedback object to the database... 
                PostCustomerFeedback(f); 

                HttpContext.Session.SetString("FeedbackPostingSuccessful", "Your feedback has been posted successfully"); 
                result = RedirectToAction("Feedback", "Customer");
            }
            return result;  
        }

        public ActionResult ResetErrorInformation()
        {
            HttpContext.Session.Remove("ErrorMessagesDelimitedString");
            return RedirectToAction("CreateFeedback", "Customer"); 
        }

        //This message is used to reset the messages that has been generated by the system when the user has successfully posted a feedback to the SQL database.
        public ActionResult ClearMessage()
        {
            HttpContext.Session.Remove("ErrorMessagesDelimitedString");
            HttpContext.Session.Remove("FeedbackPostingSuccessful"); 
            return RedirectToAction("Profile", "Customer"); 
        }

        //This method will, in fact post the information into the database
        public void PostCustomerFeedback(Feedback feedback)
        {
            //{feedback.FeedbackID}, {feedback.MemberID}, {feedback.DateTimePosted}, {feedback.Title}, {feedback.Text}, {feedback.ImageFileName}
            SqlCommand command = SQL.conn.CreateCommand();
            command.CommandText = $"SET IDENTITY_INSERT Feedback ON INSERT INTO Feedback (FeedbackID, MemberID, DateTimePosted, Title, Text, ImageFileName) VALUES (@FeedbackID, @MemberID, @DateTimePosted, @Title, @Text, @ImageFileName)";
            //Now attach the values to your alias
            OpenConnection();
            command.Parameters.AddWithValue("@FeedbackID", feedback.FeedbackID);
            command.Parameters.AddWithValue("@MemberID", feedback.MemberID); 
            command.Parameters.AddWithValue("@DateTimePosted", feedback.DateTimePosted);   
            command.Parameters.AddWithValue("@Title", feedback.Title);   
            command.Parameters.AddWithValue("@Text", feedback.Text);
            command.Parameters.AddWithValue("@ImageFileName", feedback.ImageFileName); 
            command.ExecuteNonQuery(); 
            CloseConnection(); 
        }

        public void UpdatePersonalParticulars(Customer user)
        {
            //[MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]
            SqlCommand command = SQL.conn.CreateCommand();  
            command.CommandText = @"UPDATE Customer SET MemberID=@MemberID, MName=@MName, MGender=@MGender, MBirthDate=@MBirthDate, MAddress=@MAddress, MCountry=@MCountry, MTelNo=@MTelNo, MEmailAddr=@MEmailAddr, MPassword=@MPassword WHERE MName=@TargetUsername";
            //Tell SQL what data you want to plug in
            OpenConnection();
            command.Parameters.AddWithValue("@MemberID", user.MemberID); 
            command.Parameters.AddWithValue("@MName", user.MName);
            command.Parameters.AddWithValue("@MGender", user.MGender); 
            command.Parameters.AddWithValue("@MBirthDate", user.MBirthDate); 
            command.Parameters.AddWithValue("@MAddress", user.MAddress);
            command.Parameters.AddWithValue("@MCountry", user.MCountry);
            command.Parameters.AddWithValue("@MTelNo", user.MTelNo);
            command.Parameters.AddWithValue("@MEmailAddr", user.MEmailAddr);
            command.Parameters.AddWithValue("@MPassword", user.Password);
            //Tell SQL which customer record you want it to be updated
            command.Parameters.AddWithValue("@TargetUsername", user.MName); 
            command.ExecuteNonQuery(); 
            CloseConnection(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNewPersonalParticularsAnyway()
        {
            string email = HttpContext.Session.GetString("TempStorageForEmailAddr");
            string address= HttpContext.Session.GetString("TempStorageForResidentialAddr");
            string telno = HttpContext.Session.GetString("TempStorageForTelNo");
            Customer c = new Customer
            {
                MName = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MName,
                MemberID = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MemberID,
                MGender = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MGender,
                MBirthDate = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MBirthDate,
                MCountry = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MCountry,
                Password = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).Password,
                MEmailAddr = email.Length > 0 ? email : new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MEmailAddr,
                MAddress = address.Length > 0 ? address : new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MAddress,
                MTelNo = telno.Length > 0 ? telno : new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MTelNo
            };
            UpdatePersonalParticulars(c);
            HttpContext.Session.Remove("Error"); 
            TempData["Update successful"] = "Your personal particulars have been updated";
            System.Threading.Thread.Sleep(5000);
            return RedirectToAction("Profile", "Customer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNewPersonalParticulars(IFormCollection formData)
        {
            string newEmailAddr = formData["NewUserEmailAddr"].ToString();
            string newResidentialAddr = formData["UserNewResidentialAddress"].ToString();
            string newTelephoneNumber = formData["UserNewTelNo"].ToString();
            //Relay this information to the SetNewParticularsAnyway
            HttpContext.Session.SetString("TempStorageForEmailAddr", newEmailAddr); 
            HttpContext.Session.SetString("TempStorageForResidentialAddr", newResidentialAddr); 
            HttpContext.Session.SetString("TempStorageForTelNo", newTelephoneNumber); 

            List<string> errorList = new List<string> { };  //STORES AN ARRAY OF ERROR STRINGSS

            ActionResult result = null;

            if (newEmailAddr.Length == 0 || newResidentialAddr.Length == 0 || newTelephoneNumber.Length == 0)
            {
                //Prompt the user whether he or she would want to save an empty input into the database
                if (newEmailAddr.Length == 0)
                {
                    errorList.Add("Email Address field has been left blank!");
                    if (newResidentialAddr.Length == 0)
                    {
                        errorList.Add("Residential address field has been left blank!");
                        if (newTelephoneNumber.Length == 0)
                        {
                            errorList.Add("Telephone number field has been left blank!");
                        }
                    }
                    else
                    {
                        if (newTelephoneNumber.Length == 0)
                        {
                            errorList.Add("Telephone number field has been left blank!");
                        }
                    }
                }
                else if (newResidentialAddr.Length == 0)
                {
                    errorList.Add("Residential address field has been left blank!");
                    if (newTelephoneNumber.Length == 0)
                    {
                        errorList.Add("Telephone number field has been left blank!");
                    }
                }
                else if (newTelephoneNumber.Length == 0) // newEmailAddr and the newResidentialAddr all equal to 0
                {
                    //in theory the list should be 1
                    errorList.Add("Telephone number field has been left blank!");
                }
            }
            else
            {
                //Check for duplicates... 
                //Insert some more validation over here 
                if (new LoginCustomerController().GetCustomerByEmailAddress(newEmailAddr) != null)
                {
                    errorList.Add("A duplicate email address was found");
                    if (new LoginCustomerController().GetCustomerByTelNo(newTelephoneNumber) != null)
                    {
                        errorList.Add("A duplicate telephone number was found");
                        if (new LoginCustomerController().GetCustomerByResidentialAddr(newResidentialAddr) != null)
                        {
                            errorList.Add("A duplicate residential address was found");
                        }
                    }
                    else //new LoginCustomerController().GetCustomerByTelNo(newTelephoneNumber) == null
                    {
                        if (new LoginCustomerController().GetCustomerByResidentialAddr(newResidentialAddr) != null)
                        {
                            errorList.Add("A duplicate residential address was found");
                        }
                    }
                }
                else if (new LoginCustomerController().GetCustomerByTelNo(newTelephoneNumber) != null)
                {
                    errorList.Add("A duplicate telephone number was found");
                    if (new LoginCustomerController().GetCustomerByEmailAddress(newEmailAddr) != null)
                    {
                        errorList.Add("A duplicate email address was found");
                        if (new LoginCustomerController().GetCustomerByResidentialAddr(newResidentialAddr) != null)
                        {
                            errorList.Add("A duplicate residential address was found");
                        }
                    }
                    else
                    {
                        if (new LoginCustomerController().GetCustomerByResidentialAddr(newResidentialAddr) != null)
                        {
                            errorList.Add("A duplicate residential address was found");
                        }
                    }
                }
                else if (new LoginCustomerController().GetCustomerByResidentialAddr(newResidentialAddr) != null)
                {
                    if (new LoginCustomerController().GetCustomerByEmailAddress(newEmailAddr) != null)
                    {
                        errorList.Add("A duplicate email address was found");
                        if (new LoginCustomerController().GetCustomerByTelNo(newTelephoneNumber) != null)
                        {
                            errorList.Add("A duplicate telephone number was found");
                        }
                    }
                    else
                    {
                        if (new LoginCustomerController().GetCustomerByTelNo(newTelephoneNumber) != null)
                        {
                            errorList.Add("A duplicate telephone number was found");
                        }
                    }
                }
            }
            if (errorList.Count > 0)
            {
                string parser = string.Empty;
                errorList.ForEach(item =>
                {
                    parser = parser + (errorList.IndexOf(item) == 0 ? "" : ",") + item; 
                }); 
                HttpContext.Session.SetString("Error", parser); 
                result = RedirectToAction("Change", "Customer");
            }
            else
            {
                //Check whether the user's email address is authentic


                //If the errorList's length is exactly 1, means that there are no more noticeable errors, we can now post the new personal particulars in the database
                //Form a new Customer object or Staff object 
                Customer c = new Customer
                {
                    MName       = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MName,
                    MemberID    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MemberID,
                    MGender     = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MGender,
                    MBirthDate  = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MBirthDate, 
                    MCountry    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).MCountry,
                    Password    = new LoginCustomerController().GetCustomerById(HttpContext.Session.GetString("ID")).Password,
                    MEmailAddr  = newEmailAddr,
                    MAddress    = newResidentialAddr, 
                    MTelNo      = newTelephoneNumber
                };
                UpdatePersonalParticulars(c); 
                TempData["Update successful"] = "Your personal particulars have been updated";
                System.Threading.Thread.Sleep(5000); 
                result = RedirectToAction("Profile", "Customer"); 
            }
            return result; 
        }
    }
}