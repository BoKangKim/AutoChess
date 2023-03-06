namespace Battle.Stage
{
    using Battle.AI;
    using Battle.Location;
    using Photon.Pun;
    using Photon.Realtime;
    using System.Collections;
    using UnityEngine;

    public enum STAGETYPE
    {
        PVP,
        CLONE,
        MONSTER,
        BOSS,
        AUCTION,
        PREPARE,
        NULL,
        MAX
    }

    public delegate void ChangeStage(STAGETYPE stageType);

    public class StageControl : MonoBehaviourPun
    {
        [SerializeField] private GameObject[] Monsters = null;
        private GameObject cam = null;

        private STAGETYPE[] stages = new STAGETYPE[30];
        private STAGETYPE nowStage = STAGETYPE.PREPARE;
        private int stageIndex = -1;
        public ChangeStage changeStage = null;

        private ZoneSystem.MapController[] maps = null;
        private ZoneSystem.MapController myMap = null;
        private GameObject[,] battleObject = null;

        private int preIndex = -1;
        private readonly Vector3 changeCamVec = new Vector3(19.5f, 0, 12f);
        private readonly Quaternion changeCamRot = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        private bool isEndSetEnemy = false;
        private MonsterAI monster = null;

        private delegate IEnumerator waitMaster();

        private Vector3 camStartPos = Vector3.zero;
        private PlayerData player = null;

        public STAGETYPE getNowStage()
        {
            return nowStage;
        }

        private void Awake()
        {
            cam = GameObject.Find("Cam");
            initializingStageInfo();
        }

        private void Start()
        {
            if (photonView.IsMine == true)
            {
                Timer timer = FindObjectOfType<Timer>();

                if (timer != null)
                {
                    timer.initializingStageControl(this);
                }
            }


            if (maps == null)
            {
                maps = FindObjectsOfType<ZoneSystem.MapController>();
                for (int i = 0; i < maps.Length; i++)
                {
                    if (maps[i].photonView.IsMine == true)
                    {
                        myMap = maps[i];
                        break;
                    }
                }
            }

            camStartPos = cam.transform.position;
        }

        public void checkNextStageInfo()
        {
            if (PhotonNetwork.IsMasterClient == false)
            {
                return;
            }
            else
            {
                nowStage = GameManager.Inst.GetNowStage();
                stageIndex = GameManager.Inst.getStageIndex();                
            }

            if (nowStage != STAGETYPE.PREPARE)
            {
                nowStage = STAGETYPE.PREPARE;
                photonView.RPC("CacheMasterStage", RpcTarget.All, nowStage);
                if (changeStage != null)
                {
                    photonView.RPC("RPC_changeStage", RpcTarget.All, nowStage);
                }

                return;
            }

            stageIndex++;

            if(stageIndex >= stages.Length)
            {
                nowStage = STAGETYPE.PVP;
            }
            else
            {
                nowStage = stages[stageIndex];
            }

            if (changeStage != null)
            {
                photonView.RPC("RPC_changeStage", RpcTarget.All, nowStage);
            }

            photonView.RPC("CacheMasterIndex", RpcTarget.All, stageIndex);
            photonView.RPC("CacheMasterStage", RpcTarget.All, nowStage);
        }

        [PunRPC]
        public void RPC_changeStage(STAGETYPE nowStage)
        {
            if(changeStage != null)
            {
                changeStage(nowStage);
            }
        }

        [PunRPC]
        public void CacheMasterIndex(int stageIndex)
        {
            GameManager.Inst.SyncStageIndex(stageIndex);
            this.stageIndex = stageIndex;
        }

        [PunRPC]
        public void CacheMasterStage(STAGETYPE stage)
        {
            GameManager.Inst.SetNowStage(stage);
            nowStage = stage;
        }

        public void startNextStage()
        {
            if (nowStage == STAGETYPE.PVP)
            {
                if (PhotonNetwork.IsMasterClient == true)
                {
                    isEndSetEnemy = setNextEnemy();
                    photonView.RPC("RPC_IsEndSetEnemy", RpcTarget.All);
                }
            }
            else if (nowStage == STAGETYPE.MONSTER)
            {
                if(PhotonNetwork.IsMasterClient == true)
                {
                    photonView.RPC("instantiateMonster", RpcTarget.All, nowStage);
                }
            }
            else if (nowStage == STAGETYPE.BOSS)
            {
                if(PhotonNetwork.IsMasterClient == true)
                {
                    photonView.RPC("instantiateMonster", RpcTarget.All, nowStage);
                }
            }
            else if (nowStage == STAGETYPE.PREPARE)
            {
                photonView.RPC("returnMyMap", RpcTarget.All);
            }
        }

        [PunRPC]
        public void instantiateMonster(STAGETYPE nowStage)
        {
            monster = (MonsterAI)myMap.InstantiateMonster(Monsters[0],nowStage);
        }

        [PunRPC]
        public void RPC_IsEndSetEnemy()
        {
            changeUnitMap();
        }

        private void changeUnitMap()
        {
            GameManager.Inst.GetPlayerInfoConnector().ResetUnitCount();
            battleObject = myMap.getBattleObjects();
            ParentBT bt = null;

            for (int i = 0; i < battleObject.GetLength(0); i++)
            {
                for (int j = 0; j < battleObject.GetLength(1); j++)
                {
                    if (battleObject[i, j] == null)
                    {
                        continue;
                    }

                    if (battleObject[i, j].TryGetComponent<ParentBT>(out bt) == true)
                    {
                        bt.setEnemyNickName(myMap.getEnemy().getMyNickName());
                        GameManager.Inst.GetPlayerInfoConnector().PlusUnitCount();
                    }

                    if (myMap.isMirrorModePlayer == false)
                    {
                        continue;
                    }

                    battleObject[i, j].transform.SetParent(myMap.getEnemy().transform, false);
                    battleObject[i, j].transform.localPosition = LocationControl.convertMirrorMode(battleObject[i, j].transform.localPosition);
                    battleObject[i, j].transform.rotation = changeCamRot;

                    cam.transform.position = myMap.getEnemy().transform.position + changeCamVec;
                    cam.transform.rotation = changeCamRot;
                }
            }

            
        }

        [PunRPC]
        public void returnMyMap()
        {
            GameManager.Inst.SyncStageInfoUI();

            battleObject = myMap.getBattleObjects();

            if(monster != null &&
                monster.gameObject.activeSelf == true)
            {
                PhotonNetwork.Destroy(monster.gameObject);
                monster = null;
            }

            if (myMap.isMirrorModePlayer == true)
            {
                cam.transform.position = camStartPos;
                cam.transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            for (int i = 0; i < battleObject.GetLength(0); i++)
            {
                for (int j = 0; j < battleObject.GetLength(1); j++)
                {
                    if (battleObject[i, j] == null)
                    {
                        continue;
                    }

                    LocationXY location;
                    location.x = j;
                    location.y = i;

                    if (battleObject[i, j].activeSelf == false)
                    {
                        myMap.instantiateBattleObj(battleObject[i,j].name,Vector3.zero,Quaternion.identity,location);
                    }
                    else
                    {
                        battleObject[i, j].transform.SetParent(myMap.transform, false);
                        battleObject[i, j].transform.localPosition = LocationControl.convertLocationToPosition(location);
                        battleObject[i, j].transform.rotation = Quaternion.identity;
                    }
                }
            }

            myMap.isMirrorModePlayer = false;
        }

        private bool setNextEnemy()
        {
            if (myMap.getEnemy() == null)
            {
                do
                {
                    preIndex = UnityEngine.Random.Range(0, maps.Length);
                    myMap.setEnemy(maps[preIndex], 0, 0, false);
                } while (maps[preIndex].photonView.IsMine == true);
            }
            else
            {
                int index = -1;
                do
                {
                    index = UnityEngine.Random.Range(0, maps.Length);
                    myMap.setEnemy(maps[index], 0, 0, false);
                } while (maps[index].photonView.IsMine == true
                || maps[preIndex].photonView.ViewID == maps[index].photonView.ViewID);

                preIndex = index;
            }

            maps[preIndex].setEnemy(myMap, maps[preIndex].photonView.ViewID, myMap.photonView.ViewID, true);

            int otherIndex = 0;
            ZoneSystem.MapController[] others = new ZoneSystem.MapController[2];
            for (int i = 0; i < maps.Length; i++)
            {
                if (i == preIndex ||
                    maps[i].photonView.IsMine == true)
                {
                    continue;
                }
                else
                {
                    others[otherIndex] = maps[i];
                    otherIndex++;
                }
            }

            others[0].setEnemy(others[1], others[0].photonView.ViewID, others[1].photonView.ViewID, true);
            others[1].setEnemy(others[0], others[1].photonView.ViewID, others[0].photonView.ViewID, true);

            int change = UnityEngine.Random.Range(0, 2);
            if (change == 0)
            {
                myMap.isMirrorModePlayer = true;
                maps[preIndex].StartRPC_SetIsMirrorPlayer(false);
                others[0].StartRPC_SetIsMirrorPlayer(true);
                others[1].StartRPC_SetIsMirrorPlayer(false);
            }
            else
            {
                myMap.isMirrorModePlayer = false;
                maps[preIndex].StartRPC_SetIsMirrorPlayer(true);
                others[0].StartRPC_SetIsMirrorPlayer(false);
                others[1].StartRPC_SetIsMirrorPlayer(true);
            }

            return true;
        }
        

        private void initializingStageInfo()
        {
            stages[0] = STAGETYPE.MONSTER;

            for(int i = 0; i < stages.Length; i++)
            {
                stages[i] = STAGETYPE.PVP;
            }

        }
    }
}