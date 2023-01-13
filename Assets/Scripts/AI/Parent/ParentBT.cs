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
        // 체력
        private float hp = 100;

        // 무기
        // public Wepon wepon;

        // 드롭 아이템
        public GameObject Item;
        // 확률
        private int per;

        public LocationXY getMyLocation()
        {
            return myLocation;
        }

        private void Awake()
        {
            InitializingRootNode();
            initializingSpecialRootNode();
            myLocation = LocationControl.convertPositionToLocation(gameObject.transform.position);
            rta = new RTAstar(myLocation);
            myType = initializingMytype();
        }

        private void Start()
        {
            myAni = GetComponent<Animator>();
            enemies = new List<ParentBT>();
            // Object 받을 예정
            // 일단 내가 찾고 나중에 원혁이형이 완성되면
            // 수정
            findEnemyFuncOnStart(FindObjectsOfType<ParentBT>());
            searchingTarget();

            next = (LocationXY)rta.searchNextLocation(myLocation, target.myLocation);
            nextPos = LocationControl.convertLocationToPosition(next);
            dir = (nextPos - transform.position).normalized;
        }

        private void Update()
        {
            root.Run();

            if(LocationControl.isEscapeLocation(myLocation, gameObject.transform.position))
            {
                myLocation = LocationControl.convertPositionToLocation(gameObject.transform.position);
            }

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

            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.Distance(enemies[i].transform.position, gameObject.transform.position) < minDistance)
                {
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
                    Debug.Log("대기");
                    myAni.SetBool("isMove",false);
                };
            }
        }

        protected virtual Func<bool> findEnemy
        {
            get
            {
                return () =>
                {
                    Debug.Log("타겟 찾기");
                    searchingTarget();
                    if (target == null)
                    {
                        Debug.Log("못찾아");
                        return false;
                    }
                    else
                    {
                        Debug.Log("찾아");
                        return true;
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
                    myAni.SetBool("isMove", true);
                    if (Vector3.Distance(nextPos, transform.position) <= 0.2f)
                    {
                        myLocation = LocationControl.convertPositionToLocation(transform.position);
                        next = rta.searchNextLocation(myLocation, target.getMyLocation());
                        nextPos = LocationControl.convertLocationToPosition(next);
                        dir = (nextPos - transform.position).normalized;
                    }

                    gameObject.transform.LookAt(dir);
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
                    Debug.Log("범위 내에 있음");
                    if (LocationControl.getDistance(target.getMyLocation(), myLocation) <= 1f)
                    {
                        Debug.Log("공격범위 내에 있음");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
                    Debug.Log("공격");
                    myAni.SetTrigger("isAttack");
                };
            }
        }
        
        protected virtual Action IsHit
        {
            get
            {
                return () =>
                {
                    Debug.Log("맞는다");
                    Debug.Log(hp);
                    myAni.SetTrigger("isHit");
                    // hp -= wepon.damage;
                    
                    if (hp <= 0)
                    {
                        hp = 0;
                    }
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
                    Debug.Log("죽음");
                    
                    myAni.SetBool("IsDeath", true);

                    if (ItemCount == 0)
                    {
                        Debug.Log("드롭");
                        per = 0; // 100퍼 떨굼
                        DropItem();
                    }
                };
            }
        }


        int ItemCount = 0;
        private void DropItem()
        {
            //per = UnityEngine.Random.Range(0, 2);   
            switch (per)
            {
                case 0:
                    Debug.Log("0");
                    Item.SetActive(true);
                    ItemCount = 1;
                    break;
                case 1:
                    Debug.Log("1");
                    Item.SetActive(false);
                    ItemCount = 1;
                    break;

            }
        }

        #endregion
    }

}
