using Rza_valeria.Components;
using Rza_valeria.Services;
using Rza_valeria.Models;
using Rza_valeria.Utilities;
using Microsoft.EntityFrameworkCore;
//install the Microsoft.EntityFrameworkCore.InMemory package
//this allows for creating a temporary database in memory for testing purposes.


namespace UnitTest
{
    public class Tests
    {
        // Private fields to store instances of the database context and customer service
        private TlS2301890RzaContext _context;
        private CustomerService _customerService;

        // Setup method to initialize the in-memory database and service before each test
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
        public async Task AddCustomer()//Verifies that a customer can be added to the database and retrieved successfully
        {   // Creating a new customer with a predefined username and password
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

        [Test]
        public async Task UnexistedCustomer()//Verifies that searching for a non-existent customer returns null
        {
            Customer tempCustomer = new Customer();
            tempCustomer.Username = "admin";
            tempCustomer.Password = PasswordUtils.HashPassword("admin");
            await _customerService.AddCustomerAsync(tempCustomer);
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.Username == "not admin");
            Assert.Null(result);//it passes if it failed
        }
        [Test]
        public async Task Valid_Credentials()//Verifies that a customer with valid credentials can successfully log in
        {
            Customer tempCustomer = new Customer();
            tempCustomer.Username = "admin";
            tempCustomer.Password = PasswordUtils.HashPassword("admin");
            await _customerService.AddCustomerAsync(tempCustomer);
            var result = await _customerService.LogIn(tempCustomer);
            Assert.NotNull(result);
        }
        [Test]
        public async Task IncorrectUsername()//Verifies that login fails for a customer with incorrect username
        {
            Customer tempCustomer = new Customer();
            tempCustomer.Username = "admin";
            tempCustomer.Password = PasswordUtils.HashPassword("admin");
            await _customerService.AddCustomerAsync(tempCustomer);
            tempCustomer.Username = "not admin";
            var result = await _customerService.LogIn(tempCustomer);
            Assert.Null(result);//it passes if it failed
        }
        // TearDown method to clean up resources after each test
        [TearDown]
        public void TearDown() 
        {
            _context.Dispose();// Disposing of the context to free up memory
        }

    }
}