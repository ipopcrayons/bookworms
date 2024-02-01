using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
	[Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<CustomUser> userManager;
        public IndexModel(UserManager<CustomUser> userManager)
        {
            this.userManager = userManager;
        }
        public CustomUser NewUser { get; set; }
        public string unprotectedpassword { get; set; }
        public string unprotectedCard { get; set; }




        public async Task<IActionResult> OnGetAsync()
        {
            
                
                if (Request.Cookies.TryGetValue("Email", out string userEmail))
                {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("abc");
                userEmail = userEmail + "@gmail.com";
                    Debug.WriteLine(userEmail);
                    NewUser = await userManager.FindByEmailAsync(userEmail);
                    unprotectedpassword = protector.Unprotect(NewUser.Password);
                    Debug.WriteLine(unprotectedpassword);
                    unprotectedCard = protector.Unprotect(NewUser.CreditCardNo);
            }
                
                

                if (NewUser == null)
                {
                    // Handle the case where the user isn't found
                }
            

            return Page();
        }
    }
}