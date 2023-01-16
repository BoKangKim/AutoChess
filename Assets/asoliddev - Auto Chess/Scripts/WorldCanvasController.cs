using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class to display the HealthBar and FloatingText Canvas UI in WorldSpace
/// </summary>
public class WorldCanvasController : MonoBehaviour
{
    public GameObject worldCanvas;
    public GameObject floatingTextPrefab;
    public GameObject healthBarPrefab;
   
    /// <summary>
    /// For Creating a new FloatingText
    /// </summary>
    /// <param name="position"></param>
    /// <param name="v"></param>
    public void AddDamageText(Vector3 position, float v)
    {
        GameObject go = Instantiate(floatingTextPrefab);
        go.transform.SetParent(worldCanvas.transform);
       
        go.GetComponent<FloatingText>().Init(position, v);
    }

    /// <summary>
    /// For Creating a new HealthBar
    /// </summary>
    /// <param name="position"></param>
    /// <param name="v"></param>
    public void AddHealthBar(GameObject championGO)
    {
        GameObject go = Instantiate(healthBarPrefab);
        go.transform.SetParent(worldCanvas.transform);

        go.GetComponent<HealthBar>().Init(championGO);
    }
}
