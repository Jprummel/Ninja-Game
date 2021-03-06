﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
    private CharacterController _charController;
    private AnimationController _animator;
    //Speed
    [SerializeField]private float _movementSpeed;
    [SerializeField]private float _turnSpeed;
    //Vectors
    private Vector3             _moveDir;
    private Quaternion          _rotation;
    private CameraController    _camController;

    public Vector3 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value; }
    }

    void Start()
    {
        _animator = GetComponent<AnimationController>();
        _camController = GetComponent<CameraController>();
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_moveDir.magnitude > 0)
        {
            Move();
        }

        AnimationHandling();
    }

    public void Inputs(Vector3 moveDirection)
    {
        _moveDir = moveDirection;
    }

    public void Move()
    {
        float moveDirX = _moveDir.x >= 0 ? _moveDir.x : -_moveDir.x;
        float moveDirZ = _moveDir.z >= 0 ? _moveDir.z : -_moveDir.z;
        float maxSpeed = moveDirX >= moveDirZ ? moveDirX : moveDirZ;

        _moveDir = (_moveDir.x * _camController.CameraRight + _moveDir.z * _camController.CameraForward).normalized * _movementSpeed * maxSpeed * Time.deltaTime;

        if (_moveDir != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_moveDir * Time.deltaTime);
        }
        
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,_rotation,Time.deltaTime * _turnSpeed);
        _charController.Move(_moveDir);
    }

    void AnimationHandling()
    {
        if (_moveDir == Vector3.zero)
        {
            _animator.SetAnimInt("MoveState", 0);
        }
        else if (_moveDir != Vector3.zero)
        {
            _animator.SetAnimInt("MoveState", 1);
        }
    }
}