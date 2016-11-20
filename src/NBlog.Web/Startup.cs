using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NBlog.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NBlog.Domain.IRepositories;
using NBlog.EntityFrameworkCore.Repositories;
using NBlog.Application;
using NBlog.Application.UserApp;
using NBlog.Application.MenuApp;
using NBlog.Application.DepartmentApp;
using NBlog.Application.RoleApp;
using Microsoft.Extensions.FileProviders;
using System.IO;
using NBlog.Web.Areas.Manage.Filters;

namespace NBlog.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //初始化映射关系
            NBlogMapper.Initialize();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("Default");

            //添加数据上下文
            services.AddDbContext<NBlogDBContext>(options => options.UseSqlServer(sqlConnectionString), ServiceLifetime.Scoped);

            //依赖注入
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentAppService, DepartmentAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleAppService, RoleAppService>();

            //使用mvc
            services.AddMvc();
            //Session服务
            services.AddSession();

            //登录拦截服务
            services.AddScoped<LoginActionFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                //开发环境异常处理
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境异常处理
                app.UseExceptionHandler("/Shared/Error");
            }
            //使用静态文件
            app.UseStaticFiles();

            //可以访问非wwwroot的文件
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
            //});

            //使用session
            app.UseSession();

            //400 404 等错误代码处理方式
            //app.UseStatusCodePagesWithRedirects("/shared/error");

            //使用Mvc，设置默认路由为系统登录
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                   template: "{area:exists}/{controller=home}/{action=index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");
            });

            SeedData.Initialize(app.ApplicationServices);//初始化数据
        }
    }
}
