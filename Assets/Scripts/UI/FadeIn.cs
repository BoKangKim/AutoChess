using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class FadeIn : MonoBehaviour
{
    [Header("아이콘, 페이드 인 관련")]
    [SerializeField] private Image gameo2;
    [SerializeField] private Image paper;
    [SerializeField] private Image rank1paperimg;
    [SerializeField] private Image rank2paperimg;
    [SerializeField] private Image rank3paperimg;
    [SerializeField] private Image rank4paperimg;
    [SerializeField] private Image backblack;
    [SerializeField] private Image rank1img;
    [SerializeField] private Image rank2img;
    [SerializeField] private Image rank3img;
    [SerializeField] private Image rank4img;
    [SerializeField] private GameObject gameo; //순위 아이콘
    [SerializeField] private GameObject particle; //먼지 파티클
    [SerializeField] private GameObject Button; // 시작을 위한 벋-은
    [SerializeField] private GameObject paperg;
    [SerializeField] private GameObject rank1paper;
    [SerializeField] private GameObject rank2paper;
    [SerializeField] private GameObject rank3paper;
    [SerializeField] private GameObject rank4paper;
    [SerializeField] private TextMeshProUGUI win; //TMP의 Color를 쓰게 해줌
    [SerializeField] private TextMeshProUGUI rank1text;
    [SerializeField] private TextMeshProUGUI rank2text;
    [SerializeField] private TextMeshProUGUI rank3text;
    [SerializeField] private TextMeshProUGUI rank4text;
    [SerializeField] private TMP_Text textMeshPro;
    [SerializeField] private GameObject gameoRank1;
    [SerializeField] private GameObject gameoRank2;
    [SerializeField] private GameObject gameoRank3;
    [SerializeField] private GameObject gameoRank4;
    [SerializeField] private Button rank1;
    [SerializeField] private Button rank2;
    [SerializeField] private Button rank3;
    [SerializeField] private Button rank4;


    // 아이콘 애니메이션 지속시간
    [SerializeField] private float startX;
    [SerializeField] private float startX2;

    [Header("카드 선택 관련")]
    [SerializeField] private GameObject card1;
    [SerializeField] private GameObject card2;
    [SerializeField] private GameObject card3;
    [SerializeField] private GameObject card4;
    [SerializeField] private Image cardimg1;
    [SerializeField] private Image cardimg2;
    [SerializeField] private Image cardimg3;
    [SerializeField] private Image cardimg4;
    [SerializeField] private Button cardbutton1;
    [SerializeField] private Button cardbutton2;
    [SerializeField] private Button cardbutton3;
    [SerializeField] private Button cardbutton4;

    [Header("카드 움직임 관련 변수")]
    [SerializeField] private float duration2 = 2f;
    [SerializeField] private float duration3 = 1f;
    [SerializeField] private float duration4 = 0.5f;
    [SerializeField] private float endX = -690f;
    [SerializeField] private float endX2 = -0f;
    [SerializeField] private float endY = -260f;
    [SerializeField] private WaitForSeconds WaitForone = new WaitForSeconds(1f);
    [SerializeField] private float startY;

    private void Start()
    {
        GameManager.Inst.SetEnding(this);
    }

    public void Fadein()
    {
        Button.SetActive(false);
        StartCoroutine(Fadeinco());
    }

    IEnumerator Fadeinco()
    {
        float fadecount = 0;
        while (fadecount < 0.6f)
        {
            fadecount += 0.01f;
            yield return new WaitForSeconds(0.02f);
            backblack.color = new Color(0, 0, 0, fadecount);
        }
        yield return StartCoroutine(Fadeintext());
    }

    IEnumerator Fadeintext()
    {

        float textalpha = 0;
        while (textalpha <= 1f)
        {
            textalpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            win.color = new Color(255, 255, 255, textalpha);
        }
        rank1.gameObject.SetActive(true);
        rank2.gameObject.SetActive(true);
        rank3.gameObject.SetActive(true);
        rank4.gameObject.SetActive(true);
        //yield return StartCoroutine(Minicon());
    }

    public void RankStart(int rank)
    {
        StartCoroutine(Minicon(rank));
    }

    public void rankstart1()
    {
        StartCoroutine(Minicon(1));
        rank1.gameObject.SetActive(false);
        rank2.gameObject.SetActive(false);
        rank3.gameObject.SetActive(false);
        rank4.gameObject.SetActive(false);
    }

    public void rankstart2()
    {
        StartCoroutine(Minicon(2));
        rank1.gameObject.SetActive(false);
        rank2.gameObject.SetActive(false);
        rank3.gameObject.SetActive(false);
        rank4.gameObject.SetActive(false);
    }

    public void rankstart3()
    {
        StartCoroutine(Minicon(3));
        rank1.gameObject.SetActive(false);
        rank2.gameObject.SetActive(false);
        rank3.gameObject.SetActive(false);
        rank4.gameObject.SetActive(false);
    }

    public void rankstart4()
    {
        StartCoroutine(Minicon(4));
        rank1.gameObject.SetActive(false);
        rank2.gameObject.SetActive(false);
        rank3.gameObject.SetActive(false);
        rank4.gameObject.SetActive(false);
    }

    IEnumerator Minicon(int rank)
    {
        TextMeshProUGUI text = null;

        switch (rank)
        {
            case 1:
                gameo = gameoRank1;
                gameo2 = rank1img;
                textMeshPro = rank1text;
                paperg = rank1paper;
                paper = rank1paperimg;

                break;
            case 2:
                gameo = gameoRank2;
                gameo2 = rank2img;
                textMeshPro = rank2text;
                paperg = rank2paper;
                paper = rank2paperimg;
                break;
            case 3:
                gameo = gameoRank3;
                gameo2 = rank3img;
                textMeshPro = rank3text;
                paperg = rank3paper;
                paper = rank3paperimg;
                break;
            case 4:
                gameo = gameoRank4;
                gameo2 = rank4img;
                textMeshPro = rank4text;
                paperg = rank4paper;
                paper = rank4paperimg;
                break;
            default:
                // 예외 처리
                break;
        }

        if(paperg.transform.Find("info").TryGetComponent<TextMeshProUGUI>(out text) == true)
        {
            text.text = "The greatest, truest warrior's name is " + PhotonNetwork.NickName;
        }

        float targetDilate = 0f;
        float currentTime = 0f;
        float duration = 1f;

        gameo.gameObject.SetActive(true);
        iTween.ScaleTo(gameo.gameObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", duration, "easetype", iTween.EaseType.easeOutQuad));
        yield return WaitForone;
        particle.SetActive(true);
        yield return WaitForone;
        particle.SetActive(false);
        yield return WaitForone;
        textMeshPro.gameObject.SetActive(true);
        Material mat = textMeshPro.fontMaterial;

        while (currentTime < duration2)
        {
            currentTime += Time.deltaTime;
            float tt = Mathf.Clamp01(currentTime / duration2);
            float newDilate = Mathf.Lerp(-1, targetDilate, tt);
            mat.SetFloat("_FaceDilate", newDilate);
            yield return null;
        }

        float ti = 0f;
        while (ti < 1f)
        {
            ti += Time.deltaTime / duration3;
            Vector2 newPosition = gameo2.rectTransform.anchoredPosition;
            float newX = Mathf.SmoothStep(startX, endX, ti);
            newPosition.x = newX;
            gameo2.rectTransform.anchoredPosition = newPosition;
            yield return null;
        }
        Vector2 finalPosition = gameo2.rectTransform.anchoredPosition;
        finalPosition.x = endX;
        gameo2.rectTransform.anchoredPosition = finalPosition;
        yield return WaitForone;

        paperg.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration3;
            Vector2 newPosition = paper.rectTransform.anchoredPosition;
            float newX = Mathf.SmoothStep(startX2, endX2, t);
            newPosition.x = newX;
            paper.rectTransform.anchoredPosition = newPosition;
            yield return null;
        }
        Vector2 finalPosition1 = paper.rectTransform.anchoredPosition;
        finalPosition1.x = endX2;
        paper.rectTransform.anchoredPosition = finalPosition1;
        yield return WaitForone;

        StartCoroutine(CardPositionset());
    }
    
    IEnumerator CardPosition(GameObject cards, Image cardimg)
    {
        cards.SetActive(true);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration4;
            Vector2 newPosition = cardimg.rectTransform.anchoredPosition;
            float newX = Mathf.SmoothStep(startY, endY, t);
            newPosition.y = newX;
            cardimg.rectTransform.anchoredPosition = newPosition;
            yield return null;
        }
        Vector2 finalPosition = cardimg.rectTransform.anchoredPosition;
        finalPosition.y = endY;
        cardimg.rectTransform.anchoredPosition = finalPosition;
    }

    IEnumerator CardPositionset()
    {
        yield return StartCoroutine(CardPosition(card1, cardimg1));
        yield return StartCoroutine(CardPosition(card2, cardimg2));
        yield return StartCoroutine(CardPosition(card3, cardimg3));
        yield return StartCoroutine(CardPosition(card4, cardimg4));
        cardbutton1.interactable = true;
        cardbutton2.interactable = true;
        cardbutton3.interactable = true;
        cardbutton4.interactable = true;
    }
}



