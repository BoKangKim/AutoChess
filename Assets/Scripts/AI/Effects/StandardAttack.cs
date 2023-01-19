using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.AI.Effect
{
    public abstract class StandardAttack : MonoBehaviour, AIEffect
    {
        [SerializeField] protected GameObject hitEffect = null;

        public abstract void setNickName(string nickName);
        public virtual void setDirection(Vector3 direction) { }
    }
}

