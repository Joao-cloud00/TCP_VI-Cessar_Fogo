using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Manager : MonoBehaviour
{
    private void Start()
    {
        CinemachineFreeLook[] allCams = FindObjectsOfType<CinemachineFreeLook>();
        foreach (var cam in allCams)
        {
            if (cam.gameObject.layer != gameObject.layer)
            {
                cam.Priority = 0; // for�a desativa��o das c�meras de outro jogador
            }
        }
    }


}
