using BulgarianHistory.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BulgarianHistory.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Сийдване на роли и админ потребител
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // 1. Създаваме роля Admin, ако не съществува
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Проверяваме дали съществува админ потребител
            var adminEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                // 3. Създаваме нов админ потребител
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    // 4. Добавяме към Admin роля
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Не успях да създам админ потребителя: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            // 5. Сийд на останалите данни (твоите епохи, градове, събития и т.н.)
            if (!await context.Eras.AnyAsync())
            {
                await context.Eras.AddRangeAsync(
                    new Era { Name = "First Bulgarian Empire", Description = "The first Bulgarian state (681-1018).", ImageUrl = "https://i.ibb.co/ynLS96Wk/fbe.jpg" },
                    new Era { Name = "Second Bulgarian Empire", Description = "The second Bulgarian state (1185-1396).", ImageUrl = "https://www.bulgariantimes.co.uk/wp-content/uploads/2023/05/1_29_09_18_6_19_35.jpeg" }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Cities.AnyAsync())
            {
                await context.Cities.AddRangeAsync(
                    new City { Name = "Pliska", Description = "First capital of Bulgaria.", ImageUrl = "https://i.ibb.co/GQdjJcJm/pliska.jpg", Latitude = 43.3764, Longitude = 27.1292 },
                    new City { Name = "Veliko Tarnovo", Description = "Capital of the Second Bulgarian Empire.", ImageUrl = "https://i.ibb.co/1Y53BSX9/tarnovo.jpg", Latitude = 43.0757, Longitude = 25.6172 }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Events.AnyAsync())
            {
                await context.Events.AddRangeAsync(
                    new Event { Name = "Battle of Ongal", Description = "Victory of Khan Asparuh over the Byzantine Empire.", ImageUrl = "https://byzantium.gr/imgbattle/asparuh.jpg", Year = 680, EraId = 1 },
                    new Event { Name = "Battle of Klokotnitsa", Description = "Victory of Tsar Ivan Asen II over the Despotate of Epirus.", ImageUrl = "https://byzantium.gr/imgbattle/klokonitsa.jpg", Year = 1230, EraId = 2 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}