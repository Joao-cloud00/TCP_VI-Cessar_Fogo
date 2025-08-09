using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mesa : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag de jogador
        if (other.CompareTag("Player"))
        {
            // Procura o Canvas dentro do jogador
            ColetavelFerramenta canvasHUD = other.GetComponentInChildren<ColetavelFerramenta>(true);

            if (canvasHUD != null)
            {
                canvasHUD.gameObject.SetActive(true);
                other.GetComponent<PlayerMovement>().SetControleAtivo(false);
            }
            else
            {
                Debug.LogWarning("CanvasHUD não encontrado no jogador " + other.name);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ColetavelFerramenta canvasHUD = other.GetComponentInChildren<ColetavelFerramenta>(true);

            if (canvasHUD != null)
            {
                canvasHUD.gameObject.SetActive(false);
                other.GetComponent<PlayerMovement>().SetControleAtivo(true);

            }
        }
    }
    
}
