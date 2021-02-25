using System;
using System.Linq;
using Common;
using DataAccess.Data;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiddenVilla_Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (_context.Roles.Any(x => x.Name == Sd.RoleAdmin))
            {
                return;
            }

            _roleManager.CreateAsync(new IdentityRole(Sd.RoleAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Sd.RoleCustomer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Sd.RoleEmployee)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();

            var user = _context.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, Sd.RoleAdmin).GetAwaiter().GetResult();
        }
    }
}