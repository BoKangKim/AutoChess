using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Floating text display damage numbers champions take
/// </summary>
public class FloatingText : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Vector3 moveDirection;
    private float timer = 0;

    ///How fast text moves
    public float speed = 3;

    ///How long the text is visible
    public float fadeOutTime = 1f;

    /// Start is called before the first frame update
    void Start()
    {
        
    }

    /// Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + moveDirection * speed * Time.deltaTime;


        timer += Time.deltaTime;
        float fade = (fadeOutTime - timer) / fadeOutTime;

        canvasGroup.alpha = fade;

        if (fade <= 0)
            Destroy(this.gameObject);
    }
    /// <summary>
    /// Called when floating text is created
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="v"></param>
    public void Init(Vector3 startPosition, float v)
    {
        this.transform.position = startPosition;

        canvasGroup = this.GetComponent<CanvasGroup>();

        this.GetComponent<Text>().text = Mathf.Round(v).ToString();

        moveDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)).normalized;
    }
}
