using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JatoArea : MonoBehaviour
{
    [SerializeField] private float intensidadeResfriamento = 10f;

    private void OnTriggerStay(Collider other)
    {
        BaseFireCell fogo = other.GetComponent<BaseFireCell>();
        if (fogo != null)
        {
            fogo.Resfriar(intensidadeResfriamento * Time.deltaTime);
        }
    }
}
