using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

namespace Battle.AI.Effect
{
    public class Warrior : StandardAttack
    {
        private string ownerNickName = "";
        private Vector3 direction = Vector3.zero;
        private float speed = 2f;
        private WaitForSeconds wait = new WaitForSeconds(2f);
        private delegate IEnumerator destroy();
        destroy _destroy = null;

        public override void setNickName(string nickName)
        {
            this.ownerNickName = nickName;
        }

        public override void setDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        private void Awake()
        {
            _destroy = DestroyThis;
            StartCoroutine(_destroy());
        }

        IEnumerator DestroyThis()
        {
            yield return wait;

            Destroy(this.gameObject);
        }

    }
}

