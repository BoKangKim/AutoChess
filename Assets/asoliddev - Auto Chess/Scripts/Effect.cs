using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visual effect for champions
/// </summary>
public class Effect : MonoBehaviour
{
    public GameObject effectPrefab;

    /// How long the effect should last in secounds
    public float duration;
    private GameObject championGO;
    private GameObject effectGO;

    /// Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;

        if (duration < 0)
            championGO.GetComponent<ChampionController>().RemoveEffect(this);
    }

    /// <summary>
    ///  Called when effect is created the first time
    /// </summary>
    /// <param name="_effectPrefab"></param>
    /// <param name="_championGO"></param>
    /// <param name="_duration"></param>
    public void Init(GameObject _effectPrefab, GameObject _championGO, float _duration)
    {
        effectPrefab = _effectPrefab;
        duration = _duration;
        championGO = _championGO;

        effectGO = Instantiate(effectPrefab);
        effectGO.transform.SetParent(championGO.transform);
        effectGO.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Called when the effect expired
    /// </summary>
    public void Remove()
    {
        Destroy(effectGO);
        Destroy(this);
    }
}
