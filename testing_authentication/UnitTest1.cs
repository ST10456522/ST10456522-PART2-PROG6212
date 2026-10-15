using part2POE.Controllers;
using part2POE.Controllers;
namespace testing_authentication
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
           
                Authentication test_login = new Authentication();

                string test_username = "loli";
                string test_password = "loli@123";

                string results = test_login.login(test_username, test_password);

                string expected_results = "Login successful. Participant ID: 4002";

                Assert.Equal(expected_results, results);
            }
        [Fact]
        public void Test_Registration()
        {
            // Testing registration
            Authentication test_register = new Authentication();

            string test_username = "testparticimpant456";
            string test_password = "Test@123n45";
            string test_role = "Participant";
            string test_fullname = "Test Partpicipant";
            string test_email = "testparticipant2456@gmail.com";

            string results = test_register.register(
                test_username,
                test_password,
                test_role,
                test_fullname,
                test_email
            );

            string expected_results = "Participant registered successfully";

            Assert.Equal(expected_results, results);
        }
        [Fact]
        public void Test_Different_Roles()
        {
            // Creating an object of the Authentication controller
            Authentication test = new Authentication();

            // Temporary variables
            string username = "invalidrole987";
            string password = "Test@12345";
            string role = "Guest";

            // Calling the register method
            string results = test.register(
                username,
                password,
                role
            );

            // Expected result
            string expected = "Invalid role";

            // Comparing the actual and expected results
            Assert.Equal(expected, results);
        }


        [Fact]
        public void Test_Organiser_Registration()
        {
            // Creating an object of the Authentication controller
            Authentication test_register = new Authentication();

            // Temporary variables
            string username = "organiser987";
            string password = "Test@12345";
            string role = "Organiser";
            int organiser_id = 987;
            string organiser_name = "Test Organiser";

            // Calling the register method
            string results = test_register.register(
                username,
                password,
                role,
                "",
                "",
                organiser_id,
                organiser_name
            );

            // Expected result
            string expected = "Organiser registered successfully";

            // Comparing actual and expected results
            Assert.Equal(expected, results);
        }

    }
}