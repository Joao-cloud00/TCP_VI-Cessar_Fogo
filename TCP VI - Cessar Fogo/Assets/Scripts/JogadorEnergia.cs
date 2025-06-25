using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JogadorEnergia : MonoBehaviour
{
    [Header("Energia")]
    public float energiaMaxima = 100f;
    public float energiaAtual;
    public float taxaRecuperacao = 10f;

    [Header("HUD")]
    [SerializeField] private Image barraEnergia;


    private void Start()
    {
        energiaAtual = energiaMaxima;
        AtualizarBarraEnergia();

    }

    private void Update()
    {
        // Regeneração contínua (se não estiver cheia)
        if (energiaAtual < energiaMaxima)
        {
            energiaAtual += taxaRecuperacao * Time.deltaTime;
            energiaAtual = Mathf.Min(energiaAtual, energiaMaxima);
            AtualizarBarraEnergia();
        }
    }

    public bool TemEnergia(float custo)
    {
        return energiaAtual >= custo;
    }

    private void AtualizarBarraEnergia()
    {
        if (barraEnergia != null)
        {
            barraEnergia.fillAmount = energiaAtual / energiaMaxima;
        }
    }

    public void ConsumirEnergia(float custo)
    {
        energiaAtual -= custo;
        energiaAtual = Mathf.Clamp(energiaAtual, 0, energiaMaxima);
    }
}

