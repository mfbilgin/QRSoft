using Core.Utilities.Security.Encryption;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Extentions;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Security.Jwt
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        //Şirket için JWT(JSON Web Token Oluşturur. Bkz. jwt.io)
        public AccessToken CreateToken(Company company, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, company, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        //Token opionsda yer alan bilgileri kullanarak bir token oluşturur.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Company company,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(company, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }
                //Token'a ilgili Claim'i(rol) ekler.
                private IEnumerable<Claim> SetClaims(Company company, List<OperationClaim> operationClaims)
                {
                    var claims = new List<Claim>();
                    claims.AddNameIdentifier(company.Id.ToString());
                    claims.AddEmail(company.MailAddress);
                    claims.AddName($"{company.CompanyName}");
                    claims.AddRoles(operationClaims.Select(oc => oc.OperationClaimName).ToArray());
        
                    return claims;
                }
    }
}
