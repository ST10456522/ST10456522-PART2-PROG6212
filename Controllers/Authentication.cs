using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace part2POE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
    [HttpPost]
        public string register(string username, string password,
            string role, string fullname = "", string email = "",
            int organiser_id = 0, string organiser_name = "")
        {
            string message = "";

            try
            {
                string string_connection =  @"Data Source=(localdb)\api_endpoint;Database=Raceday;";

                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();

                // Check if username already exists
                string check_query =
                    "SELECT COUNT(*) FROM authentication " +
                    "WHERE username = @username";

                SqlCommand check = new SqlCommand(check_query, connect);
                check.Parameters.AddWithValue("@username", username);

                int count = (int)check.ExecuteScalar();

                if (count > 0)
                {
                    message = "Username already exists";
                }
                else if (role == "Participant")
                {
                    // Save participant personal details
                    string query =
                        "INSERT INTO participant (fullname, email) " +
                        "VALUES (@fullname, @email); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    SqlCommand run_query =
                        new SqlCommand(query, connect);

                    run_query.Parameters.AddWithValue("@fullname", fullname);
                    run_query.Parameters.AddWithValue("@email", email);

                    int participant_id =
                        (int)run_query.ExecuteScalar();

                    // Hash the password
                    PasswordHasher<string> hasher =
                        new PasswordHasher<string>();

                    string password_hash =
                        hasher.HashPassword(username, password);

                    // Save login details
                    string auth_query =
                        "INSERT INTO authentication " +
                        "(authentication_id, username, password, role, " +
                        "participant_id, organiser_id) " +
                        "VALUES ((SELECT ISNULL(MAX(authentication_id), 0) + 1 " +
                        "FROM authentication), @username, @password, " +
                        "'Participant', @participant_id, NULL)";

                    SqlCommand auth_command = new SqlCommand(auth_query, connect);

                    auth_command.Parameters.AddWithValue("@username", username);
                    auth_command.Parameters.AddWithValue("@password", password_hash);
                    auth_command.Parameters.AddWithValue("@participant_id",
                        participant_id);

                    auth_command.ExecuteNonQuery();

                    message = "Participant registered successfully";
                }
                else if (role == "Organiser")
                {
                    // Save organiser personal details
                    string query =
                        "INSERT INTO organiser " +
                        "(organiser_id, organiser_name) " +
                        "VALUES (@id, @name)";

                    SqlCommand run_query =
                        new SqlCommand(query, connect);

                    run_query.Parameters.AddWithValue("@id", organiser_id);
                    run_query.Parameters.AddWithValue("@name", organiser_name);

                    run_query.ExecuteNonQuery();

                    // Hash the password
                    PasswordHasher<string> hasher =
                        new PasswordHasher<string>();

                    string password_hash =
                        hasher.HashPassword(username, password);

                    // Save login details
                    string auth_query =
                        "INSERT INTO authentication " +
                        "(authentication_id, username, password, role, " +
                        "participant_id, organiser_id) " +
                        "VALUES ((SELECT ISNULL(MAX(authentication_id), 0) + 1 " +
                        "FROM authentication), @username, @password, " +
                        "'Organiser', NULL, @organiser_id)";

                    SqlCommand auth_command =
                        new SqlCommand(auth_query, connect);

                    auth_command.Parameters.AddWithValue("@username", username);
                    auth_command.Parameters.AddWithValue("@password", password_hash);
                    auth_command.Parameters.AddWithValue("@organiser_id",
                        organiser_id);

                    auth_command.ExecuteNonQuery();

                    message = "Organiser registered successfully";
                }
                else
                {
                    message = "Invalid role";
                }

                connect.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
        // LOGIN
        [HttpPost("login")]
        public string login(string username, string password)
        {
            string message = "";

            try
            {
                string string_connection =
                    @"Data Source=(localdb)\api_endpoint;Database=Raceday;";

                SqlConnection connect =
                    new SqlConnection(string_connection);

                connect.Open();

                string query =
                    "SELECT * FROM authentication WHERE username = @username";

                SqlCommand run_query =
                    new SqlCommand(query, connect);

                run_query.Parameters.AddWithValue("@username", username);

                SqlDataReader data_found = run_query.ExecuteReader();

                if (data_found.Read())
                {
                    string password_hash =
                        data_found["password"].ToString();

                    string role = data_found["role"].ToString();

                    int? participant_id =
                        data_found["participant_id"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(data_found["participant_id"]);

                    int? organiser_id =
                        data_found["organiser_id"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(data_found["organiser_id"]);

                    PasswordHasher<string> hasher =
                        new PasswordHasher<string>();

                    var result = hasher.VerifyHashedPassword(
                        username, password_hash, password);

                    if (result != PasswordVerificationResult.Failed)
                    {
                        if (role == "Participant")
                        {
                            message = "Login successful. Participant ID: "
                                + participant_id;
                        }
                        else if (role == "Organiser")
                        {
                            message = "Login successful. Organiser ID: "
                                + organiser_id;
                        }
                    }
                    else
                    {
                        message = "Incorrect username or password";
                    }
                }
                else
                {
                    message = "Incorrect username or password";
                }

                data_found.Close();
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
