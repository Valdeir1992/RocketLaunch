using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Script responsável por gerenciar os propulsores.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Booster : MonoBehaviour
{
    #region PRIVATE VARIABLES

    private IWind _wind;

    [SerializeField] private BoosterData _myData;

    [SerializeField] private ParticleSystem _smoke01;

    [SerializeField] private ParticleSystem _smoke02;

    [SerializeField] private ParticleSystem _fire;
    #endregion

    #region PROTECTED VARIABLES

    protected Rigidbody _myBody;

    protected AudioSource _myAudioSource;
    #endregion

    #region UNITY METHODS

    public virtual void Awake()
    {
        _myBody = GetComponent<Rigidbody>();

        _myAudioSource = GetComponent<AudioSource>();

        _wind = DataFactory.GetWindController(_myBody);
    }

    private void FixedUpdate()
    {
        _wind.Apply();
    }
    #endregion

    #region OWN METHOS


    /// <summary>
    /// Método executado após o tempo de propulsao do foguete.
    /// </summary>
    public virtual void Callback()
    {
        Debug.Log($"finish {GetType().Name}");
    }

    /// <summary>
    /// Método responsável por iniciar emissao de particulas de fumaça
    /// </summary>
    public void TriggerSmoke()
    {
        _smoke01.Play();

        _smoke02.Play();
    }

    /// <summary>
    /// Método responsável por parar emissao de particulas de fumaça.
    /// </summary>
    public void StopSmoke()
    {
        _smoke01.Stop();

        _smoke02.Stop();
    }

    /// <summary>
    /// Método responsável por iniciar emissao de particulas de fogo.
    /// </summary>
    public void TriggerFire()
    {
        _fire.Play();
        _myAudioSource.Play();
    }

    /// <summary>
    /// Método responsável por parar emissao de particulas de fogo.
    /// </summary>
    public void StopFire()
    {
        _fire.Stop();
        _myAudioSource.Stop();
    }
    #endregion

    #region COROUTINE

    /// <summary>
    /// Corotina responsável por iniciar movimentacao do foguete.
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator Ignition(Action callback)
    {  
        float time = 0;

        float force = 0;

        while (true)
        {
            time += Time.deltaTime;

            if(time < _myData.PropulsionTime)
            {
                if(force < _myData.PropulsionPower)
                {
                    force += Time.deltaTime * _myData.PropulsionAceleration;
                }
                _myBody.AddForce(_myBody.transform.forward * force, ForceMode.Acceleration);
            }
            else
            {
                StopSmoke();

                StopFire();

                callback?.Invoke();

                yield break;
            } 

            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
