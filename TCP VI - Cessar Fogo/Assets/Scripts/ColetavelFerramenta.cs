using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColetavelFerramenta : MonoBehaviour
{
    //[SerializeField] private GameObject ferramentaPrefab;
    //fazer com que o ferramenta Prefab seja selecionado no canvas
    JogadorFerramentas jogador;
    [SerializeField]
    GameObject FerramentasCanva;
    [SerializeField]
    GameObject[] FerramentasImagem;

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
    public void AtivarImagem(int numFerramenta)
    {

        FerramentasCanva.SetActive(true);
        for (int i = 0; FerramentasImagem.Length > i; i++)
        {
            FerramentasImagem[i].SetActive(false);
        }
        FerramentasImagem[numFerramenta].SetActive(true);
    }
}
