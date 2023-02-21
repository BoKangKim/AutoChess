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

        private STAGETYPE[,] stages = new STAGETYPE[9, 4];
        private STAGETYPE nowStage = STAGETYPE.PREPARE;
        private (int row, int col) stageIndex = (0, -1);
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
                nowStage = GameManager.Inst.nowStage;
                stageIndex = GameManager.Inst.getStageIndex();                
            }

            if (nowStage != STAGETYPE.PREPARE)
            {
                nowStage = STAGETYPE.PREPARE;
                GameManager.Inst.nowStage = nowStage;
                photonView.RPC("CacheMasterStage", RpcTarget.Others, nowStage);
                if (changeStage != null)
                {
                    changeStage(nowStage);
                    photonView.RPC("RPC_changeStage", RpcTarget.All, nowStage);
                }

                return;
            }

            stageIndex.col++;

            if (stageIndex.col >= stages.GetLength(1)
                || stages[stageIndex.row, stageIndex.col] == STAGETYPE.NULL)
            {
                stageIndex.col = 0;
                stageIndex.row++;
            }

            if (stageIndex.row >= stages.GetLength(0))
            {
                stageIndex.col = 5;
                nowStage = STAGETYPE.PVP;
                GameManager.Inst.nowStage = nowStage;
            }
            else
            {
                nowStage = stages[stageIndex.row, stageIndex.col];
                GameManager.Inst.nowStage = nowStage;
            }

            if (changeStage != null)
            {
                //changeStage(nowStage);
                photonView.RPC("RPC_changeStage", RpcTarget.All, nowStage);
            }

            GameManager.Inst.SyncStageIndex(stageIndex.row, stageIndex.col);
            GameManager.Inst.nowStage = nowStage;
            photonView.RPC("CacheMasterIndex", RpcTarget.All, stageIndex.row, stageIndex.col);
            photonView.RPC("CacheMasterStage", RpcTarget.Others, nowStage);
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
        public void CacheMasterIndex(int row, int col)
        {
            GameManager.Inst.SyncStageIndex(row, col);
            stageIndex = (row, col);
        }

        [PunRPC]
        public void CacheMasterStage(STAGETYPE stage)
        {
            GameManager.Inst.nowStage = stage;
        }

        public void startNextStage()
        {
            if (nowStage == STAGETYPE.PVP)
            {
                if (PhotonNetwork.IsMasterClient == true)
                {
                    isEndSetEnemy = setNextEnemy();
                    photonView.RPC("SetIsEndSetEnemy", RpcTarget.All);
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
        public void SetIsEndSetEnemy()
        {
            changeUnitMap();
        }

        private void changeUnitMap()
        {
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
                } while (maps[index].photonView.IsMine == true
                || myMap.getEnemy().photonView.ViewID == maps[index].photonView.ViewID);

                myMap.setEnemy(maps[index], 0, 0, false);
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
            // STAGE 1
            {
                stages[0, 0] = STAGETYPE.MONSTER;
                stages[0, 1] = STAGETYPE.PVP;
                stages[0, 2] = STAGETYPE.PVP;
                stages[0, 3] = STAGETYPE.PVP;
            }
            
            // STAGE 2
            {
                stages[1, 0] = STAGETYPE.MONSTER;
                stages[1, 1] = STAGETYPE.PVP;
                stages[1, 2] = STAGETYPE.BOSS;
                stages[1, 3] = STAGETYPE.NULL;
            }

            // STAGE 3
            {
                stages[2, 0] = STAGETYPE.MONSTER;
                stages[2, 1] = STAGETYPE.PVP;
                stages[2, 2] = STAGETYPE.PVP;
                stages[2, 3] = STAGETYPE.NULL;
            }

            // STAGE 4
            {
                stages[3, 0] = STAGETYPE.AUCTION;
                stages[3, 1] = STAGETYPE.PVP;
                stages[3, 2] = STAGETYPE.BOSS;
                stages[3, 3] = STAGETYPE.NULL;
            }

            // STAGE 5
            {
                stages[4, 0] = STAGETYPE.MONSTER;
                stages[4, 1] = STAGETYPE.PVP;
                stages[4, 2] = STAGETYPE.PVP;
                stages[4, 3] = STAGETYPE.NULL;
            }

            // STAGE 6 ~ 8
            {
                for (int i = 5; i <= 7; i++)
                {
                    stages[i, 0] = STAGETYPE.PVP;
                    stages[i, 1] = STAGETYPE.PVP;
                    stages[i, 2] = STAGETYPE.PVP;
                    stages[i, 3] = STAGETYPE.NULL;
                }
            }

            // STAGE 9
            {
                stages[8, 0] = STAGETYPE.BOSS;
                stages[8, 1] = STAGETYPE.MONSTER;
                stages[8, 2] = STAGETYPE.PVP;
                stages[8, 3] = STAGETYPE.NULL;
            }

        }
    }
}