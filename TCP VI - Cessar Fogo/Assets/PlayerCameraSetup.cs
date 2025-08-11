using UnityEngine;
using Cinemachine;

public class PlayerCameraSetup : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public Transform followTarget;

    void Start()
    {
        if (freeLookCam != null && followTarget != null)
        {
            freeLookCam.Follow = followTarget;
            freeLookCam.LookAt = followTarget;
        }
    }
}
