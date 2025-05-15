using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush_FireCell : BaseFireCell
{
    [SerializeField] private float raio_propagaçao;
    [SerializeField] private float canche_propagacao;
    [SerializeField] private float delay_propagacao;


    protected override void Start()
    {
        propagationRadius = raio_propagaçao;
        propagationChance = canche_propagacao;
        base.Start();
    }
}
