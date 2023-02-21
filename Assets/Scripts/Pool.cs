using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Pool : MonoBehaviourPun ,IPunPrefabPool
{
    private readonly Dictionary<string, GameObject> resourceCache = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, Queue<GameObject>> listCache = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        PhotonNetwork.PrefabPool = this;
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject inst = null;
        Queue<GameObject> instList = null;

        prefabId = prefabId.Replace("(Clone)","");

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

        if(instList.Count == 0)
        {
            instList.Enqueue(GameObject.Instantiate(inst,position,rotation));
        }

        inst = instList.Dequeue();
        inst.SetActive(true);
        inst.transform.position = position;
        inst.transform.rotation = rotation;

        return inst;
    }

    public void Destroy(GameObject deObject)
    {
        string prefabID = deObject.name.Replace("(Clone)","");
        Queue<GameObject> instList = null;

        if(listCache.TryGetValue(prefabID,out instList) == false)
        {
            Debug.LogError(prefabID + " is not Pooling Object");
            return;
        }

        deObject.transform.parent = null;
        deObject.SetActive(false);
        instList.Enqueue(deObject);
    }

    public void listCaching(string[] cachingUnits)
    {
        for (int i = 0; i < cachingUnits.Length; i++)
        {
            resourceCache.Add(cachingUnits[i] ,Resources.Load<GameObject>(cachingUnits[i]));
        }
    }
}
