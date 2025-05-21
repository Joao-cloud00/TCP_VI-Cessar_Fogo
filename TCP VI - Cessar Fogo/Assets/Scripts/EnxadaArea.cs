using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnxadaArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GramaInflamavel grama = other.GetComponent<GramaInflamavel>();
        if (grama != null)
        {
            grama.Cavar();
        }
    }
}
