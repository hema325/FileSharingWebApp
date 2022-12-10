using AutoMapper;
using FileSharingApp.Areas.Identity.Data;
using FileSharingApp.Areas.Identity.Models;
using FileSharingApp.Constraints;
using FileSharingApp.Resources;
using FileSharingApp.Services.Mail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Net.Mail;
using System.Security.Claims;

namespace FileSharingApp.Areas.Identity.Controllers
{
    [Area(AreaNames.Identity)]
    [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.User}")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IStringLocalizer<SharedResources> stringLocalizer;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IMapper mapper, IEmailSender emailSender, IStringLocalizer<SharedResources> stringLocalizer, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.stringLocalizer = stringLocalizer;
            this.emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl, bool isPersistent)
        {
            var blocked = false;
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (!user.IsBlocked)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent, true);
                    if (result.Succeeded)
                    {
                        TempData["Message"] = stringLocalizer["Succeeded"].Value;
                        if (!string.IsNullOrEmpty(returnUrl))
                            return LocalRedirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home", new { area = AreaNames.Users });
                    }
                    else if (result.IsNotAllowed)
                    {
                        if (await userManager.CheckPasswordAsync(user, model.Password))
                            return View(nameof(ConfirmEmail), user.Id);
                    }
                }
                else
                {
                    blocked = true;
                    TempData["Message"] = "You are Blocked";
                }

            }
            if (!blocked)
            {
                TempData["Message"] = stringLocalizer["Failed"].Value;
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email, string verificationMethod)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var url = Url.Action("ResetPassword", "Account", new { Token = token, UserId = user.Id }, Request.Scheme);

                if (verificationMethod == "email")
                {
                    var msg = new MailMessage
                    {
                        Subject = "Reset Password",
                        Body = $"Please <a href=\"{url}\">Verify</a> Your Email To Change Your Password",
                        IsBodyHtml = true
                    };

                    msg.To.Add(user.Email);

                    await emailSender.SendEmailAsync(msg);

                    return View(nameof(ConfirmEmail));
                }
                else if (verificationMethod == "phoneNumber")
                {
                    //not included Yet
                }
            }

            TempData["Message"] = stringLocalizer["Failed"].Value;
            return View(nameof(ForgotPassword));

        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword(string Token, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user is not null)
            {
                var isValidToken = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "ResetPassword", Token);
                if (isValidToken)
                    return View();
            }
            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword(AddPasswordViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user is not null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = stringLocalizer["Succeeded"].Value;
                    return RedirectToAction(nameof(Login));
                }
                TempData["Message"] = stringLocalizer["Failed"].Value;
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl, bool isPersistent)
        {
            var redirectUrl = $"Identity/Account/ExternalResponse?isPersistent={isPersistent}&&returnUrl={returnUrl}";
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalResponse(string returnUrl, bool isPersistent)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            if (info is not null)
            {
                var tryGetUser = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (tryGetUser is not null)
                {
                    if (!tryGetUser.IsBlocked)
                    {
                        var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent);
                        if (!signInResult.Succeeded)
                        {
                            TempData["Message"] = stringLocalizer["Succeeded"].Value;
                        }
                    }
                    else
                    {
                        TempData["Message"] = "You are Blocked";
                        return RedirectToAction(nameof(Login));
                    }
                }

                var user = new ApplicationUser
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    var loginResult = await userManager.AddLoginAsync(user, info);
                    if (loginResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent);
                    }
                    else
                    {
                        await userManager.DeleteAsync(user);
                        TempData["Message"] = stringLocalizer["Failed"].Value;
                        return RedirectToAction(nameof(Login));
                    }
                }


            }

            if (!string.IsNullOrEmpty(returnUrl))
                return LocalRedirect(returnUrl);
            else
                return RedirectToAction(nameof(Index), "Home", new { area = AreaNames.Users });

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            var registerViewModel = new RegisterViewModel()
            {
                RolesList = roleManager.Roles.Select(identityRole => new SelectListItem { Text = identityRole.Name, Value = identityRole.Name })
            };
            return View(registerViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<ApplicationUser>(model);

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Role is null)
                    {
                        await userManager.AddToRoleAsync(user, IdentityRoles.User);
                    }
                    else
                    {
                        if (await roleManager.RoleExistsAsync(model.Role))
                        {
                            await userManager.AddToRoleAsync(user, model.Role);
                        }
                        else
                        {
                            await userManager.DeleteAsync(user);
                            TempData["Message"] = "Failed";
                            return View(model);
                        }
                    }
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var url = Url.Action("ConfirmEmail", "Account", new { Token = token, UserId = user.Id }, Request.Scheme);

                    var msg = new MailMessage
                    {
                        Subject = "Email Confirmation",
                        Body = $"Pleasse Click Here To Confirm Your Email <a href=\"{url}\" >Confirm</a>",
                        IsBodyHtml = true
                    };

                    msg.To.Add(user.Email);

                    await emailSender.SendEmailAsync(msg);
                    return View(nameof(ConfirmEmail), user.Id);
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }

            TempData["Message"] = stringLocalizer["Failed"].Value;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string Token, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user is not null)
            {
                var result = await userManager.ConfirmEmailAsync(user, Token);
                if (result.Succeeded)
                {
                    TempData["Message"] = stringLocalizer["Succeeded"].Value;
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home", new { area = AreaNames.Users });
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View();
            }

            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendConfirmationMessageAgain(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            if (user is not null)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { Token = token, UserId = user.Id }, Request.Scheme);

                var msg = new MailMessage
                {
                    Subject = "Email Confirmation",
                    Body = $"Pleasse Click Here To Confirm Your Email <a href=\"{url}\" >Confirm</a>",
                    IsBodyHtml = true
                };

                msg.To.Add(user.Email);

                await emailSender.SendEmailAsync(msg);

                TempData["Message"] = stringLocalizer["Succeeded"].Value;
                return View(nameof(ConfirmEmail), UserId);
            }

            return NotFound();

        }

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Message"] = stringLocalizer["Succeeded"].Value;
            return RedirectToAction("Index", "home", new { area = AreaNames.Users });
        }

        [HttpGet]

        public async Task<IActionResult> UserInformation()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            return View(mapper.Map<RegisterViewModel>(user));
        }

        [HttpGet]
        public async Task<IActionResult> ManageAccount()
        {
            var user = await userManager.GetUserAsync(User);
            return View(mapper.Map<ChangeUserInformationViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInformation(ChangeUserInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var result = await userManager.UpdateAsync(mapper.Map(model, user));
                if (result.Succeeded)
                {
                    TempData["Message"] = stringLocalizer["Succeeded"].Value;
                    return RedirectToAction(nameof(UserInformation));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["Message"] = stringLocalizer["Succeeded"].Value;
                    await signInManager.SignOutAsync();
                    return RedirectToAction(nameof(Login));
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(nameof(ManageAccount));
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var result = await userManager.AddPasswordAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = stringLocalizer["Succeeded"].Value;
                    await signInManager.SignOutAsync();
                    return RedirectToAction(nameof(Login));
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(nameof(ManageAccount), model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = stringLocalizer["Succeeded"].Value;
                await signInManager.SignOutAsync();
                return RedirectToAction(nameof(Index), "Home", new { area = AreaNames.Users });
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            TempData["Message"] = stringLocalizer["Failed"].Value;
            return View(nameof(ManageAccount));
        }

    }
}
