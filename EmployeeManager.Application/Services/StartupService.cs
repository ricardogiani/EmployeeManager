using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Services
{
    public class StartupService : IHostedService
    {
        private readonly IAdapterConfig _adapterConfig;

        // Injetar quaisquer dependências que seu serviço de inicialização precise
        public StartupService(IAdapterConfig adapterConfig)
        {
            _adapterConfig = adapterConfig;
        }

        // Este método é chamado quando a aplicação está iniciando
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartupService: Iniciando...");

            _adapterConfig.Config();

            Console.WriteLine("StartupService: Concluído o StartAsync.");
            return Task.CompletedTask; // Retorna um Task.CompletedTask para operações síncronas
        }

        // Este método é chamado quando a aplicação está desligando
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartupService: Desligando...");
            // Coloque aqui a lógica de limpeza, se necessário
            return Task.CompletedTask;
        }
    }
}