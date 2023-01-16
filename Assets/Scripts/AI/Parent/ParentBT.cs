using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;
using System.Collections.Generic;
using Battle.Stage;
using Battle.Location;
using Battle.RTASTAR;

namespace Battle.AI
{
    public abstract class ParentBT : MonoBehaviour
    {
        private INode root = null;
        private INode specialRoot = null;
        protected ParentBT target = null;
        protected STAGETYPE stageType = STAGETYPE.PVP;

        protected Animator myAni = null;
        private List<ParentBT> enemies = null;
        private ParentBT[] allUnits = null;

        protected string myType = null;
        [SerializeField] private string nickName = "";

        protected LocationXY myLocation;
        protected LocationXY next;
        protected Vector3 nextPos;
        protected Vector3 dir;

        Vector3 nextLocation = Vector3.zero;
        protected RTAstar rta = null;

        private float currentHP = 100f;
        protected bool ishit = false;

        public void setIsHit(bool ishit)
        {
            this.ishit = ishit;
        }

        public string getMyNickName()
        {
            return nickName;
        }

        public LocationXY getMyLocation()
        {
            return myLocation;
        }

        public LocationXY getNextLocation()
        {
            return next;
        }

        private void Awake()
        {
            InitializingRootNode();
            initializingSpecialRootNode();
            myLocation = LocationControl.convertPositionToLocation(gameObject.transform.position);
            rta = new RTAstar(myLocation,gameObject.name);
            myType = initializingMytype();
        }

        private void Start()
        {
            myAni = GetComponent<Animator>();
            enemies = new List<ParentBT>();
            // Object 받을 예정
            // 일단 내가 찾고 나중에 원혁이형이 완성되면
            // 수정
            findEnemyFuncOnStart((allUnits = FindObjectsOfType<ParentBT>()));
            searchingTarget();

            next = rta.searchNextLocation(myLocation, target.myLocation);
            nextPos = LocationControl.convertLocationToPosition(next);
            dir = (nextPos - transform.position).normalized;
        }

        private void Update()
        {
            if(specialRoot != null 
                && specialRoot.Run() == true)
            {
                return;
            }

            root.Run();
            myLocation = LocationControl.convertPositionToLocation(transform.position);
        }

        private void InitializingRootNode()
        {
            root = Selector
                (
                    Sequence
                    (
                        IfAction(isHit,hit),
                        IfAction(isDeath, death)
                    ),

                    Sequence
                    (
                        ActionN(idle),
                        NotIf(findEnemy)
                    ),

                    IfAction(isStop,idle),

                    Sequence
                    (
                        IfElseAction(isArangeIn, moveCenter, move),
                        IfElseAction(isCenter,attack,moveCenter)
                    )
                );
        }

        protected virtual void initializingSpecialRootNode() { }
        protected abstract string initializingMytype();

        #region Searching Enemy
        protected void findEnemyFuncOnStart(ParentBT[] fieldAIObejects)
        {
            rta.initAllUnits(fieldAIObejects);

            switch (myType)
            {
                case "Unit":
                    addEnemyList(fieldAIObejects);
                    break;
                case "Monster":
                    addEnemyList(fieldAIObejects, "Monster");
                    break;
                case "Boss":
                    addEnemyList(fieldAIObejects, "Boss");
                    break;
            }

        }

        private void addEnemyList(ParentBT[] fieldAIObejects, string compare)
        {
            for (int i = 0; i < fieldAIObejects.Length; i++)
            {
                if (fieldAIObejects[i].myType.CompareTo(compare) == 0)
                {
                    continue;
                }

                enemies.Add(fieldAIObejects[i]);
            }
        }

        private void addEnemyList(ParentBT[] fieldAIObejects)
        {
            if (stageType == STAGETYPE.PVP
                || stageType == STAGETYPE.CLONE)
            {
                for (int i = 0; i < fieldAIObejects.Length; i++)
                {
                    if (fieldAIObejects[i].nickName.CompareTo(nickName) == 0)
                    {
                        continue;
                    }

                    enemies.Add(fieldAIObejects[i]);
                }
            }
            else if (stageType == STAGETYPE.MONSTER)
            {
                addEnemyList(fieldAIObejects, "Monster");
            }
            else if (stageType == STAGETYPE.BOSS)
            {
                addEnemyList(fieldAIObejects, "Boss");
            }
        }

        protected virtual void searchingTarget()
        {
            float minDistance = 100000f;
            float temp = 0f;

            for (int i = 0; i < enemies.Count; i++)
            {
                LocationXY enemyLocation = enemies[i].getMyLocation();
                if ((temp = LocationControl.getDistance(enemyLocation,myLocation)) < minDistance)
                {
                    minDistance = temp;
                    target = enemies[i];
                }
            }

        }

        private bool checkIsOverlapUnits()
        {
            LocationXY unitLocation;
            for (int i = 0; i < allUnits.Length; i++)
            {
                if (allUnits[i].Equals(this))
                    continue;
                unitLocation = LocationControl.convertPositionToLocation(allUnits[i].gameObject.transform.position);
                if (unitLocation.CompareTo(myLocation) == true)
                {
                    return true;
                }
            }

            return false;
        }

        public void Damage(float damage)
        {
            currentHP -= damage;
        }
        #endregion

        #region AI Behavior
        protected virtual Action idle
        {
            get
            {
                return () =>
                {
                };
            }
        }

        protected virtual Func<bool> findEnemy
        {
            get
            {
                return () =>
                {
                    searchingTarget();
                    if (target == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                };
            }
        }

        protected virtual Action moveCenter
        {
            get
            {
                return () =>
                {
                    Vector3 centerPosition = LocationControl.convertLocationToPosition(myLocation);
                    if (Vector3.Distance(centerPosition, transform.position) <= 0.1f)
                        return;

                    dir = (centerPosition - transform.position).normalized;

                    transform.LookAt(dir);
                    gameObject.transform.Translate(dir * 1f * Time.deltaTime, Space.World);
                };
            }
        }

        protected virtual Func<bool> isCenter
        {
            get
            {
                return () =>
                {
                    Vector3 centerPosition = LocationControl.convertLocationToPosition(myLocation);
                    if (Vector3.Distance(centerPosition, transform.position) <= 0.2f)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };
            }
        }

        protected virtual Action move
        {                                                                                                                         
            get
            {
                return () =>
                {
                    myLocation = LocationControl.convertPositionToLocation(transform.position);
                    if (Vector3.Distance(nextPos, transform.position) <= 0.2f)
                    {
                        next = rta.searchNextLocation(myLocation, target.getMyLocation());
                        nextPos = LocationControl.convertLocationToPosition(next);
                        dir = (nextPos - transform.position).normalized;
                    }

                    transform.LookAt(dir);
                    gameObject.transform.Translate(dir * 1f * Time.deltaTime,Space.World);
                };
            }
        }

        protected virtual Func<bool> isArangeIn
        {
            get
            {
                return () =>
                {
                    myLocation = LocationControl.convertPositionToLocation(transform.position);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if ((double)Vector3.Distance(enemies[i].transform.position, transform.position) <= LocationControl.radius
                        && checkIsOverlapUnits() == false)
                        {
                            next = myLocation;
                            return true;
                        }
                    }

                    return false;
                };
            }
        }

        protected virtual Func<bool> isAttackAble
        {
            get
            {
                return () =>
                {
                    return false;
                };
            }
        }

        protected virtual Action attack
        {
            get
            {
                return () =>
                {
                    target.setIsHit(true);
                    // 데이터 받아와서 넣으면 됨(데미지)
                    target.Damage(1f);
                    myAni.SetTrigger("isAttack");
                };
            }
        }

        protected virtual Func<bool> isDeath
        {
            get
            {
                return () =>
                {
                    return currentHP <= 0;
                };
            }
        }

        protected virtual Action death
        {
            get
            {
                return () =>
                {
                    myAni.SetTrigger("isDeath");
                };
            }
        }

        protected virtual Action hit 
        {
            get 
            {
                return () =>
                {
                    //myAni.SetTrigger("hit");
                };
            }
        }

        protected virtual Func<bool> isHit 
        {
            get 
            {
                return () =>
                {
                    bool temp = ishit;
                    ishit = false;
                    return temp;
                };
            }
        }

        protected virtual Func<bool> isStop 
        {
            get
            {
                return () =>
                {
                    return rta.isStop;
                };
            }
        }

        #endregion
    }

}
