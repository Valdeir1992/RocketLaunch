using System.Collections.Generic;
using System.Collections;
using UnityEngine;
/// <summary>
/// Script responsável por gerencia o secundario estágio do foguete.
/// </summary>
public sealed class SecondStage : Booster, ISubject<Stage>, IObserver<Stage>
{
    #region PRIVATE VARIABLE

    private List<IObserver<Stage>> _observer = new List<IObserver<Stage>>();

    [SerializeField] private Transform _centerOfMass;
    #endregion

    #region UNITY METHODS
    private void OnEnable()
    {
        ISubject<Stage> subject = FindObjectOfType<FirstStage>();

        if (subject != null)
        {
            subject.Register(this);
        }
    }

    public override void Awake()
    { 
        base.Awake();

        StopSmoke();

        StopFire();

        _myBody.centerOfMass = _centerOfMass.localPosition;
    }

    private void OnDisable()
    {
        ISubject<Stage> subject = FindObjectOfType<FirstStage>();

        if (subject != null)
        {
            subject.Remove(this);
        }
    }
    #endregion

    #region OWN METHODS
    public override void Callback()
    { 
        StartCoroutine(FallCheck());

        base.Callback();
    } 
    #endregion

    #region PATTERN OBSERVER

    public void NotifyAll(Stage mensage)
    {
        foreach (var item in _observer)
        {
            item.Notify(mensage);
        }
    }

    public void Register(IObserver<Stage> observer)
    {
        _observer.Add(observer);
    }

    public void Remove(IObserver<Stage> observer)
    {
        if (_observer.Contains(observer))
        {
            _observer.Remove(observer);
        }
    }

    public void Notify(Stage mensage)
    {
        switch (mensage)
        {
            case Stage.STAGE_ONE_COMPLETED:
                StartCoroutine(Ignition(Callback));
                TriggerSmoke();
                TriggerFire();
                break;
        }
    }
    #endregion

    #region COROUTINE

    /// <summary>
    /// Método responsável por verificar o início da queda do foguete.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FallCheck()
    { 

        yield return new WaitUntil(() => _myBody.velocity.y <= 0); 
        
        NotifyAll(Stage.STAGE_TWO_COMPLETED);

        _myBody.drag = 5;

        yield break;
    }
    #endregion
}
