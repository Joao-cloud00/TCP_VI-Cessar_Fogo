using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreCameraTrigger : MonoBehaviour
{
    [SerializeField] private TorreCameraController torreCamera;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Proximo da torre");
        if (other.CompareTag("Player"))
        {
            var controle = other.GetComponent<PlayerMovement>();
            if (controle != null)
            {
                controle.torreCamera = torreCamera;
                controle.estaProximoDaTorre = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var controle = other.GetComponent<PlayerMovement>();
            if (controle != null)
            {
                controle.estaProximoDaTorre = false;
                torreCamera.DesativarCamera(); // força a saída da câmera
                controle.SetControleAtivo(true);
            }
        }
    }
}


