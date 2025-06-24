using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JogadorVida : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 100;
    private int vidaAtual;

    [Header("Feedback de Dano")]
    [SerializeField] private Image telaDano; // UI Overlay vermelho
    [SerializeField] private float tempoFeedback = 0.5f;

    //[Header("Som de dano")]
    // [SerializeField] private AudioSource somDano;

    private void Start()
    {
        vidaAtual = vidaMaxima;

        if (telaDano != null)
            telaDano.color = new Color(1, 0, 0, 0);
    }

    private void Update()
    {
        Debug.Log(vidaAtual);
    }

    public void ReceberDano(int quantidade)
    {
        vidaAtual -= quantidade;
        if (vidaAtual <= 0)
        {
            Morrer();
        }
        else
        {
            StartCoroutine(FeedbackDano());
        }

        // Tocar som de dano aqui
        // somDano.Play();
    }

    private IEnumerator FeedbackDano()
    {
        if (telaDano != null)
        {
            telaDano.color = new Color(1, 0, 0, 0.6f);
            yield return new WaitForSeconds(tempoFeedback);
            telaDano.color = new Color(1, 0, 0, 0f);
        }
    }

    private void Morrer()
    {
        Debug.Log($"{name} morreu!");
        // adicionar aqui o que acontecer quando morrer (respawn, game over, etc.)
    }
}

