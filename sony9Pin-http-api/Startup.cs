using System.Net;
using System.Net.Sockets;


namespace sony9Pin_http_api
{
    public class HostApplicationLifetimeEventsHostedService : IHostedService
    {
        //public static readonly AudioVideoServersManager AudioVideoServersManager = new();

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public HostApplicationLifetimeEventsHostedService(
            IHostApplicationLifetime hostApplicationLifetime)
            => _hostApplicationLifetime = hostApplicationLifetime;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        private void OnStarted()
        {
            var localIpAddresses = Dns.GetHostAddresses(Dns.GetHostName())
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork).ToArray();
            var localIpAddress = localIpAddresses[0];

            //Program.AudioVideoServersManager.GetVersion();
            //Program.AudioVideoServersManager.Initialize(localIpAddress);
        }

        private void OnStopping()
        {
            // ...
        }

        private void OnStopped()
        {
            //Program.AudioVideoServersManager.Uninitialize();
        }
    }

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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/error");

            app.UseStaticFiles(); // js and css files in wwwroot

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
