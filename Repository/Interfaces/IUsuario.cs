namespace WebAppBach.Repository.Interfaces
{
    public interface IUsuario
    {
        string Email { get; set; }
        string Nombre { get; set; }
        string Password { get; set; }
    }
}