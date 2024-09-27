using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingSystem
{
    bool _accselerationState;

    float _startTimeX;
    float _startTimeZ;
    float _currentTimeX;
    float _currentTimeZ;
    bool _statusX;
    bool _statusZ;

    public void UpdateStatusX(bool status) { _statusX = status; _startTimeX = Time.time; }
    public void UpdateStatusZ(bool status) { _statusZ = status; _startTimeZ = Time.time; }

    public Vector3 GetAccseleration(Vector3 vector, float easeTime, float applyTime)
    {
        _currentTimeX = Time.time - _startTimeX;
        _currentTimeZ = Time.time - _startTimeZ;
        Vector3 ease = Vector3.zero;

        ease.x = EasingService.EaseOutCubic( _currentTimeX / easeTime);
        ease.z = EasingService.EaseOutCubic(_currentTimeZ / easeTime);
        ease.x *= vector.x;
        ease.z *= vector.z;
        return ease / applyTime;
    }
}
