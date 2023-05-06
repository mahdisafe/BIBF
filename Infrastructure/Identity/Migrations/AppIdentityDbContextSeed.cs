using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Migrations;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManagr)
    {
        var user = new AppUser
        {
            DisplayName = "Mahdi",
            Email = "Mahdisafe@gmail.com",
            UserName = "Mahdi Safe",
            Address = new Address
            {
                FirstName = "",
                LastName = "",
                BlockNo = "",
                RoadNo = "",
                BuildingNo = "",
                City = "Hidd"
            }
        };
        await userManagr.CreateAsync(user, "Pa$$w0rd");
    }
}