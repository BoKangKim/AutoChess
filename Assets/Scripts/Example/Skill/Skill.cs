using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

namespace Skills 
{
    [RequireComponent(typeof(ParentBT))]
    public abstract class Skill : MonoBehaviour
    {
        private string skillName = "";
        private float castingTime = 0f;
        private float skillTime = 0;
        private float cTime = 0;
        private float sTime = 0;

        private void Awake()
        {
            ScriptableUnit unitData = GetComponent<ParentBT>().getUnitData();

            castingTime = unitData.GetMagicCastingTime;
        }

        private void Start()
        {
            skillName = initializingName();
            skillTime = initializingskillTime();
        }

        private void Update()
        {
            cTime += Time.deltaTime;
            if (cTime <= castingTime
                && sTime == 0)
            {
                casting();
                return;
            }

            cTime = 0f;
            sTime += Time.deltaTime;

            if(sTime <= skillTime)
            {
                run();
                return;
            }

            sTime = 0;
        }

        protected abstract float initializingskillTime();

        protected abstract string initializingName();
        
        protected abstract void run();

        protected virtual void casting() { }
    }
}
