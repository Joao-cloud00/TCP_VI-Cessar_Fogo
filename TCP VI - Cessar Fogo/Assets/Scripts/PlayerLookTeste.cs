using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerLookTeste : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private InputActionAsset inputActions; // Input Actions (arquivo .inputactions)
    [SerializeField] private string actionMapName = "Player"; // Nome do mapa de ações
    [SerializeField] private string lookActionName = "Look"; // Nome da ação de olhar

    [SerializeField] private float sensibilidadeX = 5f;
    [SerializeField] private float sensibilidadeY = 2f;

    private InputAction lookAction;

    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap(actionMapName);
        lookAction = actionMap.FindAction(lookActionName);
        lookAction.Enable();
    }

    private void OnDisable()
    {
        if (lookAction != null)
            lookAction.Disable();
    }

    private void Update()
    {
        if (freeLookCamera == null || lookAction == null) return;

        Vector2 input = lookAction.ReadValue<Vector2>();

        if (input.sqrMagnitude > 0.01f) // evita drift do analógico
        {
            freeLookCamera.m_XAxis.Value += input.x * sensibilidadeX * Time.deltaTime;
            freeLookCamera.m_YAxis.Value += input.y * sensibilidadeY * Time.deltaTime;
        }
    }
}
