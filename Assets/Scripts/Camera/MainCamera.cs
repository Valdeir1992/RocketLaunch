using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsável por determinar comportamento da camera principal.
/// </summary>
public sealed class MainCamera : MonoBehaviour
{
    #region PRIVATE VARIABLES

    private Transform _myTransform;

    [SerializeField] private Transform _rocket;

    [SerializeField] private float _distance;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        _myTransform = transform;
    } 

    private void Start()
    {
        StartCoroutine(Follow());
    }

    #endregion

    #region COROTINA

    /// <summary>
    /// Corotina responsável por executar comportamento de movimento da camera.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Follow()
    {
        while (true)
        {
            _myTransform.position = _rocket.position - Vector3.forward * _distance;

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
