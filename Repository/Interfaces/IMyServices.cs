namespace WebAppBach.Repository.Interfaces

  // Se declaran los methods availables
  {
    public class Service
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
    // Toda class que implemente this interface get access to the methods 
    public interface IMyServices
    {
        string GetMessage();

        Task<IEnumerable<Service>> GetServicesAsync();
    }
}
