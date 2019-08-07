namespace PostMortem.Web.Controllers
{
    using System;
    using ChaosMonkey.Guards;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class BaseController : ControllerBase
    {
        protected string username;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly INameGeneratorClient nameGenerator;
        private readonly ILogger logger;

        protected BaseController(
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            INameGeneratorClient nameGenerator)

        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            this.nameGenerator = nameGenerator;
            this.SetUsername();
            this.logger.LogTrace("BaseController created.");
        }

        protected BaseController(
            IHttpContextAccessor httpContextAccessor,
            INameGeneratorClient nameGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            Guard.IsNotNull(nameGenerator, nameof(nameGenerator));

            this.SetUsername();
        }

        private void SetUsername()
        {
            // so we can be anonymous
            this.username = this.httpContextAccessor.HttpContext.Request.Cookies["username"];

            if (string.IsNullOrWhiteSpace(this.username))
            {
                this.username = this.nameGenerator.GetNameAsync().GetAwaiter().GetResult();

                if (!string.IsNullOrWhiteSpace(this.username))
                {
                    this.username = this.username.Replace('\n', ' ').Trim();
                }

                this.Set("username", this.username, null);
            }
            this.logger?.LogInformation($"//---------------- Setting username to {this.username} ----------------/");
        }

        private void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddDays(expireTime.Value)
                    : DateTime.Now.AddDays(10),
                IsEssential = true,
                HttpOnly = false,
                Path = "/"
            };

            this.httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }
    }
}