using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochilaAgua : MonoBehaviour, IFerramenta
{
    [SerializeField] private ParticleSystem jatoDeAgua;

    private bool usando = false;

    public void Usar()
    {
        usando = true;
        if (jatoDeAgua != null && !jatoDeAgua.isPlaying)
        {
            jatoDeAgua.Play();
        }
    }

    public void PararUso()
    {
        usando = false;
        if (jatoDeAgua != null && jatoDeAgua.isPlaying)
        {
            jatoDeAgua.Stop();
        }
    }

    private void Update()
    {
        // (Opcional) efeito contínuo se precisar
    }
}


