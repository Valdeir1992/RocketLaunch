using UnityEngine;

/// <summary>
/// Script responsável por aplicar efeito do vento.
/// </summary>
public sealed class EnvironmentWind : IWind
{
    #region PRIVATE VARIABLE

    private readonly Rigidbody _body;

    private Vector3 _direction;
    #endregion

    #region CONSTRUCTOR

    public EnvironmentWind(Rigidbody body)
    {
        _body = body;
        _direction = Vector3.right;
    }
    #endregion

    #region OWN METHODS

    public void Apply()
    {
        _body.AddForce(_direction / 3, ForceMode.Acceleration);
    }
    #endregion
}
