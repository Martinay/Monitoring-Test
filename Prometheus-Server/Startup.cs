using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.AspNetCore;

namespace Prometheus_Server
{
    public class Startup
    {
        static readonly PerformanceCounter IdleCounter = new PerformanceCounter("Processor", "% Idle Time", "_Total");
        static readonly PerformanceCounter RamCounter = new PerformanceCounter("Memory", "Available MBytes");
        private Timer _timer;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePrometheusServer(x => x.UseDefaultCollectors = false);

            app.Run(async (context) =>
            {
                if (context.Request.Path.Value.Contains("abcd"))
                {
                    new BusinessClass().BusinessMethod();
                    return;
                }
                if (context.Request.Path.Value.Contains("defg"))
                {
                    new BusinessClass().BusinessMethod2();
                    return;
                }
                if (context.Request.Path.Value.Contains("test"))
                {
                    throw new AccessViolationException("Some MEssage");
                    return;
                }



                await context.Response.WriteAsync("Hello World!");
                throw new InvalidCastException("This is the message");
            });

            _timer = new Timer(e => UpdateMetrics(), null, TimeSpan.FromSeconds(2).Milliseconds, Timeout.Infinite);
        }

        private void UpdateMetrics()
        {
            var cpu_idle = IdleCounter.NextValue();
            var mem = RamCounter.NextValue();
            Metrics.Memory_Available.WithLabels("vc039").Set(mem);
            Metrics.CPU_Idle.WithLabels("vc039").Set(cpu_idle);

            _timer.Change(TimeSpan.FromSeconds(2).Milliseconds, Timeout.Infinite);
        }
    }
}
