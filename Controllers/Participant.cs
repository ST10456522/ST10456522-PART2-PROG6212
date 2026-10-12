using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Participant : ControllerBase
    {
        [HttpPost]
        public string store_participant(int participant_id, string fullname, string email)
        {// start of method

            string message = "";

            try
            {
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                string query = @"insert into participant values ('" + participant_id + "', '" + fullname + "', '" + email + "');";
                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "participant infomation saved successfully ";
                connect.Close();

            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        } // end of method
        [HttpDelete]
        public string participant_details(int participant_id)
        { // start of method
            string message = "";

            try
            {// start of try
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                string query = "Delete from participant " + "where participant_id= " + participant_id;
                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "participant  infomation deleted successfully ";
                connect.Close();

            }// end of try
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }//end of method
        [HttpPut]
        public string update_participantdetails(int participant_id, string fullname, string email)
        {
            string message = "";
            try
            {// start of try
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();

                string query = "UPDATE participant set fullname ='" + fullname + "',email ='"+email+"' WHERE participant_id ='" + participant_id + "';";

                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "participant infomation UPDATED successfully ";
                connect.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
    }
}
