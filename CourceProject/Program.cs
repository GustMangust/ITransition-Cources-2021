using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CourceProject {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
            /*var scope = host.Services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            var userManager = new UserManager<IdentityUser>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            ctx.Database.EnsureCreated();
            var adminRole = new IdentityRole("Admin");
            if(!ctx.Roles.Any()) {
              roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }
            if(!ctx.Users.Any(x => x.UserName == "admin")) {
              IdentityUser adminUser = new IdentityUser {
                UserName = "admin",
                Email = "admin@admin.com"
              };
              userManager.CreateAsync(adminRole, "admin");
              userManager.CreateAsync(adminUser);
            }*/
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
