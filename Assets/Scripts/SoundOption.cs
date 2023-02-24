using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class MyDictionaryEntry
{
    public string key;
    public AudioClip value;
}
public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider[] soundSlider; // MASTER, BGM, SFX


    public List<MyDictionaryEntry> myDictEntries;

    // bgm
    // LobbyBgm, IngameBgm, BossBgm, MatchingBgm

    // ȿ����
    // ���� ���� �� ���߼Ҹ� IngameStartSFX
    //SFXPlay("IngameStartSFX");
    // ��Ī ������ ����  MatchingSFX
    //SFXPlay("MatchingSFX");
    // ���� ȿ���� MergeSFX
    //SFXPlay("MergeSFX");
    // �ó��� Ȱ��ȭ ���� SynergySFX
    //SFXPlay("SynergySFX");
    // ���� ��ġ���� DropSFX
    //SFXPlay("DropSFX");
    // ���� ��ȯ ���� BuySFX
    //SFXPlay("BuySFX");
    // ���� �Ǹ� ���� SellSFX
    //SFXPlay("SellSFX");
    // ���� �Ⱦ� ���� SelectSFX
    //SFXPlay("SelectSFX");
    // �˾����� ClickSFX
    //SFXPlay("ClickSFX");


    public Dictionary<string, AudioClip> ClipDictionary;

    private void OnValidate()
    {
        ClipDictionary = new Dictionary<string, AudioClip>();
        foreach (var entry in myDictEntries)
        {
            ClipDictionary.Add(entry.key, entry.value);
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
        bgmPlay("LobbyBgm");
    }


    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MASTER", Mathf.Log10(soundSlider[0].value) * 20f);
    }
    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(soundSlider[1].value) * 20f);
    }
    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(soundSlider[2].value) * 20f);
    }
    public void bgmPlay(string bgmName)
    {
        this.gameObject.AddComponent<AudioSource>();
        AudioSource audiosource;
        AudioClip clip = null;
        if (!gameObject.TryGetComponent<AudioSource>(out audiosource))
        {
            audiosource = this.gameObject.AddComponent<AudioSource>();
        }
       
        if(ClipDictionary.TryGetValue(bgmName, out clip))
        {
            audiosource.clip = clip;
            audiosource.Play();
        }
        else
        {
            Debug.Log("�ش��ϴ� ���尡 �����ϴ�.");
        }
        audiosource.loop = true;

    }


    public void SFXPlay(string sfxName)
    {
        AudioSource[] audiosources = this.gameObject.GetComponents<AudioSource>();
        AudioClip clip = null;
        ClipDictionary.TryGetValue(sfxName, out clip);

        foreach (AudioSource source in audiosources)
        {
            if (source.clip == clip)
            {
                source.Play();
                return;
            }
        }

        AudioSource audiosource = this.gameObject.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
    }

    public void SFXPlay(string sfxName, GameObject go)
    {
        AudioSource[] audiosources = go.GetComponents<AudioSource>();
        AudioClip clip = null;
        ClipDictionary.TryGetValue(sfxName, out clip);

        foreach (AudioSource source in audiosources)
        {
            if (source.clip == clip)
            {
                source.Play();
                return;
            }
        }

        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
    }
}
