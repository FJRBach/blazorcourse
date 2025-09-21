using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebAppBach.Components.Pages
{
    public partial class ListaElementos: ComponentBase
    {
        private string? nuevoElemento;

        private int? indiceElementoEdit = null;
        private string valorEditado;

        override protected void OnInitialized()
        {
            ElementoServicio.OnChange += StateHasChanged;
        }

       private void IniciarEdicion(int index)
        {
            indiceElementoEdit = index;
            valorEditado = ElementoServicio.Elementos[index];
        }

        private void GuardarCambios(int index)
        {
            ElementoServicio.ChangesData(index, valorEditado);
            indiceElementoEdit = null;
            valorEditado = string.Empty;
        }

        private void CancelarEdicion()
        {
            indiceElementoEdit = null;
            valorEditado = string.Empty;
        }

        private void AgregarElemento()
        {
            ElementoServicio.AddElemento(nuevoElemento);
            nuevoElemento = string.Empty;
        }

        // Gestionar evento de forma asynchrone
        private async void OnStateHasChanged() 
        {
            await InvokeAsync(StateHasChanged);
        }

        private async Task HandleKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                AgregarElemento();
            }
            await Task.CompletedTask;
        }

        // Evitar fuga de memoria (optimizar en base al estado del componente)
        public void Dispose()
        {
            ElementoServicio.OnChange -= StateHasChanged;
        }
        
    }
}
