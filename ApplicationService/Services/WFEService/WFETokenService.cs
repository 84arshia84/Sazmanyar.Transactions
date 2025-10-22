using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApplicationService.Services.WFEService
{
    public class WFETokenService
    {
        private readonly IConfiguration _configuration;
        public WFETokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> ExtractFullQualifyNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);
            var fullQualifyName = claims["sub"];
            return fullQualifyName;
        }
        public async Task<string> GetWFEInvoiceToken(string fullQualifyName)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, fullQualifyName);
            claims.Add("moduleId", _configuration["WFESettings:invoiceModuleId"]);
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WFESettings:secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime ExpiresIn = DateTime.Now.AddMinutes(double.Parse(_configuration["WFESettings:jwtTokenValidityInMinutes"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                Issuer = _configuration["WFESettings:validIssuer"],
                Audience = _configuration["WFESettings:validAudience"],
                Expires = ExpiresIn,
                SigningCredentials = signIn
            };
            string token = new JwtSecurityTokenHandler().CreateEncodedJwt(securityTokenDescriptor);
            return token;
        }
        public async Task<string> GetWFEContractToken(string fullQualifyName)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, fullQualifyName);
            claims.Add("moduleId", _configuration["WFESettings:contractModuleId"]);
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WFESettings:secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime ExpiresIn = DateTime.Now.AddMinutes(double.Parse(_configuration["WFESettings:jwtTokenValidityInMinutes"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                Issuer = _configuration["WFESettings:validIssuer"],
                Audience = _configuration["WFESettings:validAudience"],
                Expires = ExpiresIn,
                SigningCredentials = signIn
            };
            string token = new JwtSecurityTokenHandler().CreateEncodedJwt(securityTokenDescriptor);
            return token;
        }


        public async Task<string> GetWFEFactorToken(string fullQualifyName)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, fullQualifyName);
            claims.Add("moduleId", _configuration["WFESettings:factorModuleId"]);
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WFESettings:secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime ExpiresIn = DateTime.Now.AddMinutes(double.Parse(_configuration["WFESettings:jwtTokenValidityInMinutes"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                Issuer = _configuration["WFESettings:validIssuer"],
                Audience = _configuration["WFESettings:validAudience"],
                Expires = ExpiresIn,
                SigningCredentials = signIn
            };
            string token = new JwtSecurityTokenHandler().CreateEncodedJwt(securityTokenDescriptor);
            return token;
        }

        public async Task<string> GetWFEContractAddendumToken(string fullQualifyName)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, fullQualifyName);
            claims.Add("moduleId", _configuration["WFESettings:contractAddendumModuleId"]);
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WFESettings:secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime ExpiresIn = DateTime.Now.AddMinutes(double.Parse(_configuration["WFESettings:jwtTokenValidityInMinutes"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                Issuer = _configuration["WFESettings:validIssuer"],
                Audience = _configuration["WFESettings:validAudience"],
                Expires = ExpiresIn,
                SigningCredentials = signIn
            };
            string token = new JwtSecurityTokenHandler().CreateEncodedJwt(securityTokenDescriptor);
            return token;
        }
        public async Task<string> GetWFETransactionToken(string fullQualifyName)
        {
            var claims = new Dictionary<string, object>();
            claims.Add(JwtRegisteredClaimNames.Sub, fullQualifyName);
            claims.Add("moduleId", _configuration["WFESettings:transactionExecutionRequestModuleId"]);
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["WFESettings:secret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime ExpiresIn = DateTime.Now.AddMinutes(double.Parse(_configuration["WFESettings:jwtTokenValidityInMinutes"]));
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Claims = claims,
                Issuer = _configuration["WFESettings:validIssuer"],
                Audience = _configuration["WFESettings:validAudience"],
                Expires = ExpiresIn,
                SigningCredentials = signIn
            };
            string token = new JwtSecurityTokenHandler().CreateEncodedJwt(securityTokenDescriptor);
            return token;
        }
    }
}
