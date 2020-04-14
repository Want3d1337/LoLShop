﻿namespace LoLShop.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using LoLShop.Common;
    using LoLShop.Data.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.Response.Redirect("/");
            }

            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? "/";
            if (this.ModelState.IsValid)
            {
                if(await this.userManager.FindByNameAsync(this.Input.Username) != null)
                {
                    return this.Page();
                }
                var user = new ApplicationUser
                {
                    UserName = this.Input.Username,
                    Email = this.Input.Email,
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        public class InputModel
        {
            [Display(Name = "Username")]
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [MaxLength(AttributesConstraints.UsernameMaxLength, ErrorMessage = AttributesErrorMessages.MaxLengthErrorMessage)]
            public string Username { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(AttributesConstraints.EmailMaxLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage, MinimumLength = AttributesConstraints.EmailMinLength)]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Password")]
            [Required(ErrorMessage = AttributesErrorMessages.RequiredErrorMessage)]
            [StringLength(AttributesConstraints.PasswordMaxLength, ErrorMessage = AttributesErrorMessages.StringLengthErrorMessage, MinimumLength = AttributesConstraints.PasswordMinLength)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = AttributesErrorMessages.CompareErrorMessage)]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }
    }
}
