using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TemperatureState
{
    FREEZED,
    COLD,
    NORMAL,
    HOT,
    HELL
}

public class TemperatureSystem : MonoBehaviour
{
    private TemperatureState state = TemperatureState.NORMAL;
    private float temperature = 25;

    public virtual void AddTemp(float change)
    {
        temperature += change;
    }
}
