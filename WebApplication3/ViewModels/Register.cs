using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "First Name must be alphanumeric")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "First Name must be alphanumeric")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Credit Card No is required")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card No must be 16 digits")]
        public string CreditCardNo { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile No must be 10 digits")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Billing Address is required")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "Shipping Address is required")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The Password must be at least 12 characters long.", MinimumLength = 12)]
        [DataType("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match")]
        [DataType("Password")]
        public string ConfirmPassword { get; set; }

        
        [RegularExpression(@"^.*\.(jpg|JPG)$", ErrorMessage = "Only .jpg files are allowed")]
        public string? PhotoPath { get; set; }
    }
}
