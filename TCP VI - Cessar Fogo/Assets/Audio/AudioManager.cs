using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioManager instance;

    public AudioSource[] Music;
    public AudioSource[] SFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PlayMusic(int LevelMusic)
    {
        Music[LevelMusic].Play();
    }
    public void StopMusic(int LevelMusic)
    {
        Music[LevelMusic].Stop();
    }
    public void PlaySFX(int COD)
    {
        SFX[COD].Play();
    }
    public void StopSFX(int COD)
    {
        SFX[COD].Stop();
    }
    public void StopAll()
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            SFX[i].Stop();
        }
    }
}