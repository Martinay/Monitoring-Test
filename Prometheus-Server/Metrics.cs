using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prometheus.Client;

namespace Prometheus_Server
{
    public class Metrics
    {
        public static readonly Gauge CPU_Idle = Prometheus.Client.Metrics
            .CreateGauge("server_cpu_available", "Percentage of idle process", false, "hostname");
        public static readonly Gauge Memory_Available = Prometheus.Client.Metrics
            .CreateGauge("server_ram_available", "Mb of ram available", false, "hostname");
    }
}
