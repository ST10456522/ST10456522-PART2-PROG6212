
using part2POE.Controllers;
namespace event_management
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Create_Event_Failed_Invalid_Organiser()
        {
            Event test = new Event();

            string results = test.CREATE_EVENT(
                98766,
                "Race",
                "Invalid Organiser Event",
                "Johannesburg",
                "Testing an invalid organiser",
                "Sunny",
                new DateTime(2027, 5, 10),
                "10km",
                -9999,
                1
            );

            string expected =
                "Organiser is not registered. Event cannot be posted.";

            Assert.Equal(expected, results);
        }
        [Fact]
        public void Test_Delete_Event_Failed()
        {
            Event test = new Event();

            // Use an event ID that does not exist.
            string results = test.DELETE_EVENT(98768, 1);

            string expected =
                "Event not found or does not belong to this organiser";

            Assert.Equal(expected, results);
        }
    }
}