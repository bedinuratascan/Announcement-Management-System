using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnnouncApp.Domain.Services;
using AnnouncApp.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnnouncApp.Extensions;
using AnnouncApp.Domain.Responses;

namespace AnnouncApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        
        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        
        [HttpPost]
        public IActionResult AccessToken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                AccessTokenResponse accessTokenResponse=authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);
                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.accesstoken);
                }
                else
                {
                    return BadRequest(accessTokenResponse.Message); 
                }
            }
        }
        
        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            AccessTokenResponse accessTokenResponse = authenticationService.CerateAccessTokenByRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accesstoken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }
        
        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource)
        {
            AccessTokenResponse accessTokenResponse = authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accesstoken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }
    }
}