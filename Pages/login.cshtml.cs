using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IskoWalk.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace IskoWalk.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ApplicationDbContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = "Please fill in all fields.";
                    return Page();
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == Email);

                if (user == null)
                {
                    ErrorMessage = "Invalid email or password.";
                    return Page();
                }

                if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
                {
                    ErrorMessage = "Invalid email or password.";
                    return Page();
                }

                // Login successful - redirect to dashboard
                return RedirectToPage("/dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error");
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}
