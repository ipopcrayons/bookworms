using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using WebApplication3.Model;
using System.Text;
using System.Diagnostics;
using reCAPTCHA.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.DataProtection;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {
        
        private UserManager<CustomUser> userManager { get; }
        private SignInManager<CustomUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            

        }





        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("abc");


                Debug.WriteLine(RModel.PhotoPath);
                Debug.WriteLine("444");


                var user = new CustomUser()
                {

                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    CreditCardNo = protector.Protect(RModel.CreditCardNo),
                    MobileNo = RModel.MobileNo,
                    BillingAddress = RModel.BillingAddress,
                    ShippingAddress = RModel.ShippingAddress,
                    PhotoPath = RModel.PhotoPath,
                    Password = protector.Protect(RModel.Password),
                    ConfirmPassword = RModel.Password,
                    PasswordHash = RModel.Password,



                };
                Debug.WriteLine(user.Email);

                var result = await userManager.CreateAsync(user, RModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    var atIndex = RModel.Email.IndexOf('@');
                    var sessionStartTime = DateTime.UtcNow;

                    Response.Cookies.Append("SessionStartTime", sessionStartTime.ToString(), new CookieOptions
                    {
                        HttpOnly = true, // Security measure

                    });
                    Response.Cookies.Append("Email", RModel.Email.Substring(0, atIndex), new CookieOptions
                    {

                        HttpOnly = true, // Recommended for security (cookie not accessible via JavaScript)
                        Secure = true, // If true, the cookie is only sent over HTTPS
                        SameSite = SameSiteMode.Strict // Controls how cookies are sent with requests from other sites
                    });
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }
            else
            {
                return Page();
            }
        }






    }
}
