using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using web_asignment1_Y2S1_2022.Models;

namespace web_asignment1_Y2S1_2022.Models
{
    public class Response
    {
        //private SqlDataReader reader;

        //public Response(SqlDataReader reader)
        //{
        //    ResponseID = reader.GetInt32(0);
        //    FeedbackID = reader.GetInt32(1);
        //    MemberID = reader.GetString(2);
        //    StaffID = reader.GetString(3);
        //    DateTimePosted = reader.GetDateTime(4);
        //    Text = reader.GetString(5);
        //}

        public int ResponseID { get; set; }
        public int FeedbackID { get; set; }
        [StringLength(9, ErrorMessage = "Member ID cannot be longer than 9 characer!")]
        public string MemberID { get; set; }
        [StringLength(20, ErrorMessage = "Staff ID cannot be longer than 20 characers!")]
        public string StaffID { get; set; }
        [Display(Name = "Post Date and Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DateTimePosted { get; set; }
        [Required]
        public string Text { get; set; }
        public Response() { } 
        
        //public static List<Response> getResponses()
        //{
        //    SqlCommand cmd = SQL.conn.CreateCommand();
        //    cmd.CommandText = @"SELECT * FROM Response ORDER BY ResponseID";
        //    SQL.conn.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    List<Response> responseList = new List<Response>();
        //    while (reader.Read())
        //    {
        //        responseList.Add(new Response(reader));
        //    }
        //    SQL.conn.Close();
        //    return responseList;
        //}
        public int addNewResponse(Response response)
        {
            SqlCommand sqlCommand = SQL.conn.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO Response (FeedbackID, MemberID, StaffID, DateTimePosted, Text) OUTPUT INSERTED.ResponseID" +
                " VALUES(@FeedbackID, @MemberID, @StaffID, @DateTimePosted, @Text)";
            sqlCommand.Parameters.AddWithValue("@FeedbackID", response.FeedbackID);
            sqlCommand.Parameters.AddWithValue("@MemberID",response.MemberID);
            sqlCommand.Parameters.AddWithValue("@StaffID", "Marketing");
            sqlCommand.Parameters.AddWithValue("@DateTimePosted", DateTime.Today);
            sqlCommand.Parameters.AddWithValue("@Text", response.Text);

            SQL.conn.Open();
            response.ResponseID = (int)sqlCommand.ExecuteScalar();
            SQL.conn.Close();
            return response.ResponseID;
        }
    }
}
