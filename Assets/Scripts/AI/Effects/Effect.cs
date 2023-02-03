using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle.AI;

namespace Battle.EFFECT
{
    public abstract class Effect : MonoBehaviour
    {
        [SerializeField] protected Hit HitEffect = null;
        private float time = 0;
        private float destroyTime = 2f;
        protected float speed = 0f;
        protected string ownerName = null;
        protected Vector3 direction = Vector3.zero;
        protected bool isNonAttackEffect = false;
        protected float attackDamage = 0f;

        private void Awake()
        {
            destroyTime = setDestroyTime();
            speed = setSpeed();
        }

        private void Update()
        {
            time += Time.deltaTime;

            if(time >= destroyTime)
            {
                Destroy(gameObject);
            }
            
            specialLogic();

            if(speed == 0f
                || direction == Vector3.zero)
            {
                return;
            }
            gameObject.transform.Translate(direction * Time.deltaTime * speed, Space.World);
        }

        public void setAttackDamage(float damage)
        {
            attackDamage = damage;
        }

        public virtual void setDirection(Vector3 targetPosition) { }
        protected abstract float setDestroyTime();
        protected abstract float setSpeed();
        protected abstract bool setIsNonAttackEffect();
        protected virtual void specialLogic() { }

        public virtual void setOwnerName(string nickName)
        {
            this.ownerName = nickName;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (ownerName == null)
            {
                Debug.LogError("Please Set OwnerName");
                return;
            }

            if(isNonAttackEffect == true)
            {
                return;
            }

            ParentBT target = null;

            if(collision.transform.TryGetComponent<ParentBT>(out target) == true)
            {
                if(target == null)
                {
                    return;
                }
                if (target.getMyNickName().CompareTo(ownerName) != 0)
                {
                    if(target == null)
                    {
                        return;
                    }
                    if(HitEffect != null)
                    {
                        Instantiate(HitEffect,gameObject.transform.position,Quaternion.identity);
                        HitEffect.setAttackDamage(attackDamage);
                    }
                    else
                    {
                        target.Damage(attackDamage);
                    }
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}


