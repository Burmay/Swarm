using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationSystem
{
    int _rotationSpeed;

    public RotationSystem(int rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }

    public Vector3 GetVector(Vector3 currentDirection, Vector3 targetDirection)
    {
        //if(Vector3.Angle(currentDirection, targetDirection) > 91) return null;
        if(targetDirection == Vector3.zero) return currentDirection;

        float step;
        step = _rotationSpeed * Time.fixedDeltaTime;

        float targetAngle = Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(currentDirection.z, currentDirection.x) * Mathf.Rad2Deg;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, step);

        Vector3 newDirection = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), currentDirection.y, Mathf.Sin(newAngle * Mathf.Deg2Rad));
        newDirection.Normalize();
        //Debug.Log($"target dir - {targetDirection}, new dir - {newDirection}");
        return newDirection;
    }

}
