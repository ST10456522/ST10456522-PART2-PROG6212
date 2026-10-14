using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class results : ControllerBase
    {
 
    [HttpPost]
       public string post_results(int results_id, int participant_id, int organiser_id, string performance_history)
        {
            string message = "";

            try
            {
                string string_connection = @"Data Source=(localdb)\api_endpoint;Database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                // Check if organiser exists
                string check_organiser = "SELECT COUNT(*) FROM organiser WHERE organiser_id = @organiser_id";
                SqlCommand check1 = new SqlCommand(check_organiser, connect);
                check1.Parameters.AddWithValue("@organiser_id", organiser_id);

                int organiser_count = Convert.ToInt32(check1.ExecuteScalar());

                // Check if participant exists
                string check_participant = "SELECT COUNT(*) FROM participant WHERE participant_id = @participant_id";
                SqlCommand check2 = new SqlCommand(check_participant, connect);
                check2.Parameters.AddWithValue("@participant_id", participant_id);

                int participant_count = Convert.ToInt32(check2.ExecuteScalar());

                if (organiser_count > 0 && participant_count > 0)
                {
                    string query = @"INSERT INTO result
                (results_id, participant_id, organiser_id, performance_history)
                VALUES (@results_id, @participant_id, @organiser_id, @performance_history)";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    run_query.Parameters.AddWithValue("@results_id", results_id);
                    run_query.Parameters.AddWithValue("@participant_id", participant_id);
                    run_query.Parameters.AddWithValue("@organiser_id", organiser_id);
                    run_query.Parameters.AddWithValue("@performance_history", performance_history);

                    run_query.ExecuteNonQuery();

                    message = "Participant results saved successfully";
                }
                else
                {
                    message = "Participant or organiser is not registered";
                }

                connect.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


       
[HttpGet("{participant_id}")]
public string get_results(int participant_id)
        {
            string message = "";

            try
            {
                string string_connection = @"Data Source=(localdb)\api_endpoint;Database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);
                connect.Open();

                string query = "SELECT * FROM result WHERE participant_id = @participant_id";

                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.Parameters.AddWithValue("@participant_id", participant_id);

                SqlDataReader data_found = run_query.ExecuteReader();

                while (data_found.Read())
                {
                    message += "Results ID: " + data_found["results_id"].ToString() +
                        ", Participant ID: " + data_found["participant_id"].ToString() +
                        ", Organiser ID: " + data_found["organiser_id"].ToString() +
                        ", Performance History: " + data_found["performance_history"].ToString()
                        + "\n";
                }

                data_found.Close();
                connect.Close();

                if (message == "")
                {
                    message = "No results found for this participant";
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
