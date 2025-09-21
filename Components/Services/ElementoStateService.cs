using WebAppBach.Components.Services.Interfaces;

namespace WebAppBach.Components.Services
{
    public class ElementoStateService: IElementoStateService
    {
        private readonly List<string> _elementos = new();

        public IReadOnlyList<string> Elementos => _elementos.AsReadOnly();

        // Evento para detectar cambios en el valor de un control vinculado a un objeto de datos, en este caso
        // El objeto es el Elemento, cada que cambia es detectado en el metodo de Notificar cambio de estado o
        // NotifyStateChanged

        public event Action OnChange;

        public void AddElemento(string elemento)
        {
            // Validar la entrada
            if (string.IsNullOrEmpty(elemento))
            {
                return;
            }

            _elementos.Add(elemento);
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            if (OnChange != null)
            {
                foreach (Action handler in OnChange.GetInvocationList())
                {
                    try
                    {
                        handler.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void ChangesData(int index, string newValue)
        {
            // Se indica que si el valor no es Null o en blanco se agregué newValue
            if (string.IsNullOrWhiteSpace(newValue))
            {
                return;
            }

            // Validate index dentro del limite de la lista para evitar exception
            if (index >= 0 && index < _elementos.Count)
            {
                _elementos[index] = newValue;
                NotifyStateChanged();
            }
        }
    }
}
