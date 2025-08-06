using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enxada : MonoBehaviour, IFerramenta
{
    [SerializeField] private GameObject areaDeAtaque; // precisa ser collider trigger
    [SerializeField] private float tempoAtivo = 0.2f;
    Animator anim;
    private void Start()
    {

        anim = GetComponentInParent<Animator>();
    }
    public void Usar()
    {
        anim.SetTrigger("Usou");
        anim.SetInteger("Item", 3);
        StartCoroutine(AtivarArea());
    }

    private System.Collections.IEnumerator AtivarArea()
    {
        areaDeAtaque.SetActive(true);
        yield return new WaitForSeconds(tempoAtivo);
        areaDeAtaque.SetActive(false);
    }
}



