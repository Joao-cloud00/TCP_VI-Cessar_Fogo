using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enxada : MonoBehaviour, IFerramenta
{
    [SerializeField] private GameObject areaDeAtaque; // Objeto com collider trigger
    [SerializeField] private float tempoAtivo = 0.2f;
    //public Animator anim;

    public void Usar()
    {
        //anim?.SetTrigger("Cavar");
        StartCoroutine(AtivarArea());
    }

    private System.Collections.IEnumerator AtivarArea()
    {
        areaDeAtaque.SetActive(true);
        yield return new WaitForSeconds(tempoAtivo);
        areaDeAtaque.SetActive(false);
    }
}



