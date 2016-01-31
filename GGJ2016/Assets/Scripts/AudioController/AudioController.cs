using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    #region Static Data
    private static GameObject _go;
    public static AudioController _instance;
    public static AudioController instance
    {
        get
        {
            if (_instance == null)
            {
                _go = new GameObject("Audio Controller", typeof(AudioController));
                DontDestroyOnLoad(_go);
                _instance = _go.GetComponent<AudioController>();
            }
            return _instance;
        }
    }
    #endregion
    #region PublicData
    public AudioClip[] _audioClips;
    public AudioClip[] _effects;

    public AudioSource bgm;
    public AudioSource sfx;

    #endregion

    public void Initialize()
    {
        Load();
        GameObject __bgm = new GameObject("BGM", typeof(AudioSource));
        GameObject __sfx = new GameObject("SFX", typeof(AudioSource));
        __bgm.transform.SetParent(transform);
        __sfx.transform.SetParent(transform);
        bgm = __bgm.GetComponent<AudioSource>();
        sfx = __sfx.GetComponent<AudioSource>();
        bgm.loop = true;
        sfx.loop = false;
        bgm.volume = 0.5f;
    }

    public void PlayMain()
    {
        if (bgm.isPlaying)
            bgm.Stop();
        bgm.clip = _audioClips[0];
        bgm.Play();
    }

    public void PlayButton()
    {
        sfx.clip = _effects[0];
        sfx.Play();
    }

    public void PlayVictory()
    {
        sfx.clip = _effects[1];
        sfx.Play();
    }

    public void PlayKill()
    {
        sfx.clip = _effects[2];
        sfx.Play();
    }

    public void PlayGetItem()
    {
        sfx.clip = _effects[3];
        sfx.Play();
    }

    public void Load()
    {
        _effects = Resources.LoadAll<AudioClip>("SFX");
        _audioClips = Resources.LoadAll<AudioClip>("BGM");
    }
}
