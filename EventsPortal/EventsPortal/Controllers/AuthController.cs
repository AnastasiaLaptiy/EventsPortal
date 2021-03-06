﻿using EventsPortal.GoogleAuthModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventsPortal.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ActionResult> Authenticate(string email)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(
                        new ClaimsIdentity(claims, "Cookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            try
            {
                Response.Cookies.Delete("Cookies");
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> GoogleAuth(GoogleUser googleUser)
        {
            try
            {
                var userList = _userService.GetUsers();

                foreach (var user in userList)
                {
                    if (user.Email == googleUser.email)
                    {
                        await Authenticate(googleUser.email);
                        return new Response { UserEmail = user.Email };
                    }
                }
                var userDTO = new UserDTO()
                {
                    GoogleId = googleUser.id,
                    Email = googleUser.email,
                    IdToken = googleUser.idToken,
                    Image = googleUser.image,
                    Name = googleUser.name,
                    Provider = googleUser.provider,
                    Token = googleUser.token
                };
                await _userService.AddUser(userDTO);
                await Authenticate(googleUser.email);
                return new Response { UserEmail = googleUser.email };
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
