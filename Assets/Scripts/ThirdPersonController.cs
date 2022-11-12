using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    private ThirdPersonActionsAsset playerActionAsset;
    private InputAction move;

    private Rigidbody _rigidBody;

    [Header("Movement fields")]
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float fallForce = 2.5f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float minimunInputValue = 0.35f;
    private Vector3 forceDirection = Vector3.zero;

    [Header("Camera reference")]
    [SerializeField]
    private Camera playerCamera;

    [Header("Ground check atributes")]
    [SerializeField]
    private Transform groundedChildTransform;
    [SerializeField]
    private LayerMask groundLayer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        playerActionAsset = new ThirdPersonActionsAsset();
    }

    private void OnEnable()
    {
        playerActionAsset.Character.Jump.started += DoJump;
        move = playerActionAsset.Character.Move;
        playerActionAsset.Character.Enable();
    }

    private void OnDisable()
    {
        playerActionAsset.Character.Jump.started -= DoJump;
        playerActionAsset.Character.Disable();
    }

    private void FixedUpdate()
    {
        Move();
        LookAt();
        GravityBoost();
    }

    private void Move()
    {
        //Basic movement, based on input
        if (move.ReadValue<Vector2>().magnitude > minimunInputValue)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;
        }

        _rigidBody.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        //Prevent infinity speed
        Vector3 horizontalVelocity = _rigidBody.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > Math.Pow(maxSpeed, 2))
            _rigidBody.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * _rigidBody.velocity.y;

    }

    private void GravityBoost()
    {
        if (_rigidBody.velocity.y < -1f && !IsGrounded()) _rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallForce - 1) * Time.fixedDeltaTime;
    }

    private void LookAt()
    {
        Vector3 direction = _rigidBody.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this._rigidBody.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _rigidBody.angularVelocity = Vector3.zero;
    }
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded()) { forceDirection += Vector3.up * jumpForce; }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundedChildTransform.position, 0.1f, groundLayer);
    }
}
