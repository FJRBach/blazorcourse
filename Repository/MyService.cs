using WebAppBach.Repository.Interfaces;

namespace WebAppBach.Repository
{
    public class MyService : IMyServices
    {
        public string GetMessage()
        {
            return "Hola";
        }

        public Task<IEnumerable<Service>> GetServicesAsync()
        {
            var services = new List<Service>
            {
                new Service { Id = 1, Name ="Venta de productos", Price= 25400 },
                new Service { Id = 2, Name ="Renta de productos", Price= 25400 },
            };
            return Task.FromResult<IEnumerable<Service>>(services);
        }
    }
}
// La clase MyService implementa la interfaz IMyServices y se puede inyectar cuando algún .razor requiera la instancia MyService
// Por ejemplo en este caso es la PageInjectionD