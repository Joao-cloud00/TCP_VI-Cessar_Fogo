using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelFerramenta : MonoBehaviour
{
    //[SerializeField] private GameObject ferramentaPrefab;
    //fazer com que o ferramenta Prefab seja selecionado no canvas
    JogadorFerramentas jogador;

    private void Start()
    {
        jogador = GetComponentInParent<JogadorFerramentas>();
    }

    
    public void EquiparFerramenta(GameObject ferramenta)
    {
        Debug.Log(ferramenta.name);
        jogador.EquiparNovaFerramenta(ferramenta);
        ColetavelFerramenta canvasHUD = GetComponentInChildren<ColetavelFerramenta>(true);

        if (canvasHUD != null)
        {
            canvasHUD.gameObject.SetActive(false);

            GetComponentInParent<PlayerMovement>().SetControleAtivo(true);
        }
    }
}

