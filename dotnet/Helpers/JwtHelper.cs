using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtHelper {

    public static string _User = "User";
    public static string _Moderator = "Moderator";
    public static string _Admin = "Admin";


    public static string? _token;
    public JwtHelper(List<string> roles) {


        var user = string.IsNullOrEmpty(roles.FirstOrDefault(f => f == _User)) ? "" : _User;
        var moderator = string.IsNullOrEmpty(roles.FirstOrDefault(f => f == _Moderator)) ? "" : _Moderator;
        var admin = string.IsNullOrEmpty(roles.FirstOrDefault(f => f == _Admin)) ? "" : _Admin;


        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.CreateToken(
            new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(_User, user!),
                    new Claim(_Moderator, moderator!),
                    new Claim(_Admin, admin!)
                }),
                Expires = DateTime.UtcNow.AddYears(int.Parse(EnvHelper._JwtDuration!)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvHelper._JwtKey!)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            }
        );
        _token = tokenHandler.WriteToken(tokenString);

    }

    public override string ToString() => _token!;
}