using NEventStore;
using PostMortem.Data.NEventStore;
using PostMortem.Data.NEventStore.Config;
using PostMortem.Domain.Comments;
using PostMortem.Domain.Events.Comments;
using PostMortem.Domain.Projects;
using PostMortem.Domain.Questions;

namespace PostMortem.Web
{
    using AutoMapper;
    using Converters;
    using Data.MongoDb;
    using Data.MongoDb.Config;
    using Domain;
    using Infrastructure;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Swashbuckle.AspNetCore.Swagger;
    using Zatoichi.Common.Infrastructure.Resilience;
    
    public static class ServiceExtensions
    {
        public static IServiceCollection InitializeContainer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddLogging();
            services.AddAutoMapper(
                typeof(ProjectProfile).Assembly, 
                typeof(Data.MongoDb.Converters.MongoDbProfile).Assembly);
            
            services.AddTransient<IExecutionPolicies, ExecutionPolicies>();
            services.AddTransient<IPolicyFactory, AsyncPolicyFactory>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>(); 
            services.AddTransient<ICommentEventStoreRepository, CommentEventStoreRepository>(); 
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IStoreEvents, MongDbStoreEvents>();
            services.TryAddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddHttpClient<INameGeneratorClient, NameGeneratorClient>();
            services.AddMediatR(typeof(CommentLikedEventArgs).Assembly);

            return services;
        }

        public static IServiceCollection InitializeOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<MongoOptions>(configuration.GetSection("MongoOptions"));
            services.Configure<MongoDbStoreEventsOptions>(configuration.GetSection("MongoDbStoreEventsOptions"));
            services.Configure<PolicyOptions>(configuration.GetSection("PolicyOptions"));
            return services;

        }

        public static IServiceCollection InitializeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Postmortem API",
                    Description = "Postmortem API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Postmortem", Email = "steve.hoff@identifix.com" }
                });
            });

            return services;

        }
    }
}