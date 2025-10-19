using NUnit.Framework;
using Library;

namespace Library.Tests
{
    public class AdminTests
    {
        private Admin admin;
        private User testUser;

        [SetUp]
        public void Setup()
        {
            admin = new Admin("Camila", "camila@example.com", "099123456", "123");
            testUser = new User("Sebastián", "sebastian@example.com", "099987654", "456");
        }

        [Test]
        public void CreateSeller_ShouldReturnTrue_WhenUserIsValidAndNotExists()
        {
            bool result = admin.CreateSeller(testUser);

            Assert.That(result, Is.True);
            Assert.That(admin.Sellers.Count, Is.EqualTo(1));
            Assert.That(admin.Sellers[0].Name, Is.EqualTo("Sebastián"));
        }

        [Test]
        public void CreateSeller_ShouldReturnFalse_WhenUserIsNull()
        {
            bool result = admin.CreateSeller(null);

            Assert.That(result, Is.False);
            Assert.That(admin.Sellers.Count, Is.EqualTo(0));
        }

        [Test]
        public void CreateSeller_ShouldReturnFalse_WhenUserAlreadyExists()
        {
            admin.CreateSeller(testUser);
            bool result = admin.CreateSeller(testUser);

            Assert.That(result, Is.False);
            Assert.That(admin.Sellers.Count, Is.EqualTo(1));
        }

        [Test]
        public void SuspendSeller_ShouldReturnTrue_WhenSellerExists()
        {
            admin.CreateSeller(testUser);

            bool result = admin.SuspendSeller("456");

            Assert.That(result, Is.True);
            Assert.That(admin.Sellers[0].IsSuspended, Is.True);
        }

        [Test]
        public void SuspendSeller_ShouldReturnFalse_WhenSellerDoesNotExist()
        {
            bool result = admin.SuspendSeller("999");

            Assert.That(result, Is.False);
        }
    }
}