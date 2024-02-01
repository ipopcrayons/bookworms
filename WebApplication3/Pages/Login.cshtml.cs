
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using WebApplication3.Model;
using WebApplication3.ViewModels;


namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<CustomUser> signInManager;

        private readonly UserManager<CustomUser> userManager;

        public LoginModel(SignInManager<CustomUser> signInManager, UserManager<CustomUser> userManager)
		{
			this.signInManager = signInManager;
            this.userManager = userManager;

        }
		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			Debug.WriteLine(LModel.Password);
            Debug.WriteLine(ModelState.IsValid);
            Debug.WriteLine(LModel.Email);


            if (ModelState.IsValid)
			{


                var user = await userManager.FindByEmailAsync(LModel.Email);
                try
                {
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        // Account is locked
                        ModelState.AddModelError(string.Empty, "Account is locked due to too many login attempts. Please try again later.");
                        return Page();
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError(string.Empty, "Account is locked due to too many login attempts. Please try again later.");
                    return Page();
                }
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email.ToString(), LModel.Password.ToString(),
			   LModel.RememberMe, false);
                Debug.WriteLine(identityResult.Succeeded);
                if (identityResult.Succeeded)
				{
					await userManager.ResetAccessFailedCountAsync(user);

					// Upon successful login
					var sessionStartTime = DateTime.UtcNow;
                    Response.Cookies.Append("SessionStartTime", sessionStartTime.ToString(), new CookieOptions
                    {
                        HttpOnly = true, // Security measure
                        
                    });
                    

                    var atIndex = LModel.Email.IndexOf('@');
                    Response.Cookies.Append("Email", LModel.Email.Substring(0, atIndex), new CookieOptions
                    {
                        
                        HttpOnly = true, // Recommended for security (cookie not accessible via JavaScript)
                        Secure = true, // If true, the cookie is only sent over HTTPS
                        SameSite = SameSiteMode.Strict // Controls how cookies are sent with requests from other sites
                    });
                    return RedirectToPage("Index");
                }
				ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
	}
}
