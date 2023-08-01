using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource bgmAudio;
    public AudioSource cvAudio;
    public AudioSource effectAudio;

    public void Awake()
    {
        Instance = this;
    }

    public void PlayBgm(string BgmName,bool loop = true)
    {
        bgmAudio.loop = loop;
        bgmAudio.Stop();
        bgmAudio.PlayOneShot(Resources.Load<AudioClip>("Sound/bgm/" + BgmName));
    }

    public void PlayCv(string CvName)
    {
        cvAudio.Stop();
        cvAudio.PlayOneShot(Resources.Load<AudioClip>("Sound/cv/" + CvName));
    }

    public void PlayEffect(string EffectName)
    {
        effectAudio.Stop();
        effectAudio.PlayOneShot(Resources.Load<AudioClip>("Sound/effect/" + EffectName));
    }

    public void BgmStop()
    {
        bgmAudio.Stop();
    }

    public void CvStop()
    {
        cvAudio.Stop();
    }

    public void EffectStop()
    {
        effectAudio.Stop();
    }
}