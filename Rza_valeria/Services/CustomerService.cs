﻿using Rza_valeria.Models;
using Microsoft.EntityFrameworkCore;


namespace Rza_valeria.Services
{
    public class CustomerService
    {
        private readonly TlS2301890RzaContext _context;
        public CustomerService(TlS2301890RzaContext context) 
        { 
            _context = context;
        }





    }
}