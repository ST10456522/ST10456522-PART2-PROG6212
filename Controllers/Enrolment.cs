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
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
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

        // GET ALL ENROLLED PARTICIPANTS
        [HttpGet]
        public string enrolment_get()
        {
            string message = "";

            try
            {
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();

                string query = @"SELECT enrolment.enrolment_id,
                                        participant.participant_id,
                                        participant.fullname,
                                        participant.email,
                                        event.event_id,
                                        event.event_name,
                                        enrolment.category_id
                                 FROM enrolment
                                 INNER JOIN participant
                                 ON enrolment.participant_id = participant.participant_id
                                 INNER JOIN event
                                 ON enrolment.event_id = event.event_id";

                SqlCommand run_query = new SqlCommand(query, connect);
                SqlDataReader data_found = run_query.ExecuteReader();

                while (data_found.Read())
                {
                    message += "Enrolment ID: " + data_found["enrolment_id"].ToString() +
                        ", Participant ID: " + data_found["participant_id"].ToString() +
                        ", Full Name: " + data_found["fullname"].ToString() +
                        ", Email: " + data_found["email"].ToString() +
                        ", Event ID: " + data_found["event_id"].ToString() +
                        ", Event Name: " + data_found["event_name"].ToString() +
                        ", Category ID: " + data_found["category_id"].ToString() + "\n";
                }

                data_found.Close();
                connect.Close();

                if (message == "")
                {
                    message = "No enrolled participants found";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }
}

