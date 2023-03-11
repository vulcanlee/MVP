using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtExpire
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "pY2THq2XTfuRHUUorBG4bCbuM6ddbo2MruFq4Pf2BRFAQ1RENt6e7iPwQTqBJqEzAo24E9J5AhimLTjNUKYDPT2nTVJiRAUuRaxf";
            var claims = new List<Claim>()
            {
                new Claim("sid", "userid"),
            };
            //DateTime now = new DateTime(2023, 03, 10, 16, 0, 0);
            DateTime now = DateTime.Now.AddHours(1);
            DateTime nowUTC = DateTime.UtcNow.AddHours(1);

            GeneratorJwtSecurityToken(key, claims, now);
            GenerateSecurityTokenDescriptor(key, now);
            GenerateSecurityTokenDescriptor(key, nowUTC);
        }

        private static void GeneratorJwtSecurityToken(string key, List<Claim> claims, DateTime now)
        {
            #region JwtSecurityToken
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                issuer: "http://localhost",
                audience: "http://localhost",
                claims: claims,
                expires: now,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(key)),
                        SecurityAlgorithms.HmacSha512)
            );

            string tokenStringJwtSecurityToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            Console.WriteLine(tokenStringJwtSecurityToken);
            Console.WriteLine();
            #endregion
        }

        private static void GenerateSecurityTokenDescriptor(string key, DateTime now)
        {
            #region SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://localhost",
                Audience = "http://localhost",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sid", "userid"),
                }),
                Expires = now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(key)),
                        SecurityAlgorithms.HmacSha512)
            };

            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var tokenStringSecurityTokenDescriptor = jwtTokenHandler.WriteToken(token);
            Console.WriteLine(tokenStringSecurityTokenDescriptor);
            Console.WriteLine();
            #endregion
        }
    }
}