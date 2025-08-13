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

    [Header("Invulnerabilidade")]
    [SerializeField] private float tempoInvulneravel = 1f;
    private bool podeTomarDano = true;

    [Header("UI")]
    [SerializeField] private Image barraVida;

    Animator animator;
    private PlayerAudio PlayerAudio;


    //[Header("Som de dano")]
    // [SerializeField] private AudioSource somDano;

    private void Start()
    {

        vidaAtual = vidaMaxima;
        AtualizarBarraVida();
       animator = GetComponentInParent<Animator>();
       PlayerAudio = GetComponentInParent<PlayerAudio>();
        if (telaDano != null)
            telaDano.color = new Color(1, 0, 0, 0);
    }

    private void Update()
    {
        //Debug.Log(vidaAtual);
    }

    private void AtualizarBarraVida()
    {
        if (barraVida != null)
            barraVida.fillAmount = (float)vidaAtual / vidaMaxima;
    }


    public void ReceberDano(int quantidade)
    {
        if (!podeTomarDano) //|| vidaAtual <= 0)
            return;

        vidaAtual -= quantidade;
        animator.SetTrigger("Hurt");
        if (vidaAtual <= 0 )
        {

            Morrer();
            Debug.Log("Jogador Morreu");
        }
        else
        {
            vidaAtual -= quantidade;
            AtualizarBarraVida();

            StartCoroutine(FeedbackDano());
            StartCoroutine(InvulnerabilidadeTemporaria());
        }

        // Som ou animação de dano futuramente
        // somDano.Play();
    }


    private IEnumerator FeedbackDano()
    {
        if (telaDano != null)
        {

            PlayerAudio.playSFX(PlayerAudio.hurtCOD);
            telaDano.color = new Color(1, 0, 0, 0.6f);
            yield return new WaitForSeconds(tempoFeedback);
            telaDano.color = new Color(1, 0, 0, 0f);
        }
    }

    private IEnumerator InvulnerabilidadeTemporaria()
    {
        podeTomarDano = false;
        yield return new WaitForSeconds(tempoInvulneravel);
        podeTomarDano = true;
    }


    private void Morrer()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (!playerMovement.controleAtivo) return;
        playerMovement.SetControleAtivo(false);
        Debug.Log($"{name} morreu!");
        //vidaAtual = 0;
        AtualizarBarraVida();
        animator.SetBool("Morreu",true);
        PlayerAudio.stopAll();
        PlayerAudio.playSFX(PlayerAudio.morrerCOD);
        StartCoroutine(GameOver());

        // adicionar aqui o que acontecer quando morrer (respawn, game over, etc.)
    }

    private IEnumerator GameOver()
    {

        yield return new WaitForSeconds(5);
        GameManager.Instance.Derrota();
        
    }

}

