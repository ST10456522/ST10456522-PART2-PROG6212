using part2POE.Controllers;

namespace event_enrolmet_test
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Enrol_Participant_Successful()
        {
            Enrolment test = new Enrolment();

            // These IDs must exist in your database.
            string results = test.enrol(
               92765,
                   4003,
                   1,
                   1
            );

            string expected = "participant infomation saved successfully ";

            Assert.Equal(expected, results);
        }
        [Fact]
        public void Test_Get_Enrolments()
        {
            Enrolment test = new Enrolment();

            string results = test.enrolment_get();

            // This assumes your database contains at least one enrolment.
            Assert.Contains("Enrolment ID:", results);
        }
    }
}