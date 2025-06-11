using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Pulo")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("Referências")]
    [SerializeField] private Transform cameraTransform; // arraste a câmera real do jogador aqui

    private Vector2 moveInput;
    private Rigidbody rb;

    private bool isRunning = false;
    [SerializeField] private bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (groundCheck == null)
        {
            // Procura o GroundCheck como filho, se não tiver sido setado no inspector
            groundCheck = transform.Find("GroundCheck");

            if (groundCheck == null)
            {
                Debug.LogWarning($"{name} está sem GroundCheck configurado!");
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("pulou");
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    private void FixedUpdate()
    {
        CheckGround();
        if (moveInput.sqrMagnitude > 0.01f)
        {
            float speed = moveSpeed * (isRunning ? runMultiplier : 1f);
            Debug.Log($"velocidade atual: {speed} ");

            Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 worldDirection = cameraTransform.TransformDirection(inputDirection);
            worldDirection.y = 0f;

            Vector3 movement = worldDirection.normalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

            // Rotaciona o jogador na direção do movimento
            if (worldDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(worldDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }
}

