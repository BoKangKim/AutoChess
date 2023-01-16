using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays champion health over champion
/// </summary>
public class HealthBar : MonoBehaviour
{
    private GameObject championGO;
    private ChampionController championController;
    public Image fillImage;

    private CanvasGroup canvasGroup;

    /// Start is called before the first frame update
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    /// Update is called once per frame
    void Update()
    {
        if(championGO != null)
        {
            this.transform.position = championGO.transform.position + new Vector3(0, 1.5f + 1.5f * championGO.transform.localScale.x, 0);
            fillImage.fillAmount = championController.currentHealth / championController.maxHealth;

            if (championController.currentHealth <= 0)
                canvasGroup.alpha = 0;
            else
                canvasGroup.alpha = 1;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Called when champion created
    /// </summary>
    /// <param name="_championGO"></param>
    public void Init(GameObject _championGO)
    {
        championGO = _championGO;
        championController = championGO.GetComponent<ChampionController>();

    }
}
