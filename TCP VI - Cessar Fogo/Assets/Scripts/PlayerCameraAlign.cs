using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraAlign : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCam;
    [SerializeField] private float alinhamentoVelocidade = 5f;
    [SerializeField] private float tempoSemInputParaAlinhar = 1.2f;

    private float tempoSemInput = 0f;

    private Vector2 ultimoInputCamera = Vector2.zero;

    void Update()
    {
        // Supondo que a c�mera use o eixo do Right Stick (X = horizontal)
        float input = Input.GetAxis("RightStickHorizontal");

        // Se o jogador est� movendo a c�mera, reinicia o tempo
        if (Mathf.Abs(input) > 0.1f)
        {
            tempoSemInput = 0f;
            ultimoInputCamera.x = input;
        }
        else
        {
            tempoSemInput += Time.deltaTime;
        }

        // Se passou o tempo sem mexer a c�mera, realinha o personagem
        if (tempoSemInput >= tempoSemInputParaAlinhar)
        {
            // Rotaciona o jogador para onde a c�mera est� olhando (ignora inclina��o)
            Vector3 frenteCam = freeLookCam.transform.forward;
            frenteCam.y = 0f;

            if (frenteCam.sqrMagnitude > 0.1f)
            {
                Quaternion rotacaoAlvo = Quaternion.LookRotation(frenteCam);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, Time.deltaTime * alinhamentoVelocidade);
            }
        }
    }
}

