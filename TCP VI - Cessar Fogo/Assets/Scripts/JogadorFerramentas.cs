using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorFerramentas : MonoBehaviour
{
    [SerializeField] private GameObject ferramentaAtual;
    public Transform pontoFerramenta;
    PlayerAudio playerAudio;
    [SerializeField] private float custoFerramenta = 99f;
    private JogadorEnergia energia;
    private bool controleAtivo = true;
    private bool tocou;



    private void Awake()
    {
        energia = GetComponent<JogadorEnergia>();
        playerAudio = GetComponent<PlayerAudio>();
        controleAtivo= GetComponent<PlayerMovement>().controleAtivo;
    }

    public void OnUseTool(InputAction.CallbackContext context)
    {

        controleAtivo = GetComponent<PlayerMovement>().controleAtivo;
        if (!controleAtivo) return;

        if (energia.TemEnergia(custoFerramenta))
        {
            if (ferramentaAtual == null) return;

            IFerramenta ferramenta = ferramentaAtual.GetComponent<IFerramenta>();
            if (ferramenta == null) return;

            if (ferramenta is MochilaAgua mochila)
            {

                if (context.started)
                {
                    ferramenta.Usar();
                    energia.ConsumirEnergia(custoFerramenta);


                    playerAudio.playSFX(playerAudio.mochilaInicioCOD);
                    playerAudio.playSFX(playerAudio.mochilaMeioCOD);

                    return;



                }else if (context.canceled)
                {
                    playerAudio.playSFX(playerAudio.mochilaFimCOD);
                    playerAudio.stopSFX(playerAudio.mochilaInicioCOD);
                    playerAudio.stopSFX(playerAudio.mochilaMeioCOD);

                    mochila.PararUso();
                }
                return;
            }
            else if (context.performed)
            {
                ferramenta.Usar();
                energia.ConsumirEnergia(custoFerramenta);
            }

        }
    }
    public void tocarSomAnimacao(int COD)
    {
        playerAudio.stopAll();
        playerAudio.playSFX(playerAudio.enchadaCOD);
    }
    public void EquiparNovaFerramenta(GameObject novaFerramentaPrefab)
    {
        if (ferramentaAtual != null)
            Destroy(ferramentaAtual);


            GameObject novaInstancia = Instantiate(novaFerramentaPrefab);
        Transform pontoDePegada = novaInstancia.transform.Find("PontoDePegada");

        if (pontoDePegada == null)
        {
            Debug.LogWarning("A ferramenta não tem um objeto filho chamado 'PontoDePegada'. Usando posição padrão.");
            novaInstancia.transform.SetParent(pontoFerramenta);
            novaInstancia.transform.localPosition = Vector3.zero;
            novaInstancia.transform.localRotation = Quaternion.identity;
        }
        else
        {
            // Alinha a ferramenta com base no PontoDePegada
            novaInstancia.transform.SetParent(pontoFerramenta, worldPositionStays: false);

            // Inverte a transformação: move o pontoDePegada para coincidir com o ponto do jogador
            novaInstancia.transform.localPosition = -pontoDePegada.localPosition;
            novaInstancia.transform.localRotation = Quaternion.Inverse(pontoDePegada.localRotation);
        }
        Animator animator = GetComponentInParent<Animator>();
        animator.SetBool("TemFerramenta", true);
        ferramentaAtual = novaInstancia;
    }



}



