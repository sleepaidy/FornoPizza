using FornoPizza.Localization;
using FornoPizza.Middleware;
using FornoPizza.Data.Enums;
using FornoPizza.Data.Models;
using FornoPizza.Data.Repository.Interfaces;
using FornoPizza.Models.Auth;
using FornoPizza.Services;
using FornoPizza.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FornoPizza.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public AccountController(
            IUserRepository userRepository,
            IAuthService authService,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var login = viewModel.Login?.Trim() ?? "";
            var user = _userRepository.GetByNameAndPassword(login, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, Auth.Error_InvalidCredentials);
                return View(viewModel);
            }

            await _authService.SignInAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel viewModel)
        {
            viewModel.Login = viewModel.Login?.Trim() ?? "";

            if (string.IsNullOrEmpty(viewModel.Login))
            {
                ModelState.Remove(nameof(RegisterViewModel.Login));
                ModelState.AddModelError(
                    nameof(RegisterViewModel.Login),
                    Auth.Error_Required);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!_userRepository.IsNameUniq(viewModel.Login))
            {
                ModelState.AddModelError(
                    nameof(RegisterViewModel.Login),
                    Auth.Error_LoginTaken);

                return View(viewModel);
            }

            var user = new UserData
            {
                Name = viewModel.Login,
                Password = viewModel.Password,
                Role = Role.User
            };

            try
            {
                _userRepository.Registration(user);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    nameof(RegisterViewModel.Login),
                    Auth.Error_CreateFailed);
                return View(viewModel);
            }

            await _authService.SignInAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(AuthService.AUTH_KEY);
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> CreateKitchen()
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var kitchenName = _configuration["Kitchen:Name"];
            var kitchenPass = _configuration["Kitchen:Password"];
            if (string.IsNullOrWhiteSpace(kitchenName) || string.IsNullOrWhiteSpace(kitchenPass))
            {
                return BadRequest(Auth.Error_KitchenNotConfigured);
            }

            if (!_userRepository.IsNameUniq(kitchenName))
            {
                var user = _userRepository.GetByNameAndPassword(kitchenName, kitchenPass);
                if (user == null)
                {
                    return BadRequest(Auth.Error_KitchenPassword);
                }

                await _authService.SignInAsync(user);
                return RedirectToAction("Index", "Kitchen");
            }
            var userData = new UserData
            {
                Name = kitchenName,
                Password = kitchenPass,
                Role = Role.Kitchen
            };
            _userRepository.Registration(userData);
            await _authService.SignInAsync(userData);
            return RedirectToAction("Index", "Kitchen");

        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string? returnUrl)
        {
            if (culture is not ("ru" or "en"))
            {
                culture = "ru";
            }

            Response.Cookies.Append(LocalizationMiddleware.CookieName, culture, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return LocalRedirect(returnUrl);
        }
    }
}
