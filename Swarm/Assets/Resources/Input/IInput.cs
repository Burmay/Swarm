using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public Vector3 Direction { get; set; }
    public bool IsGetInputXPositive { get; set; }
    public bool IsGetInputZPositive { get; set; }
    public event Action<bool> InputXStateChanged;
    public event Action<bool> InputZStateChanged;
    public event Action<bool> Spider;
}
