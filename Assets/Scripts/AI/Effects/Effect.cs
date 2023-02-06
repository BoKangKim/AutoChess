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
        protected ParentBT owner = null;
        protected Vector3 direction = Vector3.zero;
        protected bool isNonAttackEffect = false;
        protected float attackDamage = 0f;
        private bool isAttacked = false;
        private bool isInstHitEffect = false;

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

        public virtual void setOwner(ParentBT owner)
        {
            this.owner = owner;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (owner == null)
            {
                Debug.LogError("Please Set Owner");
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
                if (target.getMyNickName().CompareTo(owner.getMyNickName()) != 0)
                {
                    if(target == null)
                    {
                        return;
                    }

                    if(target.enabled == false)
                    {
                        return;
                    }

                    if (HitEffect != null && isInstHitEffect == false)
                    {
                        isInstHitEffect = true;
                        Effect hit = Instantiate(HitEffect, gameObject.transform.position, Quaternion.identity);
                        hit.setOwner(owner);
                        Destroy(gameObject);
                    }
                    else if(HitEffect == null && isAttacked == false)
                    {
                        owner.doDamage();
                        isAttacked = true;
                    }
                    
                }
            }
        }
    }
}


