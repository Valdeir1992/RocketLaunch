
/// <summary>
/// Interface responsável por moldar observadores.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IObserver<T>
{
    /// <summary>
    /// Método responsável por notificar observador.
    /// </summary>
    /// <param name="mensage"></param>
    void Notify(T mensage);
}
