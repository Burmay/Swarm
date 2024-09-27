using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MobileInput : MonoBehaviour, IInput
{
    public bool IsGetInput { get; set; }
    public Vector3 Direction { get; set; }
    public bool IsGetInputXPositive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsGetInputZPositive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action<bool> Spider;
    public event Action<bool> InputXStateChanged;
    public event Action<bool> InputZStateChanged;
}