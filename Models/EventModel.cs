
using System;
using System.Text.Json.Serialization;

namespace RaceDayMVC.Models
{
    public class EventModel
    {
        [JsonPropertyName("event_id")]
        public int EventId { get; set; }

        [JsonPropertyName("event_type")]
        public string? EventType { get; set; }

        [JsonPropertyName("event_name")]
        public string? EventName { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("event_description")]
        public string? EventDescription { get; set; }

        [JsonPropertyName("weather")]
        public string? Weather { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("distance")]
        public string? Distance { get; set; }

        [JsonPropertyName("organiser_id")]
        public int OrganiserId { get; set; }

        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }
    }
}
