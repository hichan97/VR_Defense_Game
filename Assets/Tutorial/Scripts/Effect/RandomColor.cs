using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    public float HueMax = 0f;
    public float HueMin = 0f;
    public float saturationMin = 0.7f;
    public float saturationMax = 1f;
    public float valueMin = 0.7f;
    public float valueMax = 1f;

    public UnityEvent<Color> Oncreated;

    public void Call()
    {
        var color = Random.ColorHSV(HueMin, HueMax, saturationMin, saturationMax, valueMin, valueMax);
        Oncreated.Invoke(color);
    }
}
