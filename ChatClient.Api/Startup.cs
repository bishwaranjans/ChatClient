#region Namespaces

using ChatClient.Domain.SeedWork;
using ChatClient.Infrastructure.Factories;
using ChatClient.Infrastructure.Publish;
using ChatClient.Infrastructure.Subscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NATS.Client;

#endregion

namespace ChatClient.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the nats server URL.
        /// </summary>
        /// <value>
        /// The nats server URL.
        /// </value>
        public string NatsServerUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(Configuration.GetValue<string>("NATSServerUrl")) ? Defaults.Url : Configuration.GetValue<string>("NATSServerUrl");
            }
        }

        /// <summary>
        /// Gets the nats subject.
        /// </summary>
        /// <value>
        /// The nats subject.
        /// </value>
        public string NatsSubject
        {
            get
            {
                return Configuration.GetValue<string>("NATSSubject");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the services. This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatClient.Api", Version = "v1" });
            });

            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthorization();
            services.AddHttpContextAccessor();

            var connection = NatsConnectionFactory.ConnectToNats(NatsServerUrl);
            var publisher = new Publisher(connection, NatsSubject);
            var subscriber = new Subscriber(connection, NatsSubject);
            subscriber.Subscribe();
            services.AddSingleton<IPublisher>(publisher);
            services.AddSingleton<ISubscriber>(subscriber);
        }
  
        /// <summary>
        /// Configures the specified application. This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatClient.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
