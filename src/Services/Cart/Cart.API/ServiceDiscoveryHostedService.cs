using Consul;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cart.API
{
    public class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConfiguration configuration;
        private string registrationId;
        private readonly ConsulClient consulClient;
        public ServiceDiscoveryHostedService(IConfiguration configuration)
        {
            this.configuration = configuration;
            consulClient = new ConsulClient(config =>
            {
                config.Address = configuration.GetValue<Uri>("ServiceConfig:ServiceDiscoveryAddress");
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceId = configuration.GetValue<string>("ServiceConfig:ServiceId");
            var serviceName = configuration.GetValue<string>("ServiceConfig:ServiceName");
            var serviceAddress = configuration.GetValue<Uri>("ServiceConfig:ServiceAddress");
            registrationId = $"{serviceName}-{serviceId}";

            var registration = new AgentServiceRegistration
            {
                ID = registrationId,
                Name = serviceName,
                Address = serviceAddress.Host,
                Port = serviceAddress.Port
            };

            await consulClient.Agent.ServiceDeregister(registrationId, cancellationToken);
            await consulClient.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await consulClient.Agent.ServiceDeregister(registrationId, cancellationToken);
        }
    }
}
