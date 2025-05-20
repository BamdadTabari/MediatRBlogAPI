using DataLayer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenService
{
    private readonly SecurityTokenConfig _configuration;

    public JwtTokenService(SecurityTokenConfig configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecuritysecaeasdDdw23294913129-123-1wqndlqndwoeoqiy931q429y19eiojwoo13Token"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "WQ7+dPhLEHdhdaKNzu!ck-fg86TPhUfd#E&&Qq+=vUtfxJ!@sDfe#u^prXW2&Qhmy33u!@e?5-xb*",
            audience: "WQ7+dPhLEHdhdaKNzu!ck-fg86TPhUfd#E&&Qq+=vUtfxJ!@sDfe#u^prXW2&Qhmy33u!@e?5-xb*",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserIdFromClaims(ClaimsPrincipal user)
    {
        // تلاش برای پیدا کردن claim با نوع "NameIdentifier" که معمولا به عنوان userId شناخته می‌شود
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim?.Value; // بازگرداندن userId به صورت رشته یا null اگر پیدا نشد
    }

    public int GetTokenExpiryMinutes(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            throw new ArgumentException("Invalid token");

        // یافتن claim تاریخ انقضا (exp)
        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

        if (expClaim == null)
            throw new ArgumentException("Token does not contain an expiration claim");

        // تبدیل مقدار exp به DateTime
        var expDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value)).UtcDateTime;

        // محاسبه زمان باقی‌مانده
        var remainingMinutes = (int)(expDateTime - DateTime.UtcNow).TotalMinutes;

        return remainingMinutes > 0 ? remainingMinutes : 0; // اطمینان از اینکه مقدار منفی برنگردد
    }

}
