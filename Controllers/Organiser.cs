using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;



namespace part2POE.Controllers
{ // start of namespace 
    [Route("api/[controller]")]
    [ApiController]
    public class Organiser : ControllerBase
    { // start of the main
        [HttpPost]
        public string store_infomation (int organiser_id, string organiser_name)
        {// start of method

            string message = "";

            try
            {
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                string query = @"insert into organiser values ('" + organiser_id + "', '" + organiser_name + "');";
                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "Organiser infomation saved successfully ";
                connect.Close();

            }
            catch (Exception error) 
            { 
                message = error.Message;
            }
            return message;
        } // end of method

        [HttpDelete]
        public string organiser_details (int organiser_id)
        { // start of method
            string message = "";

            try
            {// start of try
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                string query = "Delete from organiser " + "where organiser_id= " + organiser_id;
                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "Organiser infomation deleted successfully ";
                connect.Close();

            }// end of try
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
            }//end of method
        [HttpPut]
        public string update_organiserdetails(int organiser_id, string organiser_name)
        {
            string message = "";
            try
            {// start of try
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                
                string query = "UPDATE organiser set organiser_name ='" + organiser_name + "' WHERE organiser_id ='" + organiser_id + "';";
               
                SqlCommand run_query = new SqlCommand(query, connect);
                run_query.ExecuteNonQuery();
                message = "Organiser infomation UPDATED successfully ";
                connect.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }
        [HttpGet]
        public string getinfo(int organiser_id)
        {
            string message = "";
            try
            {// start of try
                string string_connection = @"Data source=(localdb)\api_endpoint;database=Raceday;";
                string query = @"select * from organiser where organiser_id = '" + organiser_id + "';";
                SqlConnection connect = new SqlConnection(string_connection);

                connect.Open();
                SqlCommand run_query = new SqlCommand(query, connect);

                SqlDataReader data_found = run_query.ExecuteReader();

                while (data_found.Read())
                {
                    message = "Organiser name:" + data_found["organiser_name"].ToString();
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
