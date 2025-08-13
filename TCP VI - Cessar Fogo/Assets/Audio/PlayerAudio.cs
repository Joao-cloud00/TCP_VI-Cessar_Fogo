using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] public int pularCOD;
    [SerializeField] public int andarCOD;
    [SerializeField] public int correrCOD;
    [SerializeField] public int hurtCOD;
    [SerializeField] public int morrerCOD;
    [SerializeField] public int usarCOD;

    public void playSFX(int sfxCOD)
    {
        if (!AudioManager.instance.SFX[sfxCOD].isPlaying)
            AudioManager.instance.PlaySFX(sfxCOD);

    }
    public void stopSFX(int sfxCOD)
    {
        if (AudioManager.instance.SFX[sfxCOD].isPlaying)
            AudioManager.instance.StopSFX(sfxCOD);
        
    }
    public void stopAll()
    {
        AudioManager.instance.StopAll();
    }
}
