using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsável por representar o paraquedas.
/// </summary>
public sealed class Parachute : MonoBehaviour, IObserver<Stage>
{
    #region PRIVATE VARIABLES

    private Transform _myParachute;
    #endregion

    #region UNITY METHOS

    private void Awake()
    {
        _myParachute = transform;
    }

    private void OnEnable()
    {
        ISubject<Stage> subject = FindObjectOfType<SecondStage>();

        if (subject != null)
        {
            subject.Register(this);
        }
    }

    private void OnDisable()
    {
        ISubject<Stage> subject = FindObjectOfType<SecondStage>();

        if (subject != null)
        {
            subject.Remove(this);
        }
    }

    #endregion

    #region PATTERN OBSERVER 
     
    public void Notify(Stage mensage)
    {
        switch (mensage)
        {
            case Stage.STAGE_TWO_COMPLETED:
                OpenParachute();
                break;
        }
    }
    #endregion

    /// <summary>
    /// Método responsável por abrir o paraquedas.
    /// </summary>
    public void OpenParachute()
    {
        _myParachute.localScale = new Vector3(1,1,1);
    }

    #region COROUTINE


    #endregion
}
