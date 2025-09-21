namespace WebAppBach.Components.Services.Interfaces
{
    public interface IElementoStateService
    {

        // Lectura IreadOnlyList para lista actual de elementos
        IReadOnlyList<string> Elementos {  get; }
        /*  
         *  Method to Add
         */ 
        void AddElemento(string elemento);

        /*
         Method to changes data
        Parameters: 
        index: para conocer el valor actual
        newValue: para el dato nuevo
         */
        void ChangesData(int index, string nuevoValor);

        event Action OnChange;


    }
}
