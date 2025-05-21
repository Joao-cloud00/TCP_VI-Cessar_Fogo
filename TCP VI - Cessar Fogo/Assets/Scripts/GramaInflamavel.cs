using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramaInflamavel : BaseFireCell
{
    [SerializeField] private GameObject buracoPrefab;

    public void Cavar()
    {
        if (State != FireState.Extinguished)
        {
            // Cancela o fogo e cria um buraco no lugar
            State = FireState.Extinguished;
            UpdateVisuals();
        }

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Destroy(gameObject);
        Instantiate(buracoPrefab, pos, rot);
    }
}

