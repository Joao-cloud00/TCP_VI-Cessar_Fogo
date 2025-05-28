using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorFerramentas : MonoBehaviour
{
    [SerializeField] private GameObject ferramentaAtual;
    public Transform pontoFerramenta;

    public void OnUseTool(InputAction.CallbackContext context)
    {
        if (ferramentaAtual == null) return;

        IFerramenta ferramenta = ferramentaAtual.GetComponent<IFerramenta>();
        if (ferramenta == null) return;

        if (context.started)
        {
            ferramenta.Usar();
        }
        else if (context.canceled)
        {
            // S� chama PararUso se a ferramenta implementar esse m�todo
            if (ferramenta is MochilaAgua mochila)
            {
                mochila.PararUso();
            }
        }
    }

    public void EquiparNovaFerramenta(GameObject novaFerramentaPrefab)
    {
        if (ferramentaAtual != null)
            Destroy(ferramentaAtual);

        GameObject novaInstancia = Instantiate(novaFerramentaPrefab);
        Transform pontoDePegada = novaInstancia.transform.Find("PontoDePegada");

        if (pontoDePegada == null)
        {
            Debug.LogWarning("A ferramenta n�o tem um objeto filho chamado 'PontoDePegada'. Usando posi��o padr�o.");
            novaInstancia.transform.SetParent(pontoFerramenta);
            novaInstancia.transform.localPosition = Vector3.zero;
            novaInstancia.transform.localRotation = Quaternion.identity;
        }
        else
        {
            // Alinha a ferramenta com base no PontoDePegada
            novaInstancia.transform.SetParent(pontoFerramenta, worldPositionStays: false);

            // Inverte a transforma��o: move o pontoDePegada para coincidir com o ponto do jogador
            novaInstancia.transform.localPosition = -pontoDePegada.localPosition;
            novaInstancia.transform.localRotation = Quaternion.Inverse(pontoDePegada.localRotation);
        }

        ferramentaAtual = novaInstancia;
    }



}



