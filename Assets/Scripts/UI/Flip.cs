using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Flip : MonoBehaviour
{
    public GameObject frontSide;
    public GameObject backSide;
    public GameObject bodycard;
    public float duration = 0.5f;
    private bool alreadyselect = false;

    public void letsgoo()
    {
        if (alreadyselect == false)
        {
            StartCoroutine(RotateImage());
            alreadyselect = true;
        }   
        }
    IEnumerator RotateImage()
    {
        iTween.ScaleTo(bodycard, iTween.Hash("scale", new Vector3(0, 0.4f, 0.4f), "time", duration, "easetype", iTween.EaseType.easeOutQuad));
        yield return new WaitForSeconds(duration);
        iTween.ScaleTo(bodycard, iTween.Hash("scale", new Vector3(0.4f, 0.4f, 0.4f), "time", duration, "easetype", iTween.EaseType.easeOutQuad));
        frontSide.SetActive(true);
        backSide.SetActive(false);
        yield break;
    }

}