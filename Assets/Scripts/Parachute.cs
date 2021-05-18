using System.Collections; 
using UnityEngine;

/// <summary>
/// Script responsável por representar o paraquedas.
/// </summary>
public sealed class Parachute : MonoBehaviour, IObserver<Stage>
{
    #region PRIVATE VARIABLES

    private Transform _myTransform;
    #endregion

    #region UNITY METHOS

    private void Awake()
    {
        _myTransform = transform;
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
                Open();
                break;
        }
    }
    #endregion

    /// <summary>
    /// Método responsável por abrir o paraquedas.
    /// </summary>
    public void Open()
    {
        StartCoroutine(OpenParachute()); 
    }

    #region COROUTINE

    private IEnumerator OpenParachute()
    {
        float elapsedTime = 0;

        while(elapsedTime < 5)
        {
            elapsedTime += Time.deltaTime;

            _myTransform.localScale = Vector3.Lerp(_myTransform.localScale, Vector3.one, elapsedTime/5);

            yield return null;
        }

        yield return new WaitUntil(() => _myTransform.rotation.eulerAngles.x > 89);

        FixedJoint join = gameObject.AddComponent<FixedJoint>();

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        join.connectedBody = transform.parent.GetComponent<Rigidbody>();

        transform.SetParent(null);
    }
    #endregion
}
