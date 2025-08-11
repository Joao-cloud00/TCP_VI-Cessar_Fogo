using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerUIInitializer : MonoBehaviour
{
    public GameObject eventSystemPrefab; // Prefab do EventSystem com InputSystemUIInputModule
    public Canvas playerCanvas; // Canvas do HUD/Menu desse jogador

    private void Start()
    {
        // Cria o EventSystem para esse jogador
        var esObj = Instantiate(eventSystemPrefab);
        var uiModule = esObj.GetComponent<InputSystemUIInputModule>();
        var playerInput = GetComponent<PlayerInput>();

        // Liga o EventSystem a esse jogador
        uiModule.actionsAsset = playerInput.actions;
        uiModule.move = InputActionReference.Create(playerInput.actions["Navigate"]);
        uiModule.submit = InputActionReference.Create(playerInput.actions["Submit"]);
        uiModule.cancel = InputActionReference.Create(playerInput.actions["Cancel"]);

        // Garante que esse EventSystem controla só esse Canvas
        esObj.transform.SetParent(playerCanvas.transform, false);
    }
}
