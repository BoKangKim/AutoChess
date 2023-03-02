using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flip : MonoBehaviour
{
    public GameObject frontSide;
    public GameObject backSide;
    public GameObject bodycard;
    private Button button;
    private float duration = 0.3f;
    private bool alreadyselect = false;

    private void Start()
    {
        button = bodycard.GetComponent<Button>();
    }

    public void letsgoo()
    {
        if (alreadyselect == false)
        {
            StartCoroutine(RotateImage());
            alreadyselect = true;
        }
        button.interactable = false;
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