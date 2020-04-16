using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModel.User;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        /* Responsável por fazer a autenticação do usuário */
        private readonly SignInManager<IdentityUser> _signInManager;

        /* Responsável por criar o usuário */
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              INotificador notificador): base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                /* Esta propriedade é para quando se registrar o identity enviar um email, solicitando que o usuário confirme pelo email */
                /* setando para true, irá dizer que ao registrar, não precisa enviar email de confirmação */
                EmailConfirmed = true
            };

            /* Cria o usuário e analisa o resultado da criação */
            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                /* Se criou o usuário com sucesso, então, já registra o mesmo */
                await _signInManager.SignInAsync(user, false); //o false é se precisa persistir o login, se precisa lembrar do usuario em logins futuros
                return CustomResponse(registerUser);
            }

            foreach(var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(loginUser);
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas.");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(loginUser);
        }
    }
}