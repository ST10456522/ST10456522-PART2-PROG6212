using RaceDayMVC.Models;
using System.Text.Json;

namespace RaceDayMVC.Services
{
    public class RaceDayApiClient
    {
        private readonly HttpClient _http;

        public RaceDayApiClient(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("RaceDayApi");
        }

        // 1. LOGIN
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            string url = $"/api/Authentication/login?username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(password)}";
            try
            {
                var response = await _http.PostAsync(url, null);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResponse();
                }
                return new LoginResponse { Success = false, Message = "API Error" };
            }
            catch (Exception ex) { return new LoginResponse { Success = false, Message = ex.Message }; }
        }

        // 2. REGISTER
        public async Task<string> RegisterAsync(string username, string password, string role, string fullname, string email, int organiserId, string organiserName)
        {
            string url = $"/api/Authentication/register?username={Uri.EscapeDataString(username)}&password={Uri.EscapeDataString(password)}&role={Uri.EscapeDataString(role)}&fullname={Uri.EscapeDataString(fullname ?? "")}&email={Uri.EscapeDataString(email ?? "")}&organiser_id={organiserId}&organiser_name={Uri.EscapeDataString(organiserName ?? "")}";
            try
            {
                var response = await _http.PostAsync(url, null);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                return "API error: " + response.StatusCode;
            }
            catch (Exception ex) { return "Error: " + ex.Message; }
        }

        // 3. GET ALL EVENTS
        public async Task<List<EventModel>> GetEventsAsync()
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync("/api/Event");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<List<EventModel>>(json, options) ?? new List<EventModel>();
                }
                return new List<EventModel>();
            }
            catch (Exception) { return new List<EventModel>(); }
        }

        // 4. CREATE EVENT
        public async Task<string> CreateEventAsync(int eventId, string eventType, string eventName, string location, string description, string weather, DateTime eventDate, string distance, int organiserId, int categoryId)
        {
            string formattedDate = eventDate.ToString("yyyy-MM-dd");
            string url = $"/api/Event?event_id={eventId}&event_type={Uri.EscapeDataString(eventType)}&event_name={Uri.EscapeDataString(eventName)}&location={Uri.EscapeDataString(location)}&event_description={Uri.EscapeDataString(description)}&weather={Uri.EscapeDataString(weather)}&event_date={formattedDate}&distance={Uri.EscapeDataString(distance)}&organiser_id={organiserId}&category_id={categoryId}";
            try
            {
                var response = await _http.PostAsync(url, null);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                return "API Error: " + response.StatusCode;
            }
            catch (Exception ex) { return "Error: " + ex.Message; }
        }

        // 5. UPDATE EVENT
        // UPDATE EVENT
        public async Task<string> UpdateEventAsync(int eventId, string eventType, string eventName, string location, string description, string weather, DateTime eventDate, string distance, int organiserId, int categoryId)
        {
            string formattedDate = eventDate.ToString("yyyy-MM-dd");

            // We are placing the eventId right after /api/Event/
            string url = $"/api/Event/{eventId}?event_type={Uri.EscapeDataString(eventType)}&event_name={Uri.EscapeDataString(eventName)}&location={Uri.EscapeDataString(location)}&event_description={Uri.EscapeDataString(description)}&weather={Uri.EscapeDataString(weather)}&event_date={formattedDate}&distance={Uri.EscapeDataString(distance)}&organiser_id={organiserId}&category_id={categoryId}";

            try
            {
                var response = await _http.PutAsync(url, null);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

                // If it fails, read the exact error from the API
                string error = await response.Content.ReadAsStringAsync();
                return $"Error {response.StatusCode}: {error}";
            }
            catch (Exception ex) { return "Connection Error: " + ex.Message; }
        }

        // DELETE EVENT
        public async Task<string> DeleteEventAsync(int eventId, int organiserId)
        {
            // We are placing the eventId right after /api/Event/
            string url = $"/api/Event/{eventId}?organiser_id={organiserId}";

            try
            {
                var response = await _http.DeleteAsync(url);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

                // If it fails, read the exact error
                string error = await response.Content.ReadAsStringAsync();
                return $"Error {response.StatusCode}: {error}";
            }
            catch (Exception ex) { return "Connection Error: " + ex.Message; }
        }

        // 7. ENROL IN EVENT
        public async Task<string> EnrolAsync(int eventId, int participantId, int categoryId)
        {
            // Your API requires an enrolment_id, we generate a random one
            Random rnd = new Random();
            int enrolmentId = rnd.Next(10000, 99999);

            string url = $"/api/Enrolment?enrolment_id={enrolmentId}&participant_id={participantId}&event_id={eventId}&category_id={categoryId}";
            try
            {
                var response = await _http.PostAsync(url, null);
                if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
                return "API Error: " + response.StatusCode;
            }
            catch (Exception ex) { return "Error: " + ex.Message; }
        }
    }
}