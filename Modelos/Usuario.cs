using WebAppBach.Repository.Interfaces;

namespace WebAppBach.Modelos
{
    public class Usuario : IUsuario
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
