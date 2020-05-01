using API.Extensions;
using API.ViewModel.TokenControl;
using API.ViewModel.User;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class AuthController : MainController
    {
        /* Responsável por fazer a autenticação do usuário */
        private readonly SignInManager<IdentityUser> _signInManager;

        /* Responsável por criar o usuário */
        private readonly UserManager<IdentityUser> _userManager;

        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              INotificador notificador,
                              IUser user) : base(notificador, user)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
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
                return CustomResponse(await GerarJwt(user.Email));
            }

            foreach (var error in result.Errors)
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
                return CustomResponse(await GerarJwt(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas.");
                return CustomResponse(loginUser);
            }

            NotificarErro("Usuário ou senha incorretos.");
            return CustomResponse(loginUser);
        }

        private async Task<LoginResponseViewModel> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            /* Faz um laço nas roles (roles são muito parecidas com as claims) e adiciona na coleção de claims, mas como role */
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var responseClaims = claims.Select(c => new ClaimViewModel
            {
                Value = c.Value,
                Type = c.Type
            }).ToList();

            /* Adiciona algumas claims para uso interno */
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // ID próprio do token
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            /* Converte as claims, para o tipo aceito pelo token, que são claims do tipo do identity */
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            /* var para manipular o token */
            var tokenHandler = new JwtSecurityTokenHandler();
            /* Gera uma chave */
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            /* Cria o token */
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                /* Aqui passa as coordenadas para o jwt criar o token */
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims, //passa para o token, todas as claims do usuário
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = responseClaims
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}