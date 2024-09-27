using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EasingService
{
    //public static float EaseOutFloat(float grad, float time)
    //{
    //    float result = 1 - MathF.Pow(1 - time, grad);
    //    if (result > 1) return 1;
    //    Debug.Log($"Time = {time}, Result = {result}");
    //    return result;
    //}

    public static float EaseOutCubic(float time)
    {
        if(time > 1) time = 1;
        float result = 1f - Mathf.Pow(1f - time, 3f);
        //Debug.Log($"Time = {time}, Result = {result}");
        return result;
    }

    public static float EaseInOutQuart(float time)
    {
        if(time > 1) time = 1;

        if (time < 0.5)
        {
            return 8 * Mathf.Pow(time, 4);
        }
        else
        {
            return 1 - Mathf.Pow(-2 * time + 2, 4) / 2;
        }
    }
}