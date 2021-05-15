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

    protected Rigidbody _body;

    [SerializeField] private BoosterData _data;

    [SerializeField] private ParticleSystem _smoke01;

    [SerializeField] private ParticleSystem _smoke02;

    [SerializeField] private ParticleSystem _fire;
    #endregion

    #region UNITY METHODS

    public virtual void Awake()
    {
        _body = GetComponent<Rigidbody>();
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
    }

    /// <summary>
    /// Método responsável por parar emissao de particulas de fogo.
    /// </summary>
    public void StopFire()
    {
        _fire.Stop();
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

            if(time < _data.PropulsionTime)
            {
                if(force < _data.PropulsionPower)
                {
                    force += Time.deltaTime * _data.PropulsionAceleration;
                }
                _body.AddForce(_body.transform.forward * force, ForceMode.Acceleration);
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
