using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Instanciação de Jogadores")]
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    [Header("HUD")]
    public GameObject telaDerrota;
    public GameObject telaVitoria;
    public GameObject telaIniciar;

    private bool jogoFinalizado = false;

    private int totalFocos = 0;
    private int focosExtintos = 0;
    [SerializeField]
    private float chanceDeAcender = 0.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0;

        PlayerInputManager.instance.DisableJoining();

        var player1 = PlayerInput.Instantiate(player1Prefab, 0, controlScheme: null, pairWithDevice: Gamepad.all[0]);
        player1.transform.position = spawnPoint1.position;
        player1.actions["Start"].performed += ctx => IniciarJogo();
        var startAction1 = player1.actions["Start"];

        var player2 = PlayerInput.Instantiate(player2Prefab, 1, controlScheme: null, pairWithDevice: Gamepad.all[1]);
        player2.transform.position = spawnPoint2.position;
        player2.actions["Start"].performed += ctx => IniciarJogo();
        var startAction2 = player2.actions["Start"];

        Debug.Log($"Start action p1: {startAction1 != null}");

    }

    private void IniciarJogo()
    {
        if (!telaIniciar) return; // Já começou

        telaIniciar.SetActive(false);
        Time.timeScale = 1;

        // Pega todos os fogos na cena
        FireTester[] fires = FindObjectsOfType<FireTester>(false);
        Debug.Log(fires.Length);

        foreach (var fire in fires)
        {
            if (Random.value <= chanceDeAcender)
            {
                fire.Ignite();
            }
        }
    }


    private void Update()
    {
        //    if (Input.GetKeyDown(KeyCode.I))
        //    {
        //        telaIniciar.SetActive(false);
        //        Time.timeScale = 1;
        //    }
    }

    // Chamada quando um jogador morre
    public void Derrota()
    {
        if (jogoFinalizado) return;

        jogoFinalizado = true;
        Debug.Log("Game Over!");
        if (telaDerrota != null) telaDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    // Chamada quando todos os incêndios forem extintos
    public void Vitoria()
    {
        if (jogoFinalizado) return;

        jogoFinalizado = true;
        Debug.Log("Vitória!");
        if (telaVitoria != null) telaVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    // Chamado no Start() de cada BaseFireCell
    public void RegistrarFoco()
    {
        totalFocos++;
    }

    // Chamado quando um foco é extinto
    public void FocoExtinto()
    {
        focosExtintos++;
        if (focosExtintos >= totalFocos)
        {
            Vitoria();
        }
    }

    // Botão de reiniciar
    public void ReiniciarCena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}





