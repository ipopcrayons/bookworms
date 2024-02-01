using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class CustomUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "First Name must be alphanumeric")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Last Name must be alphanumeric")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Credit Card No is required")]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Credit Card No must be 16 digits")]
        public string CreditCardNo { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile No must be 10 digits")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Billing Address is required")]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "Shipping Address is required")]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(12, ErrorMessage = "Password must be at least 12 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).*$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character")]
        [DataType(DataType.Password)]
        public  string Password { get; set; }

        [Compare("PasswordHash", ErrorMessage = "Password and confirmation password do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        
        [Display(Name = "Photo")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg)$", ErrorMessage = "Invalid file format. Only .jpg and .jpeg files are allowed.")]
        public string? PhotoPath { get; set; }
    }
}
