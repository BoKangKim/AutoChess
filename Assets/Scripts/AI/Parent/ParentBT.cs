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
        protected STAGETYPE stageType = STAGETYPE.PREPARE;

        protected Animator myAni = null;
        private List<ParentBT> enemies = null;
        private ParentBT[] allUnits = null;

        protected string myType = null;
        [SerializeField] protected string nickName = null;
        [SerializeField] protected GameObject effect = null;

        protected LocationXY myLocation;
        protected LocationXY next;
        protected Vector3 nextPos;
        protected Vector3 dir;

        Vector3 nextLocation = Vector3.zero;
        protected RTAstar rta = null;

        private float currentHP = 100f;
        protected bool ishit = false;

        private ScriptableUnit unitData;

        protected float attackRange;
        #region GET,SET

        public void SetState(STAGETYPE state)
        {
            this.stageType = state;
        }

        public ScriptableUnit getUnitData()
        {
            return unitData;
        }

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
        #endregion

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
            //StageControl sc = FindObjectOfType<StageControl>();
            //sc.changeStage = changeStage;

            //next = rta.searchNextLocation(myLocation, target.getMyLocation());
            //nextPos = LocationControl.convertLocationToPosition(next);
            //dir = (nextPos - transform.position).normalized;
        }

        private void Update()
        {
            if (stageType == STAGETYPE.PREPARE)
            {
                return;
            }

            if (specialRoot != null 
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
                        IfAction(isDeath, death)
                    ),

                    Sequence
                    (
                        ActionN(idle),
                        NotIf(findEnemy)
                    ),

                    Sequence
                    (
                        IfElseAction(isArangeIn, attack, move)
                        //IfAction(isCenter,attack)
                    )
                );
        }

        protected virtual void initializingSpecialRootNode() { }
        protected abstract string initializingMytype();

        public void changeStage(STAGETYPE stageType)
        {
            this.stageType = stageType;
        }

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
            else
            {
                addEnemyList(fieldAIObejects, "Monster");
            }
            
            
        }

        protected virtual void searchingTarget()
        {
            float minDistance = 100000f;
            float temp = 0f;
        
            for (int i = 0; i < enemies.Count; i++)
            {
                if ((enemies[i].transform.position.y < 0 || enemies[i].transform.position.y > 7.5f))
                {
                    continue;
                }

                if ((temp = Vector3.Distance(enemies[i].transform.position,transform.position)) <= minDistance)
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
                    myLocation = LocationControl.convertPositionToLocation(gameObject.transform.position);
                    
                    if (enemies.Count == 0)
                    {
                        findEnemyFuncOnStart((allUnits = FindObjectsOfType<ParentBT>()));
                        searchingTarget();

                        next = rta.searchNextLocation(myLocation, target.getMyLocation());
                        nextPos = LocationControl.convertLocationToPosition(next);
                        dir = (nextPos - transform.position).normalized;

                        if (gameObject.name.CompareTo("Defender (2)") == 0)
                        {
                            Debug.Log(next.ToString() + " T " + target.getMyLocation().ToString());
                        }
                    }
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
                    if (Vector3.Distance(centerPosition, transform.position) <= 0.25f)
                        return;

                    Vector3 cDir = (centerPosition - transform.position).normalized;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(cDir), Time.deltaTime * 10f);
                    gameObject.transform.Translate(cDir * 1f * Time.deltaTime, Space.World);
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
                    if (Vector3.Distance(centerPosition, transform.position) <= 0.25f)
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
                    if(myAni.GetBool("isMove") == false)
                    {
                        myAni.SetBool("isMove",true);
                    }

                    myLocation = LocationControl.convertPositionToLocation(transform.position);
                    if (Vector3.Distance(nextPos, transform.position) <= 0.2f)
                    {
                        next = rta.searchNextLocation(myLocation, target.getMyLocation());
                        nextPos = LocationControl.convertLocationToPosition(next);
                        dir = (nextPos - transform.position).normalized;
                    }

                    
                    transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir), Time.deltaTime * 10f);
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
                        if (Vector3.Distance(enemies[i].transform.position, transform.position) <= (LocationControl.radius * attackRange)
                        && checkIsOverlapUnits() == false)
                        {
                            target = enemies[i];
                            next = myLocation;
                            return true;
                        }
                    }

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
                    this.transform.LookAt(target.transform.position);
                    myAni.SetBool("isMove",false);
                    //target.setIsHit(true);
                    //target.Damage(1f);
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
                    //myAni.SetTrigger("isDeath");
                };
            }
        }

        protected virtual Action hit 
        {
            get 
            {
                return () =>
                {
                };
            }
        }

        #endregion

        public virtual void StartEffect()
        {
            GameObject _effect = Instantiate<GameObject>(effect,transform.position,Quaternion.LookRotation(transform.forward));
            //_effect.transform.localScale *= LocationControl.getDistance(myLocation,target.getMyLocation());
        }

        
    }

}








