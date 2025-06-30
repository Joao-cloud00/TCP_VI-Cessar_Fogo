using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TorreCameraController : MonoBehaviour
{
    [SerializeField] private GameObject vcamTorre; // arraste o GameObject da virtual camera
    [SerializeField] private float velocidadeMovimento = 2f;

    [SerializeField] private GameObject vcamJogador; // a virtual camera do jogador que será desativada
    [SerializeField] private Camera cameraRealJogador; // a camera real com CinemachineBrain


    private Vector2 inputMovimento;
    private bool ativa = false;

    private void Start()
    {
        vcamTorre.SetActive(false);
    }

    public void AtivarCamera()
    {
        vcamTorre.SetActive(true);
        if (vcamJogador != null) vcamJogador.SetActive(false);
        ativa = true;
    }

    public void DesativarCamera()
    {
        vcamTorre.SetActive(false);
        if (vcamJogador != null) vcamJogador.SetActive(true);
        ativa = false;
    }



    public bool EstaAtiva() => ativa;

    public void ReceberInput(Vector2 direcao)
    {
        inputMovimento = direcao;
    }

    private void Update()
    {
        if (!ativa) return;

        Vector3 movimento = new Vector3(inputMovimento.x, 0f, inputMovimento.y) * velocidadeMovimento * Time.deltaTime;
        transform.position += movimento;
    }
}

