using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.Infrastructure.Security;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Infrastructure.Data
{

    /// <summary>
    /// DEV ONLY - Seed DB with Users and their hashed passwords (ONLY WHEN NO USERS IN DB)
    /// </summary>
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext db, PasswordHasher hasher)
        {
            // Ensure database is created
            await db.Database.EnsureCreatedAsync();

            if (db.User.Any()) return;

            var officerID = Guid.NewGuid();

            db.User.AddRange(
                new User
                {
                    Username = "admin",
                    Role = "Admin",
                    PasswordHash = hasher.Hash("Admin123!"),
                    IsActive = true
                },
                new User
                {
                    Id = officerID,
                    Username = "officer",
                    Role = "Officer",
                    PasswordHash = hasher.Hash("Officer123!"),
                    IsActive = true
                },
                new User
                {
                    Username = "auditor",
                    Role = "Auditor",
                    PasswordHash = hasher.Hash("Auditor123!"),
                    IsActive = true
                }
            );


            var client = db.Client.Add(new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Omar",
                LastName = "Amr",
                Region = "US",
                Status = "Available",
                SSN = "123-22-4567",
                DateOfBirth = DateTime.Now,
                Phone = "804-310-7650",
                Address = "1600 Pennsylvania Ave NW, Washington, DC 20500"
            });

            var officer = db.Officer.Add(new Officer
            {
                Id = Guid.NewGuid(),
                UserId = officerID,
                FirstName = "Noah",
                LastName = "Wilson",
                Region = "US",
                CreatedAt = DateTime.Now
            });

            var sp = db.ServiceProvider.Add(new ServiceProvider
            {
                Id = Guid.NewGuid(),
                Name = "Richmond Health Services",
                Region = "US-Virginia",
                ServiceType = "Health",
                CreatedAt = DateTime.Now
            });

            db.Case.Add(new Case
            {
                Id = Guid.NewGuid(),
                ClientId = client.Entity.Id,
                AssignedOfficerId = officer.Entity.Id,
                ServiceProviderId = sp.Entity.Id,
                Status = "New",
                Region = "US-Virginia",
                Recommendations = "TESTING",
                CreatedAt = DateTime.Now
            });

            await db.SaveChangesAsync();
        }
    }
}