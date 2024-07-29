using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 direction;
   
   
    private bool isGrounded;
   
    public bool IsGrounded => isGrounded;
    public float verticalInput => Input.GetAxis("Vertical");
    public float horizontalInput => Input.GetAxis("Horizontal");
    public bool IsRunning { get; private set; }
    private bool isJumping;
    public event Action OnJump;
  
    private bool canMovePlayer = true;
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
        if(GameManager.instance.currentStatus==GameStatus.Dead)return;
        CheckGroundStatus();
        if (Input.GetButtonDown("Jump") && isGrounded&&!isJumping)
        {
            Jump();
        }
    }

 
    private void FixedUpdate()
    {
        if(GameManager.instance.currentStatus==GameStatus.Dead)return;

        if (!isJumping)
        {
            HandleMovement();
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        

     
        transform.Rotate(Vector3.up * mouseX);

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
          
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
        else
        {
            IsRunning = false;
        }
    }

    private void Jump()
    {
        isJumping = true;
        DOVirtual.DelayedCall(.1f,()=>
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        });
  
        OnJump?.Invoke();
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);

        if (isGrounded && isJumping)
        {
            
            DOVirtual.DelayedCall(1f,()=>
            {
                isJumping = false;
            });
   
        }
    }
  
  
}
