﻿namespace PostMortem.Web.Controllers
{
    using System;
    using ChaosMonkey.Guards;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public abstract class BaseController : ControllerBase
    {
        protected readonly string username;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly INameGeneratorClient nameGenerator;

        protected BaseController(
            IHttpContextAccessor httpContextAccessor,
            INameGeneratorClient nameGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.nameGenerator = Guard.IsNotNull(nameGenerator, nameof(nameGenerator));
            // so we can be anonymous
            this.username = httpContextAccessor.HttpContext.Request.Cookies["username"];

            if (string.IsNullOrWhiteSpace(this.username))
            {
                this.username = this.nameGenerator.GetNameAsync().GetAwaiter().GetResult();

                if (!string.IsNullOrWhiteSpace(this.username))
                {
                    this.username = this.username.Replace('\n', ' ').Trim();
                }

                this.Set("username", this.username, null);
            }
        }

        private void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddMinutes(expireTime.Value)
                    : DateTime.Now.AddMilliseconds(10)
            };

            this.httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }
    }
}