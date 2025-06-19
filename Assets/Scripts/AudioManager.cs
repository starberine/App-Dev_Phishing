using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip mainMenuBGM;
    public AudioClip gameSceneBGM;           // added
    public AudioClip fishingCaughtSFX;       // added
    public AudioClip buttonPressSFX;
    public AudioClip fishStartSFX;
    public AudioClip fishFailSFX;
    public AudioClip bestiaryOpenSFX;
    public AudioClip bestiaryCloseSFX;
    public AudioClip bestiaryButtonSFX;
    public AudioClip portalTeleportSFX;
    public AudioClip playerJumpSFX;

    private float masterVolume = 1f;
    private float bgmVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
            PlayBGM(mainMenuBGM);
        else if (scene.name == "GameScene")
            PlayBGM(gameSceneBGM);
        else
            bgmSource.Stop();
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume * masterVolume;
        bgmSource.time = 2f; // optional skip
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume * masterVolume);
    }

    public void PlayFishingCaughtSFX()
    {
        PlaySFX(fishingCaughtSFX);
    }

    public void PlayButtonPressSFX()
    {
        PlaySFX(buttonPressSFX);
    }

    public void PlayFishStartSFX()
    {
        PlaySFX(fishStartSFX);
    }

    public void PlayFishFailSFX()
    {
        PlaySFX(fishFailSFX);
    }

    public void PlayBestiaryOpenSFX()
    {
        PlaySFX(bestiaryOpenSFX);
    }

    public void PlayBestiaryCloseSFX()
    {
        PlaySFX(bestiaryCloseSFX);
    }

    public void PlayBestiaryButtonSFX()
    {
        PlaySFX(bestiaryButtonSFX);
    }


    public void PlayPortalTeleportSFX()
    {
        PlaySFX(portalTeleportSFX);
    }

    public void PlayPlayerJumpSFX()
    {
        PlaySFX(playerJumpSFX);
    }




    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        UpdateVolumes();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        UpdateVolumes();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        UpdateVolumes();
    }

    public float GetMasterVolume() => masterVolume;
    public float GetBGMVolume() => bgmVolume;
    public float GetSFXVolume() => sfxVolume;

    private void UpdateVolumes()
    {
        if (bgmSource != null)
            bgmSource.volume = bgmVolume * masterVolume;
        if (sfxSource != null)
            sfxSource.volume = sfxVolume * masterVolume;
    }
}
