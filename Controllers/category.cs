using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class category : ControllerBase
    {
        [HttpPost]

        public string category_post(int category, string category_name, string age, int organiser_id, int event_id)
        {
            SqlConnection connect = new SqlConnection(
               @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RaceDayDB;Integrated Security=True");

            connect.Open();

            // Check whether the organiser exists
            string check = "SELECT COUNT(*) FROM Organiser WHERE organiser_id = @organiser_id";

            SqlCommand check_query = new SqlCommand(check, connect);
            check_query.Parameters.AddWithValue("@organiser_id", organiser_id);

            int organiser_exists = (int)check_query.ExecuteScalar();

            if (organiser_exists == 0)
            {
                connect.Close();
                return "Organiser does not exist.";
            }

            // Check whether the event exists and belongs to this organiser
            string check_event = @"SELECT COUNT(*) FROM Event
                                   WHERE event_id = @event_id
                                   AND organiser_id = @organiser_id";

            SqlCommand check_event_query = new SqlCommand(check_event, connect);
            check_event_query.Parameters.AddWithValue("@event_id", event_id);
            check_event_query.Parameters.AddWithValue("@organiser_id", organiser_id);

            int event_exists = (int)check_event_query.ExecuteScalar();

            if (event_exists == 0)
            {
                connect.Close();
                return "Event does not exist or does not belong to this organiser.";
            }

            // Create the age category
            string query = @"INSERT INTO Category
                             (category, category_name, age, event_id, organiser_id)
                             VALUES
                             (@category, @category_name, @age, @event_id, @organiser_id)";

            SqlCommand run_query = new SqlCommand(query, connect);

            run_query.Parameters.AddWithValue("@category", category);
            run_query.Parameters.AddWithValue("@category_name", category_name);
            run_query.Parameters.AddWithValue("@age", age);
            run_query.Parameters.AddWithValue("@event_id", event_id);
            run_query.Parameters.AddWithValue("@organiser_id", organiser_id);

            run_query.ExecuteNonQuery();

            connect.Close();

            return "Age category created successfully.";
        }
    }
}

