using UnityEngine;
using TMPro;

namespace Battle.Stage
{
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

    public class StageControl : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI STAGE = null;

        private STAGETYPE[,] stages = new STAGETYPE[9, 4];
        private STAGETYPE nowStage = STAGETYPE.NULL;
        private (int row, int col) stageIndex = (0, -1);
        public ChangeStage changeStage = null;
        private ZoneSystem.MapController[] maps = null;
        private ZoneSystem.MapController myMap = null;

        private void Awake()
        {
            initializingStageInfo();
            checkNextStageInfo();
        }

        private void Start()
        {
            maps = FindObjectsOfType<ZoneSystem.MapController>();
            for(int i = 0; i < maps.Length; i++)
            {
                if (maps[i].photonView.IsMine == true)
                {
                    myMap = maps[i];
                    return;
                }
            }
        }

        public void checkNextStageInfo()
        {
            if (nowStage != STAGETYPE.PREPARE)
            {
                nowStage = STAGETYPE.PREPARE;
                STAGE.text = nowStage.ToString();
                if(changeStage != null)
                {
                    changeStage(nowStage);
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
            }
            else
            {
                nowStage = stages[stageIndex.row, stageIndex.col];
            }

            STAGE.text = nowStage.ToString() + "( " + (stageIndex.row + 1) + " - " + (stageIndex.col + 1) + " )";
            if (changeStage != null)
            {
                changeStage(nowStage);
            }
        }

        private void startNextStage()
        {
            if(nowStage == STAGETYPE.PVP)
            {

            }

        }

        private void initializingStageInfo()
        {
            // STAGE 1
            {
                stages[0, 0] = STAGETYPE.MONSTER;
                stages[0, 1] = STAGETYPE.MONSTER;
                stages[0, 2] = STAGETYPE.MONSTER;
                stages[0, 3] = STAGETYPE.BOSS;
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