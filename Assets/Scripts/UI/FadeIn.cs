using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// ��â���� ���� �ٺ��� ���ÿ����÷ο�� ���̰� OPEN AI�� õ�������� â���ϽŴ� �ٵ� ����ȭ�� ���� �Ѵ�

public class FadeIn : MonoBehaviour
{
    public Image backblack; // �̰Ž� ��׶��� ����̿�
    public GameObject Button; // �̰Ž� ���� �����̿� ��ư ������ ���� �����ɷ� ģ�粲
    public TextMeshProUGUI win; //TMP�� Color�� ���� ����
    public float duration = 1f; // ������ �ִϸ��̼� ���ӽð�
    public GameObject gameo; //���� ������
    public GameObject particle; //���� ��ƼŬ
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    public void Fadein() //���̵� �� �Լ�. ��ư ȣ���. Fadeinco�� ȣ��ȴ�.
    {
        Button.SetActive(false);
        StartCoroutine(Fadeinco());
    }
    //Fadein �� ȣ��Ǹ� ù��°�� Fadeinco�� ȣ��ǰ�, yield return�� ���ؼ� Fadeintext�� ȣ���ϰ� �ȴ�. ���� Minicon�� ȣ���ϰ�, ���������� ������ ī�带 �����ϸ� CardChoice�� ȣ���Ѵ�.
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
    } //��������� ��׶��尡 ��ο����� ���� �ڷ�ƾ���� �Ѿ�� ����
    IEnumerator Fadeintext()
    {
        float textalpha = 0;
        while (textalpha <= 1f)
        {
            textalpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            win.color = new Color(255, 255, 255, textalpha);
        } //����� ��� �ؽ�Ʈ�� ���̵����� �̷������ �ڵ�
        yield return StartCoroutine(Minicon());
    }
    IEnumerator Minicon()
    {
        gameo.SetActive(true);
        iTween.ScaleTo(gameo, iTween.Hash("scale", new Vector3(1, 1, 1), "time", duration, "easetype", iTween.EaseType.easeOutQuad));
        //iTween�̶�� Ȯ�����α׷�(����)���. �ʿ�� ���� ���� (�� ��Դ� �ȵ�. �� �κ��� ���� ������ ������ ����.)
        yield return new WaitForSeconds(1);
        particle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        particle.SetActive(false);
        yield return StartCoroutine(Cardfade());
        // ����� �����ܰ� ��ƼŬ ���. 
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