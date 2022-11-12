using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    [Header("Ground check atributes")]
    [SerializeField]
    private Transform groundedChildTransform;
    [SerializeField]
    private LayerMask groundLayer;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (IsGrounded())
        {
            _animator.SetBool("IsGrounded", true);
            _animator.SetFloat("InputVertical", 0);
            _animator.SetFloat("InputMagnitude", _rigidbody.velocity.magnitude);

        } else
        {
            _animator.SetBool("IsGrounded", false);
            _animator.SetFloat("InputMagnitude", 0);
            _animator.SetFloat("InputVertical", _rigidbody.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundedChildTransform.position, 0.2f, groundLayer);
    }
}
