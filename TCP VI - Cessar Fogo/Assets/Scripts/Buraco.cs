using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : BaseFireCell
{
    protected override void Start()
    {
        combustivelAtual = 0f;
        propagationChance = 0f;
        State = FireState.Extinguished;
        UpdateVisuals();
    }

    public override void Ignite() { } // Nunca pega fogo
    public override void Extinguish() { } // Não faz nada

    public override bool PodeReceberPropagacao()
    {
        return Random.value < 0.1f;
    }

}

