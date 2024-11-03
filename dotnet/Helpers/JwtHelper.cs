using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtHelper {

    public static string? _token;
    public JwtHelper(string id) {

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.CreateToken(
            new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, id)
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
    public static IDictionary<string, string> Decode(string token) {
        if (string.IsNullOrEmpty(token)) {
            return new Dictionary<string, string> {
                { "Error", "No token available to decode." }
            };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claimsDictionary = new Dictionary<string, string>();
        foreach (var claim in jwtToken.Claims) {
            claimsDictionary[claim.Type] = claim.Value;
        }

        return claimsDictionary;
    }

}