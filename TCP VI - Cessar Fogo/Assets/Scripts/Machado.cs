using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machado : MonoBehaviour, IFerramenta
{
    [SerializeField] private GameObject areaDeAtaque;
    [SerializeField] private float tempoAtivo = 0.3f;

    private bool podeAtacar = true;

    private void Start()
    {
        if (areaDeAtaque != null)
            areaDeAtaque.SetActive(false);
    }

    public void Usar()
    {
        if (podeAtacar)
            StartCoroutine(Ataque());
    }

    private IEnumerator Ataque()
    {
        podeAtacar = false;

        //iniciar a animação de ataque
        //anim.SetTrigger("Atacar");

        if (areaDeAtaque != null)
            areaDeAtaque.SetActive(true);

        //tocar um som de machado
        //audioSource.PlayOneShot(somDeImpacto);

        yield return new WaitForSeconds(tempoAtivo);

        if (areaDeAtaque != null)
            areaDeAtaque.SetActive(false);

        // Pequeno cooldown opcional antes de poder atacar novamente (pode ajustar o valor)
        yield return new WaitForSeconds(0.2f);

        podeAtacar = true;
    }
}


