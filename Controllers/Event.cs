using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Event : ControllerBase
    {
    
[HttpPost]
public string CREATE_EVENT(int event_id, string event_type,
    string event_name, string location, string event_description,
    string weather, int organiser_id, int category_id)
        {
            string message = "";

            try
            {
                string string_connection = @"Data Source=(localdb)\api_endpoint;Database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();

                // Check if organiser exists
                string check_query = "SELECT COUNT(*) FROM organiser WHERE organiser_id = @organiser_id";

                SqlCommand check = new SqlCommand(check_query, connect);
                check.Parameters.AddWithValue("@organiser_id", organiser_id);

                int count = Convert.ToInt32(check.ExecuteScalar());

                if (count > 0)
                {
                    string query = @"INSERT INTO event
                (event_id, event_type, event_name, Location,
                event_description, weather, organiser_id, category_id)
                VALUES (@event_id, @event_type, @event_name, @location,
                @event_description, @weather, @organiser_id, @category_id)";

                    SqlCommand run_query = new SqlCommand(query, connect);

                    run_query.Parameters.AddWithValue("@event_id", event_id);
                    run_query.Parameters.AddWithValue("@event_type", event_type);
                    run_query.Parameters.AddWithValue("@event_name", event_name);
                    run_query.Parameters.AddWithValue("@location", location);
                    run_query.Parameters.AddWithValue("@event_description", event_description);
                    run_query.Parameters.AddWithValue("@weather", weather);
                    run_query.Parameters.AddWithValue("@organiser_id", organiser_id);
                    run_query.Parameters.AddWithValue("@category_id", category_id);

                    run_query.ExecuteNonQuery();

                    message = "Event information saved successfully";
                }
                else
                {
                    message = "Organiser is not registered. Event cannot be posted.";
                }

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
