using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IskoWalk.Data;
using IskoWalk.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace IskoWalk.Pages
{
    public class SignupModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SignupModel> _logger;

        public SignupModel(ApplicationDbContext context, ILogger<SignupModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string FullName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "Please fill in all fields.";
                    return Page();
                }

                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Passwords do not match.";
                    return Page();
                }

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == Email);

                if (existingUser != null)
                {
                    ErrorMessage = "An account with this email already exists.";
                    return Page();
                }

                var user = new User
                {
                    FullName = FullName,
                    Email = Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                SuccessMessage = "Account created successfully! You can now login.";
                
                // Clear form
                FullName = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
