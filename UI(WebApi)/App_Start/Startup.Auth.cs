﻿using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UI_WebApi_.Providers;

namespace UI_WebApi_
{
    public partial class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                // устанавливает URL, по которому клиент будет получать токен
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            // включает в приложении функциональность токенов
            app.UseOAuthBearerTokens(OAuthOptions);
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }

    }
}