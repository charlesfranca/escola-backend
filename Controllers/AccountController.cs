using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaDeVoce.Backend.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("username,password")]Services.ViewModel.UserViewModel model){
            Console.Write(model);
            AuthenticationModel response = null;
            
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            if (model.Id != Guid.Empty) method = System.Net.Http.HttpMethod.Put;

            response = await ApiRequestHelper.postPutEncodedRequest<AuthenticationModel>(
                EscolaDeVoce.Backend.Helpers.EscolaDeVoceEndpoints.tokenUrl,
                model.username,
                model.password
            );

            if(response != null){
                const string Issuer = "https://www.escoladevoce.com.br";
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "Charles", ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Surname, "França", ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Country, "BR", ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Country, "BR", ClaimValueTypes.String, Issuer),
                    new Claim("TOKEN", response.access_token, ClaimValueTypes.String, Issuer),
                    new Claim("facebookid", "112345432145432", ClaimValueTypes.String, Issuer),
                    new Claim("id", Guid.NewGuid().ToString(), ClaimValueTypes.String)
                };

                var userIdentity = new ClaimsIdentity(claims, "Passport");
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = true,
                        AllowRefresh = false
                    });

                return RedirectToAction("Index", "Project");
            }

            Console.Write(response);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToActionPermanent("Login", "Account");
        }
        
        public IActionResult Signup(){
            return View();
        }
    }
}