using UnityEngine;

/// <summary>
/// Script responsável por desacoplar instancias.
/// </summary>
internal class DataFactory
{

    /// <summary>
    /// Método responsável por retornar interface IWind.
    /// </summary>
    /// <param name="booster"></param>
    /// <returns></returns>
    internal static IWind GetWindController(Rigidbody booster)
    {
        return new EnvironmentWind(booster);
    }
}