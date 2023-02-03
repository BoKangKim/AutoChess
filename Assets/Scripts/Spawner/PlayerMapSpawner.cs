using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerMapSpawner : MonoBehaviourPun
{
    public static GameObject Map;
    public GameObject Cam;
    void Start()
    {
        Map = PhotonNetwork.Instantiate("Map", Vector3.zero, Quaternion.identity);

        switch (PhotonNetwork.LocalPlayer.CustomProperties["PlayerNum"])
        {
            case 0:
                {
                    Map.transform.position = Vector3.zero;
                    Cam.transform.position = Vector3.zero;
                    break;
                }
            case 1:
                {
                    Map.transform.position = new Vector3(100f, 0, 0);
                    Cam.transform.position = new Vector3(100f, 0, 0);
                    break;
                }
            case 2:
                {
                    Map.transform.position = new Vector3(0, 0, 100f);
                    Cam.transform.position = new Vector3(0, 0, 100f);
                    break;
                }
            case 3:
                {
                    Map.transform.position = new Vector3(100f, 0, 100f);
                    Cam.transform.position = new Vector3(100f, 0, 100f);
                    break;
                }
        }

    }



}
