
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Entities;



namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var users = JsonSerializer.Deserialize<List<SeedUserDTO>>(userData, options);

        if (users == null)
        {
            Console.WriteLine("No users found in the seed data.");
            return;
        }

        using var hmac = new HMACSHA512();
        foreach (var user in users)
        {
            var member = new AppUser
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$wOrd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = user.Id,
                    DisplayName = user.DisplayName,
                    Description = user.Description,
                    DateofBirth = user.DateofBirth,
                    ImageUrl = user.ImageUrl,
                    Gender = user.Gender,
                    City = user.City,
                    Country = user.Country,
                    LastActive = user.LastActive,
                    Created = user.Created
                }
            };
            member.Member.Photos.Add(new Photo
            {
                Url = user.ImageUrl,
                MemberId = user.Id,
            });
            context.Users.Add(member);
        }
        await context.SaveChangesAsync();
    }
}
