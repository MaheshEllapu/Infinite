using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Mini_Project_RRS;

namespace Testing
{
    [TestFixture]
    public class AuthServiceTests
    {
        [SetUp]
        public void Setup()
        {
            // This runs before every test
            AuthService.Logout();
        }

        [Test]
        public void Login_WithValidCredentials_ReturnsTrueAndSetsUserData()
        {
            // Arrange - these values must exist in your test DB
            string username = "testuser";
            string password = "testpass";

            // Act
            bool result = AuthService.Login(username, password);

            // Assert
            ClassicAssert.IsTrue(result, "Login should succeed for valid credentials.");
            ClassicAssert.AreEqual(username, AuthService.LoggedInUsername);
            ClassicAssert.IsNotNull(AuthService.LoggedInUserRole);
            ClassicAssert.Greater(AuthService.LoggedInUserId, 0);
        }

        [Test]
        public void Login_WithInvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string username = "invalid";
            string password = "wrong";

            // Act
            bool result = AuthService.Login(username, password);

            // Assert
            ClassicAssert.IsFalse(result, "Login should fail for invalid credentials.");
            ClassicAssert.AreEqual(0, AuthService.LoggedInUserId);
            ClassicAssert.IsNull(AuthService.LoggedInUserRole);
        }

        [Test]
        public void Register_WithUniqueUsername_ReturnsTrue()
        {
            // Arrange
            string username = "newuser_" + Guid.NewGuid().ToString("N").Substring(0, 5);
            string email = $"{username}@test.com";
            string phone = "9999999999";
            string password = "password123";

            // Act
            bool result = AuthService.Register(username, email, phone, password);

            // Assert
            ClassicAssert.IsTrue(result, "Registration should succeed for a unique username.");
        }

        [Test]
        public void Register_WithDuplicateUsername_ReturnsFalse()
        {
            // Arrange - use same user twice
            string username = "dupuser_" + Guid.NewGuid().ToString("N").Substring(0, 5);
            string email = $"{username}@test.com";
            string phone = "8888888888";
            string password = "password123";

            // First registration
            bool firstResult = AuthService.Register(username, email, phone, password);
            ClassicAssert.IsTrue(firstResult, "First registration should succeed.");

            // Act - try again with same username
            bool secondResult = AuthService.Register(username, email, phone, password);

            // Assert
            ClassicAssert.IsFalse(secondResult, "Registration should fail for duplicate username.");
        }

        [Test]
        public void Logout_ClearsLoggedInUserData()
        {
            // Arrange
            AuthService.Login("testuser", "testpass");

            // Act
            AuthService.Logout();

            // Assert
            ClassicAssert.AreEqual(0, AuthService.LoggedInUserId);
            ClassicAssert.IsNull(AuthService.LoggedInUserRole);
            ClassicAssert.IsNull(AuthService.LoggedInUsername);
        }
    }
}
