
/// <summary>
/// Interface responsável por moldar subjects.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISubject<T>
{
    /// <summary>
    /// Método com a funcionalidade de registrar um observer.
    /// </summary>
    /// <param name="observer"></param>
    void Register(IObserver<T> observer);

    /// <summary>
    /// Método com a funcionalidade de remover um observer registrado.
    /// </summary>
    /// <param name="observer"></param>
    void Remove(IObserver<T> observer);

    /// <summary>
    /// Método responsável por notificar todos os observers registrados.
    /// </summary>
    /// <param name="mensage"></param>
    void NotifyAll(T mensage);
}
