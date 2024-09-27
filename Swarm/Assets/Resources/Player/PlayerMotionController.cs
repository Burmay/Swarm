using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerMotionController : MonoBehaviour
{
    [Inject] IInput _input;
    
    [SerializeField] Rigidbody _rigidbody;
    
    [SerializeField] float _baseSpeed;
    [SerializeField] float _freeStopTime;
    [SerializeField] int _baseRotationSpeed;
    [SerializeField] float _easeTime;
    [SerializeField] float _easeApplyTime;
    
    const float _diagonalCoef = 0.707f;
    float _tempMaxSpeed;
    
    EasingSystem _easingSystem;
    RotationSystem _rotationSystem;
    
    Vector3 _currentDirection;
    Vector3 _currentSpeed, _tempSpeed;
    Vector3 _curretAcceleration;
    Vector3 _lastPosition;
    Vector3 _velocity;
    
    void Awake()
    {
        _easingSystem = new EasingSystem();
        _rotationSystem = new RotationSystem(_baseRotationSpeed);
        _input.InputXStateChanged += _easingSystem.UpdateStatusX;
        _input.InputZStateChanged += _easingSystem.UpdateStatusZ;
    }
    
    private void FixedUpdate()
    {
        UpdateFields();
        Accseleration();
        Desseleration();
        Move();
        //Rotate();
    }
    
    void UpdateFields()
    {
        _velocity = (_rigidbody.position - _lastPosition) / Time.fixedDeltaTime;
        _velocity.x = Math.Abs(_velocity.x);
        _velocity.z = Math.Abs(_velocity.z);
        _lastPosition = _rigidbody.position;
    }
    
    void Accseleration()
    {
       _curretAcceleration = _easingSystem.GetAccseleration(_input.Direction, _easeTime, _easeApplyTime);
    }
    
    void Desseleration()
    {
        if (_input.Direction.x == 0 || _input.Direction.y == 0)
        {
            if (_velocity == Vector3.zero) return;
    
            if (_input.Direction.x == 0)
            {
                if (_curretAcceleration.x == 0 && _velocity.x != 0) _curretAcceleration.x = -Math.Sign(_currentSpeed.x) / _freeStopTime;
    
                if(Math.Abs((_curretAcceleration.x * Time.fixedDeltaTime * _baseSpeed) / _easeTime) > _velocity.x) 
                { _currentSpeed.x = 0; _lastPosition.x = _rigidbody.position.x; _curretAcceleration.x = 0; } 
            }
    
            if (_input.Direction.z == 0)
            {
                if (_curretAcceleration.z == 0 && _velocity.z != 0) _curretAcceleration.z = -Math.Sign(_currentSpeed.z) / _freeStopTime;
    
                if (Math.Abs((_curretAcceleration.z * Time.fixedDeltaTime * _baseSpeed) / _easeTime) > _velocity.z)
                { _currentSpeed.z = 0; _lastPosition.z = _rigidbody.position.z; _curretAcceleration.z = 0; }
            }
        }
    }
    
    void Move()
    {
        
        _tempMaxSpeed = _baseSpeed;
        _currentSpeed += (_curretAcceleration * Time.fixedDeltaTime * _baseSpeed);
        
        _currentSpeed.x = Mathf.Clamp(_currentSpeed.x, -_tempMaxSpeed, _tempMaxSpeed);
        _currentSpeed.z = Mathf.Clamp(_currentSpeed.z, -_tempMaxSpeed, _tempMaxSpeed);

        _tempSpeed = _currentSpeed;
        if (_currentSpeed.x != 0 && _currentSpeed.z != 0) { _tempSpeed *= _diagonalCoef; _tempMaxSpeed *= _diagonalCoef; }

        _rigidbody.MovePosition(_rigidbody.position + _tempSpeed * Time.fixedDeltaTime);

        Debug.Log(_tempSpeed);
    }
    
    void Rotate()
    {
        _currentDirection = _rotationSystem.GetVector(_rigidbody.rotation * Vector3.forward, _currentSpeed);
        if (_currentDirection != _rigidbody.rotation * Vector3.forward)
        {
            _rigidbody.MoveRotation(Quaternion.LookRotation(_currentDirection));
        }
    }
    
    void OnDestroy()
    {
        _input.InputXStateChanged -= _easingSystem.UpdateStatusX;
        _input.InputZStateChanged -= _easingSystem.UpdateStatusZ;
    }

    //[Inject] IInput _input;
    //
    //Vector3 virtualPoint;
    //
    //Vector3 newPoint;
    //
    //[SerializeField] float _baseSpeed;
    //[SerializeField] float _interpolation;
    //
    //private void FixedUpdate()
    //{
    //    virtualPoint += new Vector3(_input.Direction.x * _baseSpeed * Time.fixedDeltaTime, 0, _input.Direction.z * _baseSpeed * Time.fixedDeltaTime);
    //
    //    Debug.Log(virtualPoint);
    //
    //    //newPoint.x = Mathf.Lerp(transform.position.x, virtualPoint.x, _interpolation);
    //    //newPoint.z = Mathf.Lerp(transform.position.z, virtualPoint.z, _interpolation);
    //    Vector3 velocity = Vector3.zero;
    //    transform.position = Vector3.SmoothDamp(transform.position, virtualPoint, ref velocity, _interpolation);
    //}
}