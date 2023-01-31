using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//using Photon.Pun;
//using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userID;
    [SerializeField] string playerID;

    [SerializeField] Button roundInfoOn;
    [SerializeField] Button roundInfoOff;
    [SerializeField] GameObject roundInfoUI;
    [SerializeField] Button settingExitUI;
    [SerializeField] GameObject settingWindow;
    [SerializeField] Button synergyUI;
    [SerializeField] GameObject synergyContents;
    [SerializeField] Button rankingUI;
    [SerializeField] GameObject rankingContents;
    [SerializeField] TextMeshProUGUI prossessionGold;

    protected int gold;
    protected int gachaWeponGold;
    protected int gachaUnitGold;
    protected bool IsESC { get; set; }
    protected bool IsSynergy { get; set; }
    protected bool IsRanking { get; set; }
 

    protected void Awake()
    {
        DontDestroyOnLoad(this);
    }
    protected void Start()
    {
        userID.text = "User".ToString(); // user data
        playerID = userID.text; // save user id
        gold = 1000; // user data

        IsESC = false;
        IsSynergy = false;
        IsRanking = false;
        

    }
    protected void Update()
    {
        // Prossession Gold        
        if (gold <= 0) gold = 0;
        prossessionGold.text = (gold + " G").ToString();

        // Game Setting
        if (Input.GetKeyDown(KeyCode.Escape)) SettingMenuESC();
       
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
    public void OnSynergyContents()
    {
        if (synergyUI.transform.position.x >= 180f) return;
        synergyUI.transform.position = Vector2.Lerp(synergyUI.transform.position, new Vector2(180f, 100f), Time.deltaTime * 3f);
    }
    public void OffSynergyContents()
    {
        if (synergyUI.transform.position.x <= -150f) return;
        synergyUI.transform.position = Vector2.Lerp(synergyUI.transform.position, new Vector2(-150f, 100f), Time.deltaTime * 3f);
    }
    protected void SynergyContents()
    {
        if (!IsSynergy)
        {
            IsSynergy = true;
            OnSynergyContents();
        }
        else
        {
            IsSynergy = false;
            OffSynergyContents();
        }
    }
    #endregion

    // Ranking Contesnt UI
    #region Ranking Contesnt UI
    public void OnRankContents()
    {
        if (rankingUI.transform.position.x <= -180f) return;
        rankingUI.transform.position = Vector2.Lerp(rankingUI.transform.position, new Vector2(-180f, 100f), Time.deltaTime * 3f);
    }
    public void OffRankContents()
    {
        if (rankingUI.transform.position.x >= 150f) return;
        rankingUI.transform.position = Vector2.Lerp(rankingUI.transform.position, new Vector2(150f, 100f), Time.deltaTime * 3f);
    }
    protected void RankingContents()
    {
        if (!IsRanking)
        {
            IsRanking = true;
            OnRankContents();
        }
        else
        {
            IsRanking = false;
            OffRankContents();
        }
    }
    #endregion

    // Gacha Wepon UI
    public void OnWeponGacha()
    {
        gachaWeponGold = 10;
        gold -= gachaWeponGold;
    }
    // Gacha Unit UI
    public void OnUnitGacha()
    {
        gachaUnitGold = 100;
        gold -= gachaUnitGold;
    }

}
