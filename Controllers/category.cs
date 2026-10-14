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

        public string category_post(int category_id, string category_name, string age, int organiser_id, int event_id)
        {
            SqlConnection connect = new SqlConnection(
               @"Data source=(localdb)\api_endpoint;database=Raceday;");

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
                             (category_id, category_name, age, event_id, organiser_id)
                             VALUES
                             (@category_id, @category_name, @age, @event_id, @organiser_id)";

            SqlCommand run_query = new SqlCommand(query, connect);

            run_query.Parameters.AddWithValue("@category_id", category_id);
            run_query.Parameters.AddWithValue("@category_name", category_name);
            run_query.Parameters.AddWithValue("@age", age);
            run_query.Parameters.AddWithValue("@event_id", event_id);
            run_query.Parameters.AddWithValue("@organiser_id", organiser_id);

            run_query.ExecuteNonQuery();

            connect.Close();

            return " category created successfully.";
        }
        [HttpGet]
        public IActionResult category_get()
        {
            SqlConnection connect = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RaceDayDB;Integrated Security=True");

            connect.Open();

            string query = "SELECT category, category_name, age FROM Category";

            SqlCommand run_query = new SqlCommand(query, connect);
            SqlDataReader reader = run_query.ExecuteReader();

            List<object> categories = new List<object>();

            while (reader.Read())
            {
                categories.Add(new
                {
                    category = reader["category"],
                    category_name = reader["category_name"],
                    age = reader["age"]
                });
            }

            reader.Close();
            connect.Close();

            return Ok(categories);
        }
        [HttpGet("category/{category_id}")] //
        public IActionResult enrolment_get()
        {
            SqlConnection connect = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RaceDayDB;Integrated Security=True");

            connect.Open();

            string query = @"SELECT enrolment_id, participant_id, event_id, category
                             FROM Race_Enrolment";

            SqlCommand run_query = new SqlCommand(query, connect);
            SqlDataReader reader = run_query.ExecuteReader();

            List<object> enrolments = new List<object>();

            while (reader.Read())
            {
                enrolments.Add(new
                {
                    enrolment_id = reader["enrolment_id"],
                    participant_id = reader["participant_id"],
                    event_id = reader["event_id"],
                    category = reader["category"]
                });
            }

            reader.Close();
            connect.Close();

            return Ok(enrolments);
        }
    }
}

