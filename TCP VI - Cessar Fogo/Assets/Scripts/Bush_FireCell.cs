using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush_FireCell : BaseFireCell
{
    [SerializeField] private float raio_propagašao;
    [SerializeField] private float canche_propagacao;
    [SerializeField] private float delay_propagacao;


    protected override void Start()
    {
        propagationRadius = raio_propagašao;
        propagationChance = canche_propagacao;
        base.Start();
    }
}
