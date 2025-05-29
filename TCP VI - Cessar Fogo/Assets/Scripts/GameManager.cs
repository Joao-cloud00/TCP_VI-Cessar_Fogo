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
        PlayerInputManager.instance.DisableJoining(); // evita jogador extra

        var player1 = PlayerInput.Instantiate(player1Prefab, 0, controlScheme: null, pairWithDevice: Gamepad.all[0]);
        player1.transform.position = spawnPoint1.position;

        Debug.Log($"Player 1: index = {player1.playerIndex}, controle = {player1.devices[0].displayName}");

        var player2 = PlayerInput.Instantiate(player2Prefab, 1, controlScheme: null, pairWithDevice: Gamepad.all[1]);
        player2.transform.position = spawnPoint2.position;

        Debug.Log($"Player 2: index = {player2.playerIndex}, controle = {player2.devices[0].displayName}");

    }
}




