using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulaDeAgua : MonoBehaviour
{
    [SerializeField] private float intensidadeResfriamento = 5f;

    private void OnParticleCollision(GameObject other)
    {
        BaseFireCell fogo = other.GetComponent<BaseFireCell>();
        if (fogo != null)
        {
            fogo.Resfriar(intensidadeResfriamento * Time.deltaTime);
        }
    }
}

