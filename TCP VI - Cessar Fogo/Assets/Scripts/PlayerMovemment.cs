using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotacaoVelocidade = 10f;

    [SerializeField] private Transform cameraTransform; // arraste a c�mera real do jogador aqui

    private Vector2 moveInput;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            // Dire��o com base na c�mera
            Vector3 direcao = new Vector3(moveInput.x, 0, moveInput.y);
            direcao = cameraTransform.TransformDirection(direcao);
            direcao.y = 0;

            // Move o jogador
            Vector3 movimento = direcao.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movimento);

            // Gira o jogador para a dire��o do movimento
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotacaoAlvo, rotacaoVelocidade * Time.fixedDeltaTime));
        }
    }
}

