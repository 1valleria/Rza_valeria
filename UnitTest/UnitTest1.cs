using Rza_valeria.Components;
using Rza_valeria.Services;
using Rza_valeria.Models;
using Microsoft.EntityFrameworkCore;




namespace UnitTest
{
    public class Tests
    {
        private TlS2301890RzaContext _context;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TlS2301890RzaContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new TlS2301890RzaContext(options);
            _customerService = new CustomerService(_context);
        }

        [Test]
        public async Task Test1()
        {
            Customer tempCustomer = new Customer()
            {
                Username = "admin",
                Password = "admin"
            };
            await _customerService.AddCustomerAsync(tempCustomer);
            var result = await _context.Customers.FirstOrDefaultAsync(
                c => c.Username == "admin");
            Assert.NotNull(result);
        }

        [TearDown]
        public void TearDown() 
        {
            _context.Dispose();
        }

    }
}