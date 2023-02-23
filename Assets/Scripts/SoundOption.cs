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

    // 효과음
    // 게임 시작 후 군중소리 IngameStartSFX
    // 매칭 성공시 사운드  MatchingSFX
    // 머지 효과음 MergeSFX
    // 시너지 활성화 사운드 SynergySFX
    // 유닛 배치사운드 DropSFX
    // 유닛 소환 사운드 BuySFX
    // 유닛 판매 사운드 SellSFX
    // 유닛 픽업 사운드 SelectSFX
    // 팝업사운드 ClickSFX

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
<<<<<<< Updated upstream
        this.gameObject.AddComponent<AudioSource>();
=======
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
            Debug.Log("해당하는 사운드가 없습니다.");
        }
        audiosource.loop = true;

>>>>>>> Stashed changes
    }


    public void SFXPlay(string sfxName)
    {
<<<<<<< Updated upstream
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.Play();
=======
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
>>>>>>> Stashed changes

        AudioSource audiosource = this.gameObject.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
    }


   

}
