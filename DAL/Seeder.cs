using DATA;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Seeder
    {
        public static async Task SeedUsers(DataContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<User>
                {
                    new User{FirstName="Roi",LastName="Shalev",UserName="roika8",Email="Roika.shalev@test.com"},
                    new User{FirstName="Omri",LastName="Shalev",UserName="omriKing",Email="ketempaz6@test.com"},
                    new User{FirstName="Amit",LastName="Elkayam",UserName="amit2500",Email="beerSheva@test.com"}
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }
        }
    }
}
