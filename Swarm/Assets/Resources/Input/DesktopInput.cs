using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour, IInput
{
    public bool IsGetInputXPositive { get; set; }
    public bool IsGetInputZPositive { get; set; }

    public Vector3 Direction { get; set; }

    public event Action<bool> InputXStateChanged;
    public event Action<bool> InputZStateChanged;
    public event Action<bool> Spider;

    const string _vertical = "Vertical";
    const string _horizontal = "Horizontal";

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis(_horizontal);
        float verticalInput = Input.GetAxis(_vertical);

        Vector3 newDirection = new Vector3(horizontalInput, 0, verticalInput);

        bool isInputXPositiveNow = newDirection.x > 0f;
        bool isInputZPositiveNow = newDirection.z > 0f;

        if (isInputXPositiveNow != IsGetInputXPositive)
        {
            IsGetInputXPositive = isInputXPositiveNow;
            InputXStateChanged?.Invoke(IsGetInputXPositive);
        }

        if (isInputZPositiveNow != IsGetInputZPositive)
        {
            IsGetInputZPositive = isInputZPositiveNow;
            InputZStateChanged?.Invoke(IsGetInputZPositive);
        }

        Direction = newDirection; 
    }
}
