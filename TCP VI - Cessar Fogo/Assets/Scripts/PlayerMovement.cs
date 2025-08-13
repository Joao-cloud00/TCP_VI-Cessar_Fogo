using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
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

    [Header("Audio")]


    [Header("Referências")]
    [SerializeField] private Transform cameraTransform; // arraste a câmera real do jogador aqui

    public bool estaProximoDaTorre = false;

    private Vector2 moveInput;
    private Rigidbody rb;
    private PlayerAudio PlayerAudio;
    private bool isRunning = false;
    private bool isWalking = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isFalling;
    [SerializeField] private float custoPulo = 15f;
    [SerializeField] private float custoCorrida = 5f;
    private JogadorEnergia energia;
    Animator animator;

    public bool controleAtivo = true;

  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerAudio = GetComponent<PlayerAudio>();
        energia = GetComponent<JogadorEnergia>();
        animator = GetComponent<Animator>();

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


    private void FixedUpdate()
    {
        if (!controleAtivo) { moveInput = Vector2.zero; isWalking = false; animator.SetBool("Andando", isWalking); return; }
        CheckGround();


        SFXManager();
        animator.SetBool("Andando", isWalking);


        if (moveInput.sqrMagnitude > 0.01f)
        {

            float speed = moveSpeed * (isRunning ? runMultiplier : 1f);
            //Debug.Log($"velocidade atual: {speed} ");

            Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            Vector3 worldDirection = cameraTransform.TransformDirection(inputDirection);
            worldDirection.y = 0f;

            Vector3 movement = worldDirection.normalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);

            isWalking = true;

            // Rotaciona o jogador na direção do movimento
            if (worldDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(worldDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }
        else { isWalking = false; }
    }
    private void SFXManager()
    {
        //andar

        if (animator.GetBool("Andando") && !animator.GetBool("Correndo"))
        {
            PlayerAudio.playSFX(PlayerAudio.andarCOD);
        }
        else
        {
            PlayerAudio.stopSFX(PlayerAudio.andarCOD);
        }

        if (animator.GetBool("Andando") && animator.GetBool("Correndo"))
        {
            PlayerAudio.playSFX(PlayerAudio.correrCOD);
        }
        else
        {
            PlayerAudio.stopSFX(PlayerAudio.correrCOD);

        }
    }
    public void SetControleAtivo(bool ativo)
    {
        controleAtivo = ativo;
    }

    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!controleAtivo) return;
        moveInput = context.ReadValue<Vector2>();
        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!controleAtivo) return;

        if (energia.TemEnergia(custoPulo))
        {


            isRunning = context.ReadValueAsButton();
            animator.SetBool("Correndo", isRunning);
            
            energia.ConsumirEnergia(custoCorrida);
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("pulou");
        if (!controleAtivo) return;

        if (context.performed && isGrounded && energia.TemEnergia(custoPulo))
        {
            PlayerAudio.playSFX(PlayerAudio.pularCOD);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            energia.ConsumirEnergia(custoPulo);
            animator.SetTrigger("Pulou");
        }
    }



    //public void OnMoverCameraTorre(InputAction.CallbackContext context)
    //{
    //    if (torreController != null && torreController.EstaAtiva())
    //    {
    //        torreController.ReceberInput(context.ReadValue<Vector2>());
    //    }
    //}


    //public void OnToggleTorreCamera(InputAction.CallbackContext context)
    //{

    //    if (!context.performed) return;

    //    //Debug.Log("Botão Triângulo pressionado");

    //    if (estaProximoDaTorre)
    //    {
    //        //Debug.Log("Está próximo da torre, alternando câmera");
    //        if (torreCamera.EstaAtiva())
    //        {
    //            torreCamera.DesativarCamera();
    //            SetControleAtivo(true);
    //            //Debug.Log("Usando camera");
    //        }
    //        else
    //        {
    //            torreCamera.AtivarCamera();
    //            SetControleAtivo(false);
    //        }
    //    }
    //}


    

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }
}