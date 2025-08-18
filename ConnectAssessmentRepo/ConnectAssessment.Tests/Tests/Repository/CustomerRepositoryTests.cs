using ConnectAssessment.Data;
using ConnectAssessment.Data.Models.Entities;
using ConnectAssessment.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ConnectAssessment.Tests.Repositories
{
    [TestFixture]
    public class CustomerRepositoryTests : IDisposable
    {
        private ConnectAssessmentDbContext _context;
        private CustomerRepository _repo;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ConnectAssessmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ConnectAssessmentDbContext(options);
            _repo = new CustomerRepository(options);
        }

        [Test]
        public async Task AddAsync_AddsCustomer_WhenValid()
        {
            var customer = new tbCustomer
            {
                Name = "John",
                SurName = "Doe",
                CompanyName = "Test Company",
                AccountNumber = "ACC123",
                Branch = "Main"
            };

            await _repo.AddAsync(customer);
            var dbCustomer = await _context.tbCustomers.FindAsync(customer.CustomerId);

            Assert.That(dbCustomer, Is.Not.Null);
            Assert.That(dbCustomer.Name, Is.EqualTo("John"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentNullException_WhenNull()
        {
            var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.AddAsync(null));
            Assert.That(ex.ParamName, Is.EqualTo("customer"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenNameEmpty()
        {
            var customer = new tbCustomer
            {
                Name = "",
                SurName = "Doe",
                AccountNumber = "ACC123",
                Branch = "Main"
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(customer));
            Assert.That(ex.ParamName, Is.EqualTo("Name"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenSurNameEmpty()
        {
            var customer = new tbCustomer
            {
                Name = "John",
                SurName = "",
                AccountNumber = "ACC123",
                Branch = "Main"
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(customer));
            Assert.That(ex.ParamName, Is.EqualTo("SurName"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenAccountNumberEmpty()
        {
            var customer = new tbCustomer
            {
                Name = "John",
                SurName = "Doe",
                CompanyName = "Test Company",
                AccountNumber = "",
                Branch = "Main"
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(customer));
            Assert.That(ex.ParamName, Is.EqualTo("AccountNumber"));
        }

        [Test]
        public void AddAsync_ThrowsArgumentException_WhenBranchEmpty()
        {
            var customer = new tbCustomer
            {
                Name = "John",
                SurName = "Doe",
                CompanyName = "Test Company",
                AccountNumber = "ACC123",
                Branch = ""
            };
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(customer));
            Assert.That(ex.ParamName, Is.EqualTo("Branch"));
        }

        [Test]
        public async Task GetByIdAsync_ReturnsCustomer_WhenExists()
        {
            var customer = new tbCustomer
            {
                Name = "Jane",
                SurName = "Smith",
                CompanyName = "Example Corp",
                AccountNumber = "ACC999",
                Branch = "West"
            };
            await _context.tbCustomers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var result = await _repo.GetByIdAsync(customer.CustomerId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Jane"));
        }

        [Test]
        public void GetByIdAsync_ThrowsArgumentException_WhenIdInvalid()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repo.GetByIdAsync(0));
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllCustomers()
        {
            var c1 = new tbCustomer
            {
                Name = "Alice",
                SurName = "Wonder",
                CompanyName = "Wonderland Inc.",
                AccountNumber = "ACC111",
                Branch = "A"
            };
            var c2 = new tbCustomer
            {
                Name = "Bob",
                SurName = "Builder",
                CompanyName = "Wonderland Group.",
                AccountNumber = "ACC222",
                Branch = "B"
            };

            await _context.tbCustomers.AddRangeAsync(c1, c2);
            await _context.SaveChangesAsync();

            var all = await _repo.GetAllAsync();
            Assert.That(all, Has.Exactly(2).Items);
        }

        [Test]
        public async Task UpdateAsync_UpdatesCustomer_WhenValid()
        {
            var customer = new tbCustomer
            {
                Name = "James",
                SurName = "Brown",
                CompanyName = "Tech Corp",
                AccountNumber = "ACC555",
                Branch = "HQ"
            };
            await _context.tbCustomers.AddAsync(customer);
            await _context.SaveChangesAsync();

            customer.Name = "Jamie";
            await _repo.UpdateAsync(customer);

            var updated = await _context.tbCustomers.FindAsync(customer.CustomerId);
            Assert.That(updated.Name, Is.EqualTo("Jamie"));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}