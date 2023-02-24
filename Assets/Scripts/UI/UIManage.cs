using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using UnityEngine.EventSystems;


public class UIManage : MonoBehaviour
{
    public Canvas mainLobby;
    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private Canvas ticket;
    [SerializeField] private Canvas loadingcanvas;
    [SerializeField] private Canvas logincanvas;
    public Canvas matching;
    public Canvas selectbattle;

    [SerializeField] private GameObject selecticonPopup;
    [SerializeField] private GameObject unitShop;
    [SerializeField] private GameObject selectShop;
    [SerializeField] private GameObject record;
    [SerializeField] private GameObject collectionBook;
    [SerializeField] private GameObject book1Page;
    [SerializeField] private GameObject book2Page;
    [SerializeField] private GameObject book3Page;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject cantLivenet;
    [SerializeField] private GameObject comfirmation;
    [SerializeField] private GameObject exitbutton;
    [SerializeField] private GameObject ticketToolTip1;
    [SerializeField] private GameObject ticketToolTip2;
    [SerializeField] private GameObject ticketToolTip3;

    [SerializeField] private Image title;
    [SerializeField] private Image startimg;
    public Button startbutton;

    [SerializeField] private Slider ingameloading;
    [SerializeField] private Slider startloading;
    [SerializeField] private TextMeshProUGUI loadingtext;
    [SerializeField] private TextMeshProUGUI startext;
    public TextMeshProUGUI matchingtime;
    public TextMeshProUGUI sUnitText;

    private WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);
    private WaitForSeconds waitforthound = new WaitForSeconds(0.1f);
    private WaitForSeconds waittitle = new WaitForSeconds(0.025f);


    public Image userIcon;
    public Image sellunit;

    public NetworkManager networkManager;

    public Sprite[] userIconImage;
    AudioSource audioSource = null;
    public AudioClip buttonSound = null;
    //public AudioSource audioSource = null;

    public void Start()
    {
        StartCoroutine(Fadeintitle());

        GameManager.Inst.soundOption.bgmPlay("LobbyBgm");
    }

    public void ShopOpen()
    {
        //mainLobby.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(true);
        GameManager.Inst.soundOption.SFXPlay("ClickSFX");




    }

    public void ShopExit()
    {
        shopCanvas.gameObject.SetActive(false);
        //mainLobby.gameObject.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void SelectionIconOn()
    {
        selecticonPopup.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void SelectionIconOff()
    {
        selecticonPopup.gameObject.SetActive(false);


        GameManager.Inst.soundOption.SFXPlay("ClickSFX");


    }
    public void SelectBattleMode()
    {
        selectbattle.gameObject.SetActive(true);
        mainLobby.gameObject.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void SelectBattleExit()
    {
        selectbattle.gameObject.SetActive(false);
        mainLobby.gameObject.SetActive(true);


        GameManager.Inst.soundOption.SFXPlay("ClickSFX");
    }

    public void LiveOutPopup()
    {
        cantLivenet.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void LiveOutPopupExit()
    {
        cantLivenet.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");
    }
    public void BuyTicket()
    {
        ticket.gameObject.SetActive(true);
        shopCanvas.gameObject.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void BuyTicketExit()
    {
        ticket.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void BuyUnit()
    {
        unitShop.SetActive(true);
        selectShop.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void BuyUnitExit()
    {
        unitShop.SetActive(false);
        selectShop.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void RecordOn()
    {
        record.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void RecordExit()
    {
        record.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void CollectionbookOpen()
    {
        collectionBook.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void CollectionbookExit()
    {
        collectionBook.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void NextPage1()
    {
        book1Page.SetActive(false);
        book2Page.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void NextPage2()
    {
        book2Page.SetActive(false);
        book3Page.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void BeforePage1()
    {
        book1Page.SetActive(true);
        book2Page.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void BeforePage2()
    {
        book2Page.SetActive(true);
        book3Page.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void SettingOn()
    {
        setting.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void SettingOff()
    {
        setting.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void GameOut()
    {
        Application.Quit();

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");


#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }


    public void Iconchange()
    {
        string[] str = EventSystem.current.currentSelectedGameObject.name.Split("Button");
        int num = int.Parse(str[1]);
        userIcon.sprite = userIconImage[num];
        GameManager.Inst.dataBase.userInfo.userIconIndex = num;

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");


    }

    public void SetSellItemPopup(Sprite SellIcon)
    {
        Sprite newImage = SellIcon;
        sellunit.sprite = newImage;
        comfirmation.SetActive(true);
        exitbutton.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void SetSellItem(TextMeshProUGUI SellUnitText)
    {
        TextMeshProUGUI newtext = SellUnitText;
        sUnitText.text = newtext.text;

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void comfirmationExit()
    {
        comfirmation.SetActive(false);
        exitbutton.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void TicketToolTip1On()
    {
        ticketToolTip1.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void TicketToolTip1Off()
    {
        ticketToolTip1.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void TicketToolTip2On()
    {
        ticketToolTip2.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void TicketToolTip2Off()
    {
        ticketToolTip2.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void TicketToolTip3On()
    {
        ticketToolTip3.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void TicketToolTip3Off()
    {
        ticketToolTip3.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }
    public void GotoDappx()
    {
        Application.OpenURL("https://dappstore.me");

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void Letsmatch()
    {
        matching.gameObject.SetActive(true);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public void MatchExit()
    {
        matching.gameObject.SetActive(false);

        GameManager.Inst.soundOption.SFXPlay("ClickSFX");

    }

    public IEnumerator MatchTimer()
    {
        int sec = 0;
        int min = 0;
        while (true)
        {
            yield return waitForOneSecond;
            sec++;
            if (sec >= 60)
            {
                sec = 0;
                ++min;
            }
            //if (sec == 5)
            //{
            //    loadingcanvas.gameObject.SetActive(true);
            //    matching.gameObject.SetActive(false);
            //    yield return StartCoroutine(Lodingbar1());
            //}
            string time = string.Format("{0:00}:{1:00}", min, sec);
            matchingtime.text = time;
        }
    }
    public IEnumerator Lodingbar1()
    {
        float maxValue = 1.0f;


        while (ingameloading.value < maxValue)
        {
            float percentage = Mathf.Floor(ingameloading.value * 100);
            ingameloading.value += 0.01f;
            loadingtext.text = percentage.ToString() + "%";
            yield return waitforthound;
        }
        loadingtext.text = "100%";

    }


    IEnumerator Fadeintitle()
    {
        float fadecount = 0f;
        while (fadecount < 1f)
        {
            fadecount += 0.01f;
            yield return waittitle;
            title.color = new Color(1f, 1f, 1f, fadecount);
        }
        yield return waitForOneSecond;
        StartCoroutine(Fadeouttle());
    }
    IEnumerator Fadeouttle()
    {
        float fadecount = 1f;
        while (fadecount > 0f)
        {
            fadecount -= 0.01f;
            yield return waittitle;
            title.color = new Color(1f, 1f, 1f, fadecount);
        }
        yield return waitForOneSecond;
        startimg.color = new Color(1f, 1f, 1f, 1f);
        startbutton.gameObject.SetActive(true);
    }

    public IEnumerator FadeoutStart()
    {
        float fadecount = 1f;
        while (fadecount > 0f)
        {
            fadecount -= 0.01f;
            yield return waittitle;
            startimg.color = new Color(fadecount, fadecount, fadecount, 1f);
            startbutton.image.color = new Color(fadecount, fadecount, fadecount, 1f);
            startext.color = new Color(fadecount, fadecount, fadecount, 1f);
        }

        logincanvas.gameObject.SetActive(false);
        mainLobby.gameObject.SetActive(true);
    }
}