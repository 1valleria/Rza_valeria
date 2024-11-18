// Import necessary models and Entity Framework Core library.
using Rza_valeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Rza_valeria.Services
{
    // This `CustomerService` class provides methods for managing customer-related operations, such as 
    // adding customers, logging in, changing passwords, and retrieving customer information.
    public class CustomerService
    {
        private readonly RzaContext _context;  // `_context` is a database context for accessing and modifying the Customers table.

        // Constructor that initializes the database context.
        public CustomerService(RzaContext context)
        {
            _context = context;
        }

        // Adds a new customer to the database asynchronously.
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);  // Adds a new customer to the `Customers` table.
            await _context.SaveChangesAsync();  // Commits the changes to the database.
        }

        // Authenticates a customer by checking their username and password.
        public async Task<Customer?> LogIn(Customer customer)
        {
            return await _context.Customers.FirstOrDefaultAsync(
                c => c.Username == customer.Username &&
                c.Password == customer.Password);  // Searches for a customer with matching username and password.
        }

        // Changes the password for a specific customer if the old password matches.
        public async Task ChangePassword(int customerId, string hashedOldpassword, string hashedNewPassword)
        {
            Customer? customer = await _context.Customers.SingleOrDefaultAsync(
                c => c.CustomerId == customerId && c.Password == hashedOldpassword); // Checks if customer ID and old password match.

            if (customer != null)  // If a matching customer is found, update their password.
            {
                customer.Password = hashedNewPassword;
                await _context.SaveChangesAsync();  // Saves the updated password to the database.
            }
        }

        // Retrieves a list of all customers from the database asynchronously.
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();  // Returns all customers as a list.
        }

        // Retrieves a specific customer by their ID asynchronously.
        public async Task<Customer> GetCustomerFromIdAsync(int id)
        {
            return await _context.Customers.FirstAsync(c => c.CustomerId == id);  // Finds a customer by their unique ID.
        }

        // Checks if a given username already exists in the database asynchronously.
        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);  // Searches for a customer with the specified username.
            return result != null;  // Returns true if a matching username exists, otherwise false.
        }
        public async Task<string> GetCustomerNameAsync(int userid)
        {
            if (userid == 0)
            {
                return "";
            }
            else
            {
                Customer customer = _context.Customers.SingleOrDefault(c => c.CustomerId == userid);
                if (customer != null)
                {
                    return $"{customer.FirstName} {customer.LastName}";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
