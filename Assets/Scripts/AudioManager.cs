using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private float _mainVolume = 1f;
    private float _bgmVolume = 1f;
    private float _sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetMainVolume() => _mainVolume;
    public float GetBGMVolume() => _bgmVolume;
    public float GetSFXVolume() => _sfxVolume;

    public void SetMainVolume(float volume)
    {
        _mainVolume = volume;
    }

    public void SetBGMVolume(float volume)
    {
        _bgmVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = volume;
    }
}
