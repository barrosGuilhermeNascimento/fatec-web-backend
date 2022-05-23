using System.Text;
using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Core.Model;
using ApiFatecWeb.Core.Repository;
using ApiFatecWeb.Core.Repository.Interface;
using ApiFatecWeb.Core.Service;
using ApiFatecWeb.Core.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiFatecWeb.Configuration
{
    public static class DependencyInjection
    {
        
        /// <summary>
        /// Add Class Library Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddClassLibraryServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region secret

            var key = Encoding.ASCII.GetBytes("!HA8MYvLCe7KnTiDhs%GRiLz#S!ceq9XP*k#z8");
            #endregion


            #region Services
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITicketMessagesService, TicketMessagesService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUtilService, UtilService>();
            #endregion

            #region Repositories
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<ITicketMessagesRepository, TicketMessagesRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPassRecoverRepository, UserPassRecoverRepository>();
            #endregion

            #region Logs
            services.AddScoped<ILogHandler, LogHandler>();
            #endregion

            #region JWT

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            #endregion

            #region Map

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //Configure the mapping here
                cfg.CreateMap<UserModel, UserModelLogin>().ReverseMap();
                cfg.CreateMap<UserModel, UserEntity>().ReverseMap();
                cfg.CreateMap<TicketInsertModel, TicketEntity>().ReverseMap();
                cfg.CreateMap<TicketMessagesInsertModel, TicketMessagesEntity>().ReverseMap();
                cfg.CreateMap<TicketMessagesModel, TicketMessagesEntity>().ReverseMap();
                cfg.CreateMap<TicketModel, TicketEntity>().ReverseMap();
                cfg.CreateMap<TicketModel, TicketInsertModel>().ReverseMap();
                cfg.CreateMap<UserRegisterModel, UserEntity>();
                cfg.CreateMap<UserRegisterModel, UserModel>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }
    }
}
