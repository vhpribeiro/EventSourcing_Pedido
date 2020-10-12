using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing_Pedido.API.Configuracoes
{
    public class ConfiguracaoDoSwagger
    {
        public static void Configurar(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>  
            {  
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo  
                {  
                    Title = "Pedido API",  
                    Version = "v1",  
                    Description = "Projeto para praticar event sourcing e comunicação de micro serviços através do RabbitMQ," +
                                  " o projeto que o complementa é o EventSourcing_Pagamento. A ideia é simular um pedido feito em um " +
                                  "e-commerce. Este serviço recebe o chamado de criar o pedido e o outro micro serviço de pagamento valida o cartão de crédito." +
                                  "Essa validação é feita de maneira assíncrona e por um background service na aplicação de Pagamento",  
                });  
            });  
        }    
    }
}