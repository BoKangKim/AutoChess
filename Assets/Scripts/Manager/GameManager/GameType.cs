using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameType : MonoBehaviour
{
    #region Singleton
    private static GameType instance = null;
    public static GameType Inst 
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameType>();

                if(instance == null)
                {
                    instance = new GameObject("GameTypeManagement").AddComponent<GameType>();
                }
            }

            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private GAMETYPE type = GAMETYPE.MAX;

    public GAMETYPE getType()
    {
        return type;
    }

    public void setType(GAMETYPE type,GameObject networkManager)
    {
        NetworkManager net = null;
        if (networkManager.TryGetComponent<NetworkManager>(out net) == false)
        {
            return;
        }

        this.type = type;
    }
}
