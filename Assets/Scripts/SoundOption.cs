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
    // ��Ī ������ ����  MatchingSFX
    // ���� ȿ���� MergeSFX
    // �ó��� Ȱ��ȭ ���� SynergySFX
    // ���� ��ġ���� DropSFX
    // ���� ��ȯ ���� BuySFX
    // ���� �Ǹ� ���� SellSFX
    // ���� �Ⱦ� ���� SelectSFX
    // �˾����� ClickSFX

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
    }
    public void SFXPlay(string sfxName, GameObject go)
    {
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.Play();

    }

   

}
