using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script responsável por gerencia o primeiro estágio do foguete.
/// </summary>
public sealed class FirstStage : Booster, ISubject<Stage>
{
    #region PRIVATE VARIABLE

    private List<IObserver<Stage>> _observer = new List<IObserver<Stage>>();
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        StartCoroutine(Ignition(Callback));

        TriggerFire();

        TriggerSmoke();
    }
    #endregion

    #region OWN METHODS
    public override void Callback()
    {
        NotifyAll(Stage.STAGE_ONE_COMPLETED);

        Destroy(GetComponent<FixedJoint>());

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
    #endregion
}
