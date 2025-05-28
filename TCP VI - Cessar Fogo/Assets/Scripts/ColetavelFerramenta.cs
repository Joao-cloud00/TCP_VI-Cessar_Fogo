using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetavelFerramenta : MonoBehaviour
{
    [SerializeField] private GameObject ferramentaPrefab;

    private void OnTriggerEnter(Collider other)
    {
        JogadorFerramentas jogador = other.GetComponent<JogadorFerramentas>();
        if (jogador != null)
        {
            jogador.EquiparNovaFerramenta(ferramentaPrefab);
            Destroy(gameObject); // Remove a ferramenta do cenário após pegar
        }
    }
}

