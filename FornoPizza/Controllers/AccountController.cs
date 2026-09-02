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

        public AccountController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
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
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
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
                    "Обязательное поле");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!_userRepository.IsNameUniq(viewModel.Login))
            {
                ModelState.AddModelError(
                    nameof(RegisterViewModel.Login),
                    "Этот логин уже используется.");

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
                    "Не удалось создать аккаунт. Попробуйте другой логин.");
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
    }
}
