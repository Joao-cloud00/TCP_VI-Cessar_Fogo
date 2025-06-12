using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochilaAgua : MonoBehaviour, IFerramenta
{
    [SerializeField] private ParticleSystem jatoDeAgua;
    [SerializeField] private GameObject jatoArea;

    private bool usando = false;

    private void Start()
    {
        if (jatoArea != null)
            jatoArea.SetActive(false);
    }

    public void Usar()
    {
        usando = true;
        if (jatoDeAgua != null && !jatoDeAgua.isPlaying)
        {
            jatoDeAgua.Play();
            if (jatoArea != null)
                jatoArea.SetActive(true);
        }
    }

    public void PararUso()
    {
        usando = false;
        if (jatoDeAgua != null && jatoDeAgua.isPlaying)
        {
            jatoDeAgua.Stop();
            if (jatoArea != null)
                jatoArea.SetActive(false);
        }
    }

    private void Update()
    {
        // (Opcional) efeito contínuo se precisar
    }
}


