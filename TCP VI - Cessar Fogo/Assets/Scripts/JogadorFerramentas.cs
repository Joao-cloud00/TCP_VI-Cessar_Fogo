using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorFerramentas : MonoBehaviour
{
    [SerializeField] private GameObject ferramentaAtual;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IFerramenta ferramenta = ferramentaAtual?.GetComponent<IFerramenta>();
            ferramenta?.Usar();
        }
    }

    public void EquiparFerramenta(GameObject novaFerramenta)
    {
        ferramentaAtual = novaFerramenta;
    }
}

