using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using web_asignment1_Y2S1_2022.Models;

namespace web_asignment1_Y2S1_2022.Controllers
{
    public class MarketManagerController : Controller
    {
        // GET: MarketManagerController
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == String.Empty)
            {
                //Prompt the user to log in to verify that the user is a Marketing Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            else
            {
                if (HttpContext.Session.GetString("Role").Equals("Marketing Manager"))
                {
                    ViewData["Page"] = "ViewFeedback";
                    return View(GetFeedbacks());
                }
                else
                {
                    return RedirectToAction("Forbidden", "Home");
                }
            }

        }
        public IActionResult ViewResponse()
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == String.Empty)
            {
                //Prompt the user to log in to verify that the user is a Marketing Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            //Display all response in database
            ViewData["Page"] = "ViewResponse";
            return (View(GetResponse()));
        }
        //GET: Get all feedbacks from the sql database
        private List<Feedback> GetFeedbacks() 
        {
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Feedback ORDER BY DateTimePosted Desc";
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Feedback> feedbackList = new List<Feedback>();
            //Get Data from database
            while (reader.Read())
            {
                feedbackList.Add(
                    new Feedback
                    {
                        FeedbackID = reader.GetInt32(0),
                        MemberID = reader.GetString(1),
                        DateTimePosted = reader.GetDateTime(2),
                        Title = reader.GetString(3),
                        Text = reader.GetString(4),                        
                        ImageFileName = reader.IsDBNull(5) ?"N/A":reader.GetString(5)
                    });
            }
            SQL.conn.Close();
            //Return all the feedback item from database
            return feedbackList;
        }

        //GET: Get all the response item from Database
        public List<Response> GetResponse()
        {
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"Select * from Response order by DateTimePosted";
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Response> responseList = new List<Response>();
            //Add data to the response list
            while (reader.Read())
            {
                responseList.Add(
                    new Response
                    {
                        ResponseID = reader.GetInt32(0),
                        FeedbackID = reader.GetInt32(1),
                        MemberID = reader.IsDBNull(2) ?"N/A":reader.GetString(2),
                        StaffID = reader.IsDBNull(3) ?"N/A":reader.GetString(3),
                        DateTimePosted = reader.GetDateTime(4),                        
                        Text = reader.GetString(5),
                    });
            }
            SQL.conn.Close();
            //Return all response item in database
            return responseList;
        }
        //GET: Get the specific response item
        public Response GetSpecificResponse(int id)
        {
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"Select * from Response where ResponseID = @ResponseID";
            cmd.Parameters.AddWithValue("@ResponseID", id);
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Response response = new Response();
            while (reader.Read())
            {
                response.ResponseID = reader.GetInt32(0);
                response.FeedbackID = reader.GetInt32(1);
                response.MemberID = reader.IsDBNull(2) ? "N/A" : reader.GetString(2);
                response.StaffID = reader.IsDBNull(3) ? "N/A" : reader.GetString(3);
                response.DateTimePosted = reader.GetDateTime(4);
                response.Text = reader.GetString(5);
            }
            SQL.conn.Close();           
            return response;
        }
        //GET: Get the specific Feedback item
        public Feedback GetSpecificFeedback(int id)
        {
            //Retrive the specific feedback row from database by FeedbackID
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Feedback WHERE FeedbackID = @FeedbackID";
            cmd.Parameters.AddWithValue("@FeedbackID", id);
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Feedback feedback = new Feedback();
            while (reader.Read())
            {
                feedback.FeedbackID = reader.GetInt32(0);
                feedback.MemberID = reader.GetString(1);
                feedback.DateTimePosted = reader.GetDateTime(2);
                feedback.Title = reader.GetString(3);
                feedback.Text = reader.GetString(4);
                feedback.ImageFileName = reader.IsDBNull(5) ? "N/A" : reader.GetString(5);
            }
            SQL.conn.Close();
            //Return the specific feedback item
            return feedback;
        }


        // GET: Create new response item
        public IActionResult CreateResponse(int? id)
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == String.Empty)
            {
                //Prompt the user to log in to verify that the user is a Marketing Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            //Get the specific feedback item using feedbackid from view
            Feedback feedback = GetSpecificFeedback(id.Value);
            Response response = new Response();
            //Overwrite response item with coresponding feedback information
            response.MemberID = feedback.MemberID;
            response.FeedbackID = feedback.FeedbackID;

            return View(response);
        }

        // POST: Create Response
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResponse(Response response)
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == String.Empty)
            {
                //Prompt the user to log in to verify that the user is a Marketing Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            // Check Validity
            if (ModelState.IsValid)
            {
                //Insert into DataBase
                int responseID = response.addNewResponse(response);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
        //GET: Get all transaction items
        public List<SalesTransactions> GetTransactions()
        {
            ViewData["Page"] = "ViewTransaction";
            SqlCommand cmd = SQL.conn.CreateCommand();
            //Select only memberid and sum of total for the corresponding memberid from database
            //Filter only the transaction made last month
            cmd.CommandText = @"select MemberID, sum (Total) as 'Total' from SalesTransaction 
                                where DATEPART(m,DateCreated) = DATEPART(m,DATEADD(m,-1,GETDATE())) 
                                group by MemberID order by Total Desc";
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<SalesTransactions> transactionList = new List<SalesTransactions>();
            while (reader.Read())
            {
                SalesTransactions s =
                    new SalesTransactions
                    {
                        MemberID = reader.IsDBNull(0) ? "N/A" : reader.GetString(0),
                        Total = reader.GetDecimal(1),                        
                    };
                transactionList.Add(s);
            }
            SQL.conn.Close();
            

            return transactionList;
        }
        //GET: Display all transactions for each member
        public IActionResult ViewTransaction()
        {
            ViewData["Page"] = "ViewTransaction";
            return (View(GetTransactions()));
        }
        //GET: Get the specific transaction for member
        public SalesTransactions GetSpecificTransaction(string id)
        {
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"select MemberID, sum (Total) as 'Total' from SalesTransaction 
                                where (DATEPART(m,DateCreated) = DATEPART(m,DATEADD(m,-1,GETDATE()))) And (MemberID = @MemberID)
                                group by MemberID order by Total Desc";
            cmd.Parameters.AddWithValue("@MemberID", id);
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            SalesTransactions transaction = new SalesTransactions();
            while (reader.Read())
            {

                transaction.MemberID = reader.IsDBNull(0) ? "N/A" : reader.GetString(0);
                transaction.Total = reader.GetDecimal(1);
                    
            }
            SQL.conn.Close();
            return transaction;
        }
        //GET: Add Voucher into Database
        public void GetVoucher(SalesTransactions transaction)
        {
            decimal amount = new decimal();
            if(transaction.Total < 200)
            {
                amount = 0;
            }
            else if (transaction.Total < 500)
            {
                amount = 20;
            }
            else if (transaction.Total < 1000)
            {
                amount = 40;
            }
            else if (transaction.Total < 1500)
            {
                amount = 80;
            }
            else
            {
                amount = 160;
            }
            //Get the current month and year
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            SqlCommand sqlCommand = SQL.conn.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO CashVoucher (MemberID, Amount, MonthIssuedFor, YearIssuedFor, Status)" +
                " VALUES(@MemberID, @Amount, @MonthIssuedFor, @YearIssuedFor, @Status)";
            sqlCommand.Parameters.AddWithValue("@MemberID", transaction.MemberID);
            sqlCommand.Parameters.AddWithValue("@Amount", amount);
            sqlCommand.Parameters.AddWithValue("@MonthIssuedFor", month);
            sqlCommand.Parameters.AddWithValue("@YearIssuedFor", year);
            sqlCommand.Parameters.AddWithValue("@Status", 0);
            SQL.conn.Open();
            sqlCommand.ExecuteNonQuery();
            SQL.conn.Close();
        }
        //GET: Issue Voucher 
        public ActionResult IssueVoucher(string memberId = "")
        {
            if (HttpContext.Session.GetString("Role") != null && HttpContext.Session.GetString("Role") == String.Empty)
            {
                //Prompt the user to log in to verify that the user is a Marketing Manager
                return RedirectToAction("Login", "LoginCustomer");
            }
            ViewData["MemberID"] = memberId;
            if (memberId == "")
            {
                return RedirectToAction("Index");
            }
            //Issue Voucher only if pass validation
            if (CheckRepeat(memberId)==false)
            {
                SalesTransactions t = GetSpecificTransaction(memberId);
                GetVoucher(t);
                return RedirectToAction(nameof(Index));
            }
            //Redirect to Error page
            else
            {
                return RedirectToAction(nameof(VoucherRepeated));
            }
        }
        //GET: Check Whether the Voucher already issued before
        public bool CheckRepeat(string id)
        {
            bool status = true;
            SqlCommand cmd = SQL.conn.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(1) from CashVoucher where MemberID = @MemberID 
                                AND YearIssuedFor = @YearIssuedFor AND MonthIssuedFor = @MonthIssuedFor";
            cmd.Parameters.AddWithValue("@MemberID", id);
            cmd.Parameters.AddWithValue("@YearIssuedFor", DateTime.Now.Year);
            cmd.Parameters.AddWithValue("@MonthIssuedFor", DateTime.Now.Month);
            SQL.conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            int check = 1;
            //Get the number of times occruing for the member in current month
            while (reader.Read())
            {
                check = reader.GetInt32(0);
            }
            SQL.conn.Close();
            //Check whether there is repeat
            if(check== 0)
            {
                status = false;
            }
            return status;
        }
        //GET: Error page
        public ActionResult VoucherRepeated()
        {
            return View();
        }


    }
}
