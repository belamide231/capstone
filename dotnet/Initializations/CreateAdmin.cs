using dotenv.net;
using Microsoft.AspNetCore.Identity;

public class CreateAdmin {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly string _email;
    private readonly string _password;

    public CreateAdmin(UserManager<ApplicationUser> userManager, Mongo mongo) {

        DotEnv.Load();

        _userManager = userManager;
        _email = Environment.GetEnvironmentVariable("EMAIL")!;
        _password = Environment.GetEnvironmentVariable("PASSWORD")!;
    }

    public async Task Create() {

        var user = await _userManager.FindByEmailAsync(_email);
        if (user == null) {

            user = new ApplicationUser(_email);
            var result = await _userManager.CreateAsync(user);

            if(result.Succeeded) {

                user.Roles = ["admin"];
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, _password);
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
