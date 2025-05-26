using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // Garante que temos o PlayerInputManager na cena
        var manager = FindObjectOfType<PlayerInputManager>();

        // Instancia dois jogadores automaticamente
        for (int i = 0; i < 2; i++)
        {
            manager.JoinPlayer(i, -1, null, Gamepad.all[i]);
        }
    }
}


