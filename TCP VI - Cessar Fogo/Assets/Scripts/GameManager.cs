using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private void Start()
    {
        var manager = PlayerInputManager.instance;

        // Spawna manualmente os dois jogadores com seus respectivos controles
        manager.DisableJoining();

        // Jogador 1
        PlayerInput player1 = PlayerInput.Instantiate(
            player1Prefab,
            playerIndex: 0,
            controlScheme: null,
            pairWithDevice: Gamepad.all.Count > 0 ? Gamepad.all[0] : null);

        player1.transform.position = spawnPoint1.position;
        player1.transform.rotation = spawnPoint1.rotation;

        // Jogador 2
        PlayerInput player2 = PlayerInput.Instantiate(
            player2Prefab,
            playerIndex: 1,
            controlScheme: null,
            pairWithDevice: Gamepad.all.Count > 1 ? Gamepad.all[1] : null);

        player2.transform.position = spawnPoint2.position;
        player2.transform.rotation = spawnPoint2.rotation;
    }
}



