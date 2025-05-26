using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JogadorFerramentas : MonoBehaviour
{
    [SerializeField] private GameObject ferramentaAtual;

    public void OnUseTool(InputAction.CallbackContext context)
    {
        if (context.performed)
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


