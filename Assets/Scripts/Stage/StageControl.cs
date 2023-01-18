using UnityEngine;
using System.Collections.Generic;

namespace Battle.Stage 
{
    // Timer <-> Stage => AI
    // 
    public enum STAGETYPE
    {
        PVP,
        CLONE,
        MONSTER,
        BOSS,
        AUCTION,
        PREPARE,
        MAX
    }
    public delegate void ChangeStage(STAGETYPE stageType);

    public class StageControl : MonoBehaviour
    {
        private STAGETYPE[,] stages = new STAGETYPE[9,4];
        private (int col, int row) stageIndex = (0, 0);
        private Timer timer = null;
        public ChangeStage changeStage = null;

        private void Awake()
        {
            // 네트워크
            if(TryGetComponent<Timer>(out timer) == false)
            {
                timer = new GameObject("Timer").AddComponent<Timer>();
            }
        }

        private void checkNextStageInfo() 
        {
            stageIndex.row++;
            
            if(stageIndex.row >= stages.GetLength(1))
            {
                stageIndex.row = 0;
                stageIndex.col++;

                if (stageIndex.col >= stages.GetLength(0))
                {
                    stageIndex.row = -1;
                    stageIndex.col = -1;
                }
            }

            if(stageIndex.row == -1
                || stageIndex.col == -1)
            {
                changeStage(STAGETYPE.PVP);
                return;
            }

            changeStage(stages[stageIndex.col,stageIndex.row]);
        }

        private void startNextStage() 
        {

        }
    }
}