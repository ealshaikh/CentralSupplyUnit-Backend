namespace CSU_Infra.Service
{
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using CSU_Core.Service;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class LoginService : I_LoginServicecs
    {
        private readonly I_LoginRepository _loginRepository;

        private readonly IUserRepository _userRepository;

        public LoginService(I_LoginRepository loginRepository, IUserRepository userRepository)
        {
            this._loginRepository = loginRepository;
            this._userRepository = userRepository;
        }

        public string UserLogin(User login)
        {
            var result = _loginRepository.UserLogin(login);
            if (result == null)
            {
                return null;
            }
            else
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                string roleName = _userRepository.GetRoleName(result.Roleid).GetAwaiter().GetResult();

                var claims = new List<Claim>
                {
                    new Claim("User Id", result.Userid.ToString()),
                    new Claim("Full Name", result.Firstname + ' '+ result.Lastname),
                    new Claim("Email", result.Email),
                    new Claim("Role Id", result.Roleid.ToString()),
                    new Claim("Role Name", roleName)
                };

                var tokenOptions = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: signingCredentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return token;
            }
        }
    }
}
