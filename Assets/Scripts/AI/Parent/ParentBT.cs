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

        protected string myType = null;
        [SerializeField] private string nickName = "";

        protected LocationXY myLocation;
        protected LocationXY next;
        protected Vector3 nextPos;
        protected Vector3 dir;

        Vector3 nextLocation = Vector3.zero;
        protected RTAstar rta = null;

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
            // Object ���� ����
            // �ϴ� ���� ã�� ���߿� ���������� �ϼ��Ǹ�
            // ����
            findEnemyFuncOnStart(FindObjectsOfType<ParentBT>());
            searchingTarget();

            next = (LocationXY)rta.searchNextLocation(myLocation, target.myLocation);
            nextPos = LocationControl.convertLocationToPosition(next);
            dir = (nextPos - transform.position).normalized;
        }

        private void Update()
        {
            root.Run();

            if (specialRoot == null)
            {
                return;
            }

            specialRoot.Run();
        }

        private void InitializingRootNode()
        {
            root = Selector
                (
                    IfAction(isDeath, death),

                    Sequence
                    (
                        ActionN(idle),
                        NotIf(findEnemy)
                    ),

                    IfElseAction(isArangeIn, attack, move)
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
            float maxDistance = -100f;
            float temp = 0f;

            for (int i = 0; i < enemies.Count; i++)
            {
                if ((temp = Vector3.Distance(enemies[i].transform.position,transform.position)) > maxDistance)
                {
                    maxDistance = temp;
                    target = enemies[i];

                    
                }
            }
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
                        Debug.Log("TEST");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                };
            }
        }

        protected virtual Func<bool> isCenter 
        {
            get
            {
                return () =>
                {
                    if(Vector3.Distance(LocationControl.convertLocationToPosition(myLocation), transform.position) <= 0.2f)
                    {
                        return true;
                    }

                    return false;
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
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        if (Vector3.Distance(enemies[i].transform.position, transform.position) <= 1.58f)
                        {
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
                    return false;
                };
            }
        }

        protected virtual Action death
        {
            get
            {
                return () =>
                {

                };
            }
        }


        #endregion
    }

}
