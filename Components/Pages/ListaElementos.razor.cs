using Microsoft.AspNetCore.Components;

namespace WebAppBach.Components.Pages
{
    public partial class ListaElementos: ComponentBase
    {
        private List<string> elementos = new();
        private string nuevoElemento;

        private void AgregarElemento()
        {
            if (!string.IsNullOrWhiteSpace(nuevoElemento))
            {
                elementos.Add(nuevoElemento);
                nuevoElemento = string.Empty; //Limpiar campo de entrada
            }
        }
    }
}
