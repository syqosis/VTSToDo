using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using VTSToDo.Shared.Extensions;

namespace VTSToDo.DAL
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ToDoContext(serviceProvider.GetRequiredService<DbContextOptions<ToDoContext>>()))
            {
                context.Users.Add(new User
                {
                    UserName = "test",
                    Password = CryptographyExtensions.ComputeHash("test123")
                });

                context.SaveChanges();
            }
        }
    }
}
