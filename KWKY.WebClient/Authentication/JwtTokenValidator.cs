using Microsoft.IdentityModel.Tokens;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KWKY.WebClient.Authentication
{
    /// <summary>
    /// JWT token 认证
    /// </summary>
    public class JwtTokenValidator : ISecurityTokenValidator
    {
        private readonly ILogger _logger;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public JwtTokenValidator (JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _logger = LogManager.GetCurrentClassLogger();
        }
        bool ISecurityTokenValidator.CanValidateToken => true;

        int ISecurityTokenValidator.MaximumTokenSizeInBytes { get; set; }

        bool ISecurityTokenValidator.CanReadToken (string securityToken)
        {
            return true;
        }

        //验证token
        ClaimsPrincipal ISecurityTokenValidator.ValidateToken (string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal ret = null;
            validatedToken = null;
            if ( string.IsNullOrEmpty(securityToken) || !_jwtSecurityTokenHandler.CanReadToken(securityToken) )
            {
                return null;
            }
            try
            {
                ret = _jwtSecurityTokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);

                //ret.FindFirst("GId")?.Value
            }
            catch ( Exception ex )
            {
                _logger.Error(ex);
            }
            return ret;
        }
    }
}
