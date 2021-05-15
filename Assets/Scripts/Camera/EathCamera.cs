using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EathCamera : MonoBehaviour
{
    #region PRIVATE VARIABLES

    private Transform _myTransform;

    [SerializeField] private Transform _rocket; 

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
            _myTransform.LookAt(_rocket);

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
