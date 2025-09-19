using WebAppBach.Repository.Interfaces;

namespace WebAppBach.Repository
{
    public class MyService : IMyServices
    {
        public string GetMessage()
        {
            return "Hola";
        }
    }
}
