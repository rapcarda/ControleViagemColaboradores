using API.Configuration;
using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityConfiguration(Configuration);

            /* o typeof do parâmetro é para dizer para o auto mapper que tudo que vier do Startup ele irá resolver */
            services.AddAutoMapper(typeof(Startup));
            services.WebApiConfig();
            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production");
                /* Forçando ser Https. Diz para o browser que a app só fala HTTPS */
                /* Porém, se algum externo chamar a APP sem https, então a app vai se comunicar sem https */
                /* Por isso usa UseHttpsRedirection AppConfig, e com isso mesmo vindo sem https, ele força ser https */
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvcConfig();
        }
    }
}
