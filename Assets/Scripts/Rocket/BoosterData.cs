using UnityEngine;
/// <summary>
/// Script responsavel por armazenar dados do motor do foquete.
/// </summary>
[CreateAssetMenu(menuName = "Prototipo/Data/Booster")]
public class BoosterData : ScriptableObject
{
    [SerializeField] private float _propulsionTime;

    [SerializeField] private float _propulsionPower;

    [SerializeField] private float _propulsionAceleration;

    public float PropulsionTime { get => _propulsionTime; }

    public float PropulsionPower { get => _propulsionPower; }

    public float PropulsionAceleration { get => _propulsionAceleration;}
}
