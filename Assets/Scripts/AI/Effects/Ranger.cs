using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

namespace Battle.AI.Effect 
{
    public class Ranger : StandardAttack
    {
        private string ownerNickName = "";
        private Vector3 direction = Vector3.zero;
        private float speed = 10f;
        private WaitForSeconds wait = new WaitForSeconds(4f);
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

        public void instHitEffect(Vector3 target)
        {
            GameObject hit = Instantiate(hitEffect,target,Quaternion.identity);
        }

        private void Awake()
        {
            _destroy = DestroyThis;
            StartCoroutine(_destroy());
        }

        private void Update()
        {
            transform.Translate(direction * Time.deltaTime * speed,Space.World);
        }

        IEnumerator DestroyThis()
        {
            yield return wait;

            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            ParentBT ai = null;
            if (other.transform.TryGetComponent<ParentBT>(out ai) == true)
            {
                if (ai.getMyNickName().CompareTo(ownerNickName) != 0)
                {

                    Destroy(this.gameObject);
                    instHitEffect(ai.transform.position);
                }
            }
        }
        
    }
}

