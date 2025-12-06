using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VendingMachine.DAL.Model;

namespace VendingMachine.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<Users> _userManger;

        public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManger, UserManager<Users> userManger)
        {
            _context = context;
            _roleManger = roleManger;
            _userManger = userManger;
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!(await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
                if (!await _roleManger.Roles.AnyAsync())
                {
                    await _roleManger.CreateAsync(new IdentityRole { Name = "Admin" });
                    await _roleManger.CreateAsync(new IdentityRole { Name = "Customer" });
                }
                if (!await _userManger.Users.AnyAsync())
                {
                    var user1 = new Users()
                    {
                        Email = "Lama@gmail.com",
                        UserName = "lama rafat",
                        PhoneNumber = "01012345678",
                        EmailConfirmed = true,
                    };
                    var user2 = new Users()
                    {
                        Email = "nemeh@gmail.com",
                        UserName = "nemeh fayyad",
                        PhoneNumber = "01012345678",
                        EmailConfirmed = true,
                    };
                    var user3 = new Users()
                    {
                        Email = "layal@gmail.com",
                        UserName = "layal rafat",
                        PhoneNumber = "01012345678",
                        EmailConfirmed = true,
                    };

                    await _userManger.CreateAsync(user1, "La@000");
                    await _userManger.CreateAsync(user2, "Ly@000");
                    await _userManger.CreateAsync(user3, "na@000");

                    await _userManger.AddToRoleAsync(user1, "Admin");
                    await _userManger.AddToRoleAsync(user2, "Admin");
                    await _userManger.AddToRoleAsync(user3, "Customer");


                }
                //await _context.SaveChangesAsync();

            }
        }
    }
}

