using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Pool : MonoBehaviourPun ,IPunPrefabPool
{
    private readonly Dictionary<string, GameObject> resourceCache = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Queue<GameObject>> listCache = new Dictionary<string, Queue<GameObject>>();

    private void OnDisable()
    {
        PhotonNetwork.PrefabPool = default;
    }

    private void OnEnable()
    {
        PhotonNetwork.PrefabPool = this;
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject inst = null;
        Queue<GameObject> instList = null;

        if(resourceCache.TryGetValue(prefabId,out inst) == false)
        {
            inst = Resources.Load<GameObject>(prefabId);
            resourceCache.Add(prefabId,inst);
        }

        if(listCache.TryGetValue(prefabId, out instList) == false)
        {
            instList = new Queue<GameObject>();
            listCache.Add(prefabId, instList);
        }

        if(instList.Count != 0)
        {
            inst = instList.Dequeue();
        }
        else
        {
            inst = Instantiate(inst);
        }

        inst.SetActive(true);
        inst.transform.position = position;
        inst.transform.rotation = rotation;

        return inst;
    }

    public void Destroy(GameObject gameObject)
    {
        string prefabID = gameObject.name.Replace("(Clone)","");
        Queue<GameObject> instList = null;

        if(listCache.TryGetValue(prefabID,out instList) == false)
        {
            Debug.LogError(prefabID + " is not Pooling Object");
            return;
        }

        gameObject.SetActive(false);
        instList.Enqueue(gameObject);
    }
}
