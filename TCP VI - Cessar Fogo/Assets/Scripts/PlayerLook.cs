using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private float sensibilidadeX = 5f;
    [SerializeField] private float sensibilidadeY = 2f;

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("input");
        Vector2 input = context.ReadValue<Vector2>();

        if (freeLookCamera != null)
        {
            Debug.Log("Entrou no IF");
            freeLookCamera.m_XAxis.Value += input.x * sensibilidadeX * Time.deltaTime;
            freeLookCamera.m_YAxis.Value += input.y * sensibilidadeY * Time.deltaTime;
        }
    }
}


