using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{

    ScriptableUnit unitData;

    [SerializeField] TextMeshProUGUI userID;
    [SerializeField] string playerID;

    [SerializeField] GameObject roundInfoUI;
    [SerializeField] GameObject settingWindow;
    [SerializeField] GameObject synergyContents;
    [SerializeField] GameObject rankingContents;

    [SerializeField] TextMeshProUGUI RoundInfoNum;
    [SerializeField] TextMeshProUGUI RoundStepNum;
    [SerializeField] TextMeshProUGUI expansionGold;
    [SerializeField] TextMeshProUGUI userGold;
    [SerializeField] TextMeshProUGUI gachaWeponGold;
    [SerializeField] TextMeshProUGUI gachaUnitGold;

    protected int gold;
    protected int gachaWeponGoldValue;
    protected int gachaUnitGoldValue;
    protected int expansionGoldValue;
    protected int expansionLV;
    protected int deployedUnit;
    protected int roundStepNumber;
    protected int roundNumber;
    protected bool IsESC { get; set; }
    protected bool IsSynergy { get; set; }
    protected bool IsRanking { get; set; }


    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }
    protected void Start()
    {
        unitData = FindObjectOfType<ScriptableUnit>();
        //userID.text = unitData.GetUnitName; // user data
        userID.text = playerID;
        playerID = userID.text; // save user id
        gold = 100; // user data

        // °íÁ¤ ÁöºÒ ºñ¿ë
        expansionGoldValue = 10;
        gachaWeponGoldValue = 3;
        gachaUnitGoldValue = 4;
        gachaWeponGold.text = (gachaWeponGoldValue + " G").ToString();
        gachaUnitGold.text = (gachaUnitGoldValue + " G").ToString();
        expansionGold.text = (expansionGoldValue + " G").ToString();

        IsESC = false;
        IsSynergy = false;
        IsRanking = false;

        // data ¹Þ¾Æ¿Ã °Í
        expansionLV = 1;
        deployedUnit = 3;
        roundStepNumber = 3;
        roundNumber = 1;


    }
    protected void Update()
    {
        if (gold <= 0) gold = 0;
        userGold.text = (gold + " G").ToString();

        // Game Setting
        if (Input.GetKeyDown(KeyCode.Escape)) SettingMenuESC();

        SynergyContents();
        RankingContents();
        Debug.Log("local Position X : " + synergyContents.transform.localPosition.x);
        Debug.Log("Position X : " + synergyContents.transform.position.x);

        // Round Info
        RoundInfoNum.text = (expansionLV + " / " + deployedUnit).ToString();
        RoundStepNum.text = (roundNumber + "-" + roundStepNumber).ToString();
    }


    // Setting Menu
    #region Setting Menu
    public void OnSettingMenu()
    {
        settingWindow.gameObject.SetActive(true);
    }
    public void OffSettingMenu()
    {
        settingWindow.gameObject.SetActive(false);
    }
    protected void SettingMenuESC()
    {
        if (!IsESC)
        {
            IsESC = true;
            OnSettingMenu();
        }
        else
        {
            IsESC = false;
            OffSettingMenu();
        }
    }
    public void Surrender()
    {
        Debug.Log("Ç×º¹");
    }
    #endregion
    protected bool IsRound;
    // Round Info Menu
    #region Setting Menu
    public void OnRoundInfoMenu()
    {
        roundInfoUI.gameObject.SetActive(true);
    }
    public void OffRoundInfoMenu()
    {
        roundInfoUI.gameObject.SetActive(false);
    }
    protected void Round()
    {
        if (!IsRound)
        {
            IsRound = true;
            OnRoundInfoMenu();
        }
        else
        {
            IsRound = false;
            OffRoundInfoMenu();
        }
    }
    #endregion

    // Synergy Contents UI
    #region Synergy Contents UI   
    protected void SynergyContents()
    {
        if (IsSynergy)
        {
            if (synergyContents.transform.position.x >= -7f) return;
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x + 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 50f);

        }
        else
        {
            if (synergyContents.transform.position.x <= -10.2f) return;
            synergyContents.transform.localPosition = Vector2.Lerp(synergyContents.transform.localPosition, new Vector2(synergyContents.transform.localPosition.x - 10f, synergyContents.transform.localPosition.y), Time.deltaTime * 50f);
        }
    }
    public void OnOffSynergyContents()
    {
        if (IsSynergy)
        {
            IsSynergy = false;
        }
        else
        {
            IsSynergy = true;
        }
    }
    #endregion

    // Ranking Contesnt UI
    #region Ranking Contesnt UI   

    protected void RankingContents()
    {
        if (IsRanking)
        {
            if (rankingContents.transform.position.x <= 7f) return;
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x - 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 50f);
        }
        else
        {
            if (rankingContents.transform.position.x >= 10.2f) return;
            rankingContents.transform.localPosition = Vector2.Lerp(rankingContents.transform.localPosition, new Vector2(rankingContents.transform.localPosition.x + 10f, rankingContents.transform.localPosition.y), Time.deltaTime * 50f);
        }
    }

    public void OnOffRankingContents()
    {
        if (IsRanking)
        {
            IsRanking = false;
        }
        else
        {
            IsRanking = true;
        }
    }
    // À¯ÀúÀÇ ¶óÀÌÇÁ ¹Þ¾Æ¿Í¼­ ³ôÀº ¼ø¼­´ë·Î Á¤·Ä
    // ÀÌ¹ÌÁö fill mode
    #endregion

    // Gacha Wepon UI
    protected int MixRandomWepon;
    public void OnWeponGacha()
    {
        if (gold <= gachaWeponGoldValue) return;
        MixRandomWepon = Random.Range(0, 4);
        gold -= gachaWeponGoldValue;
    }
    // Gacha Unit UI
    protected int MixRandomUnit;
    public void OnUnitGacha()
    {        
        if (gold <= gachaUnitGoldValue) return;
        MixRandomUnit = Random.Range(0, 21); // »ÌÀ» ¶§¸¶´Ù È®·ü ·£´ý
        gold -= gachaUnitGoldValue;
    }

    // Faction Expansion UI
    public void OnFactionExpansion()
    {
        if (gold <= expansionGoldValue) return;
        gold -= expansionGoldValue;
    }

    protected void WeponRandomCalculation()
    {
        // 25%
        // 1¼º 4Á¾
        // safety zone ¿¡ ¹èÄ¡
        // ¿¹¿ÜÃ³¸® : ¿©À¯ °ø°£ ¾øÀ½
        // ¸Ê ½ºÅ©¸³Æ® Âü°í
    }
    protected void UnitRandomCalculation()
    {
        // »ÌÀº À¯´ÖÀº »Ì±â È®·ü¿¡¼­ Á¦¿Ü
        // 1¹ø »Ì°í ³ª¸é maxValue - 1
        // ÆÇ¸Å µÈ À¯´Ö -> »Ì±â È®·ü¿¡ Ãß°¡
        // 2¼º ¿ÀÅ© ÇÏ³ª ÆÇ¸Å -> 1¼º ¿ÀÅ© 2°³ Áõ°¡
        // gachaUnit/maxUnit
        
        
        int[] RadomUnitList;


        // ÇØ´ç µ¥ÀÌÅÍ¿¡ ¸Â´Â ¹øÈ£ÀÇ À¯´Ö Á¦°ø
        int GetRandomUnit = Random.Range(0, 22);
        // ÀÌ¹Ì »ÌÀº À¯´Ö Áßº¹ X -> »ÌÀº À¯´Ö »èÁ¦

        switch (GetRandomUnit)
        {            
            case 1:
                Debug.Log("À¯´Ö »Ì±â - 1");
                break;
            case 2:
                Debug.Log("À¯´Ö »Ì±â - 2");
                break;
            case 3:
                Debug.Log("À¯´Ö »Ì±â - 3");
                break;
            case 4:
                Debug.Log("À¯´Ö »Ì±â - 4");
                break;
            case 5:
                Debug.Log("À¯´Ö »Ì±â - 5");
                break;
            case 6:
                Debug.Log("À¯´Ö »Ì±â - 6");
                break;
            case 7:
                Debug.Log("À¯´Ö »Ì±â - 7");
                break;
            case 8:
                Debug.Log("À¯´Ö »Ì±â - 8");
                break;
            case 9:
                Debug.Log("À¯´Ö »Ì±â - 9");
                break;
            case 10:
                Debug.Log("À¯´Ö »Ì±â - 10");
                break;
            case 11:
                Debug.Log("À¯´Ö »Ì±â - 11");
                break;
            case 12:
                Debug.Log("À¯´Ö »Ì±â - 12");
                break;
            case 13:
                Debug.Log("À¯´Ö »Ì±â - 13");
                break;
            case 14:
                Debug.Log("À¯´Ö »Ì±â - 14");
                break;
            case 15:
                Debug.Log("À¯´Ö »Ì±â - 15");
                break;
            case 16:
                Debug.Log("À¯´Ö »Ì±â - 16");
                break;
            case 17:
                Debug.Log("À¯´Ö »Ì±â - 17");
                break;
            case 18:
                Debug.Log("À¯´Ö »Ì±â - 18");
                break;
            case 19:
                Debug.Log("À¯´Ö »Ì±â - 19");
                break;
            case 20:
                Debug.Log("À¯´Ö »Ì±â - 20");
                break;
            case 21:
                Debug.Log("À¯´Ö »Ì±â - 21");

                break;
        }
    }

    // Áø¿µ Exp °¡ max ¸é Áø¿µ level up
    protected Slider FactionExp;
    protected int FactionExpValue;
    protected TextMeshProUGUI FactionLeveltext;
    protected int FactionLevelValue;
    protected void FactionLevel()
    {
        FactionExp.value = FactionExpValue;
    }

    // Game Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
