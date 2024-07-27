using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 direction;
   
   
    private bool isGrounded;

    public bool IsGrounded => isGrounded;
    public float verticalInput => Input.GetAxis("Vertical");
    public float horizontalInput => Input.GetAxis("Horizontal");
    public bool IsRunning { get; private set; }

    public event Action OnJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody NULL");
        }
    }

    private void Update()
    {
        CheckGroundStatus();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
      
        direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            
            Vector3 moveDirection = transform.TransformDirection(direction);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            IsRunning = Input.GetKey(KeyCode.LeftShift);
            float speed = IsRunning ? runSpeed : walkSpeed;
            print(speed);
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
        else
        {
            IsRunning = false;
        }
    }

    private void Jump()
    {
        DOVirtual.DelayedCall(.1f,()=>
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        });
  
        OnJump?.Invoke();
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
    }
}
