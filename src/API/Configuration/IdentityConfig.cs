using API.Data;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            /* Configuração do contexto do identity */
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            /* As classes passadas (exemplo IdentityUser, Role) são classes que podem ser manipuladas e você pode alterar o funcionamento do identity */
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>() //aqui é para dizer pro identity que voce esta trabalhando com Entity, e o contexto é esse
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            var appSettingsSection = configuration.GetSection("AppSettings");
            /* Aqui esta dizendo para o .net core, que a classe AppSettings, representa um trecho do AppSettings.json, e quando injetar ela na app, */
            /* já irá vir com as informações */
            services.Configure<AppSettings>(appSettingsSection);

            /* Aqui pega no var, os dados da classe configurada acima (já com as informações do json) */
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            /* Abaixo diz, que toda vez que for autenticar alguem, o padrão é para gerar um token */
            /* e toda vez que for valida, (chalenge), também será via token */
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                /* Se garantir que só vai usar https, pode usar true */
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    /* Vai validar se quem esta emitindo tem que ser o mesmo do token, de acordo com a chave */
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    /* Valida apenas o Issuer apenas com o nome */
                    ValidateIssuer = true,
                    ValidateAudience = true,

                    /* Diz qual a audiencia e qual o issuer (estas informações tem que vir no token) */
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });

            return services;
        }
    }
}
