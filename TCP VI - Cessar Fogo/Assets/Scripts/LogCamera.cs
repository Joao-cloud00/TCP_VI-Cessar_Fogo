using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCamera : MonoBehaviour
{
    //private void Update()
    //{
    //    var brain = GetComponent<Cinemachine.CinemachineBrain>();
    //    if (brain != null && brain.ActiveVirtualCamera != null)
    //    {
    //        Debug.Log($"Câmera ativa: {brain.ActiveVirtualCamera.Name}");
    //    }
    //}

    void Update()
    {
        var brain = GetComponent<Cinemachine.CinemachineBrain>();
        if (brain != null && brain.ActiveVirtualCamera != null)
        {
            Debug.Log($"{name}: Câmera ativa: {brain.ActiveVirtualCamera.Name}");
        }
    }

}
