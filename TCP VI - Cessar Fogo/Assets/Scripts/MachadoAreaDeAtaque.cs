using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachadoAreaDeAtaque : MonoBehaviour
{
    [SerializeField] private LayerMask layerObstaculos;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerObstaculos) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}

