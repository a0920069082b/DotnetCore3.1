using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using FubonMailApi.Context;
using FubonMailApi.Controllers.Services;
using FubonMailApi.Controllers.Services.IServices;
using FubonMailApi.Controllers.Services.Repositories;
using FubonMailApi.Controllers.Services.Repositories.IRepositories;
using FubonMailApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace FubonMailApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入跨域問題
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin() //允許所有網域連線
                          .AllowAnyHeader() //允許所有表頭呼叫API
                          .AllowAnyMethod() //允許任何 HTTP 方法 GET、POST、PUT、DELETE
                          .AllowCredentials(); //認證需要在 CORS 要求的特殊處理。 根據預設，瀏覽器不會傳送具有跨原始要求的認證。 認證包含 cookie 與 HTTP 驗證配置。
                });
            });

            //注入JWT驗證
            // 檢查 HTTP Header 的 Authorization 是否有 JWT Bearer Token    
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                // 設定 JWT Bearer Token 的檢查選項
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //發行者驗證
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = true, //接收者驗證
                        ValidAudience = Configuration["Jwt:Issuer"],
                        // ValidateLifetime = true, // 存活時間驗證
                        ValidateIssuerSigningKey = true, //金鑰驗證
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                }
            );

            //注入SQL Server連線
            services.AddEntityFrameworkSqlServer().AddDbContext<CustomContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);
            // services.AddTransient<CustomContext>();


            #region  注入Services的DI
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRolesService,RolesService>();
            services.AddScoped<IFunctionNamesService,FunctionNamesService>();
            services.AddScoped<IActionsService,ActionsService>();
            services.AddScoped<IUsersService, UsersService>();
            #endregion

            #region 注入Repositories的DI
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IRolePermissionsRepository, RolePermissionsRepository>();
            services.AddScoped<IFunctionNamesRepository, FunctionNamesRepository>();
            services.AddScoped<IActionsRepository, ActionsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            #endregion

            // 加入HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //加入MVC架構
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // 設定Controllers 回傳格式
            services.AddControllers().AddNewtonsoftJson(
                options => {
                     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }
            );

            //注入AutoMapper
            services.AddAutoMapper(typeof(Startup));
            //注入HttpClient
            services.AddHttpClient();

            //AddSwaggerGen：Swagger 產生器是負責取得 API 的規格並產生 SwaggerDocument 物件。
             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,CustomContext dbContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            
            // UseSwagger
            // Swagger Middleware 負責路由，提供 SwaggerDocument 物件。
            // 可以從 URL 查看 Swagger 產生器產生的 SwaggerDocument 物件。
            // http://localhost:5000/swagger/v1/swagger.json
            app.UseSwagger();

            //UseSwaggerUI
            //SwaggerUI 是負責將 SwaggerDocument 物件變成漂亮的介面。
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // description: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "RESTful API v1.0.0"
                );
                c.RoutePrefix = string.Empty;
            });

                        //設定跨域權限
            app.UseCors("CorsPolicy");
            //建立資料庫連線
            dbContext.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseCustomMiddleware(0);
            app.UseRouting();

            app.UseEndpoints(endpoints => //  取代原先2.1版本的useMVC
            {
                endpoints.MapControllers();
            });        }
    }
}
