using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; //Se não tiver versão, utiliza a default
                options.DefaultApiVersion = new ApiVersion(1, 0); // e a versão default quando não tiver definida, é 1.0
                options.ReportApiVersions = true; //Isso para passar no header do response, se a API esta ok, ou obsoleta, se tem versão mais recente
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; //nomenclatura de versionamento, vai ficar v e depois até 3 parametros, exemplo v1.0.0
                options.SubstituteApiVersionInUrl = true; //se tiver uma rota padrão, substitui o numero da versão pela default por exemplo
            });

            /* Para não retornar erro do ModelState, e sim os erros personalidados */
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddCors(options =>
            {
                /* Adicionou uma politica de CORS, para a app receber qualquer chamada externa, isso apenas para desenvolvimento */
                options.AddPolicy("Development",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

                /* Adicionou uma politica de CORS, para acesso externo na APP, para produção */
                options.AddPolicy("Production",
                    builder => builder
                    .WithMethods("GET")
                    .WithOrigins("http://qualquer.com") //somente este site pode fazer get na app
                    .SetIsOriginAllowedToAllowWildcardSubdomains());

            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfig(this IApplicationBuilder app)
        {
            /* Caso a chamada seja Http, força ser Https */
            app.UseHttpsRedirection();
            app.UseMvc();

            return app;
        }
    }
}
