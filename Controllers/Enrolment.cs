using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Enrolment : ControllerBase
    {
        [HttpPost]
        
         public string enrol(int enrolment_id, int participant_id, int event_id, int category_id)
        {
            string message = "";

            try
            {
                string string_connection = @"Data source=(localdb)\MSSQLLocalDB;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                string query = @"insert into enrolment values ('" + enrolment_id + "', '" + participant_id + "', '" + event_id + "', '" + category_id + "');";
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
        }
    }
}

