using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// 복창하자 나는 바보다 스택오버플로우는 신이고 OPEN AI는 천지만물을 창조하신다 근데 최적화는 내가 한다

public class FadeIn : MonoBehaviour
{
    public Image backblack; // 이거슨 백그라운드 명암이여
    public GameObject Button; // 이거슨 시작 조건이여 버튼 누르면 게임 끝난걸로 친당께
    public TextMeshProUGUI win; //TMP의 Color를 쓰게 해줌
    public float duration = 1f; // 아이콘 애니메이션 지속시간
    public GameObject gameo; //순위 아이콘
    public GameObject particle; //먼지 파티클
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    public void Fadein() //페이드 인 함수. 버튼 호출용. Fadeinco가 호출된다.
    {
        Button.SetActive(false);
        StartCoroutine(Fadeinco());
    }
    //Fadein 이 호출되면 첫번째로 Fadeinco가 호출되고, yield return에 의해서 Fadeintext를 호출하게 된다. 이후 Minicon을 호출하고, 마지막으로 유저가 카드를 선택하면 CardChoice를 호출한다.
    IEnumerator Fadeinco()
    {
            float fadecount = 0;
            while (fadecount < 0.9f)
            {
                fadecount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                backblack.color = new Color(0, 0, 0, fadecount);
            }
        yield return StartCoroutine(Fadeintext());
    } //여기까지는 백그라운드가 어두워지고 다음 코루틴으로 넘어가는 구간
    IEnumerator Fadeintext()
    {
        float textalpha = 0;
        while (textalpha <= 1f)
        {
            textalpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            win.color = new Color(255, 255, 255, textalpha);
        } //여기는 상단 텍스트의 페이드인이 이루어지는 코드
        yield return StartCoroutine(Minicon());
    }
    IEnumerator Minicon()
    {
        gameo.SetActive(true);
        iTween.ScaleTo(gameo, iTween.Hash("scale", new Vector3(1, 1, 1), "time", duration, "easetype", iTween.EaseType.easeOutQuad));
        //iTween이라는 확장프로그램(에셋)사용. 필요시 설명 가능 (단 깊게는 안됨. 이 부분은 구글 센세의 도움을 받음.)
        yield return new WaitForSeconds(1);
        particle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        particle.SetActive(false);
        yield return StartCoroutine(Cardfade());
        // 여기는 아이콘과 파티클 담당. 
    }
    IEnumerator Cardfade()
    {
        card1.SetActive(true);
        card2.SetActive(true);
        card3.SetActive(true);
        card4.SetActive(true);
        yield break;
    }
  }