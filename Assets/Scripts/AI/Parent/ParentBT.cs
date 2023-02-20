using UnityEngine;
using BehaviorTree;
using static BehaviorTree.BehaviorTreeMan;
using System;
using System.Collections.Generic;
using Battle.Stage;
using Battle.Location;
using Battle.RTASTAR;
using Battle.EFFECT;
using Photon.Pun;
using Photon.Realtime;

namespace Battle.AI
{
    [RequireComponent(typeof(UnitClass.Unit), typeof(Rigidbody), typeof(BoxCollider))]
    public abstract class ParentBT : MonoBehaviourPun
    {
        #region MyData
        private INode root = null;
        private INode specialRoot = null;
        protected ParentBT target = null;
        protected ParentBT skilltarget = null;

        protected STAGETYPE stageType = STAGETYPE.PREPARE;

        protected Animator myAni = null;
        private List<ParentBT> enemies = null;
        private List<ParentBT> myUnits = null;

        private ParentBT[] allUnits = null;

        protected string myType = null;
        [SerializeField] protected string nickName = null;
        [SerializeField] protected Effect standardAttackEffect = null;
        [SerializeField] protected SkillEffect skillEffect = null;

        protected LocationXY myLocation;
        protected LocationXY next;
        protected Vector3 nextPos;
        protected Vector3 dir;

        Vector3 nextLocation = Vector3.zero;
        protected RTAstar rta = null;
        protected UnitClass.Unit unitData = null;

        [Header("unitStatus")]
        private float currentHP = 100f;
        protected bool ishit = false;

        protected float attackDamage = 0f;
        protected float spellPower = 0f;
        protected float maxMana = 10f;
        protected float maxHP = 0f;
        protected float shield = 0f;

        protected float mana = 0f;
        protected float attackRange = 0f;
        protected float manaRecovery = 5f;

        private bool die = false;
        private bool isInit = false;
        private string enemyNickName = "";

        private Vector3 startposition;
        private bool isFirst = true;

        #endregion
        #region GET,SET

        public ParentBT getSkillTarget()
        {
            return skilltarget;
        }
        
        public void setAttackDamage(float addAtk)
        {
            attackDamage += addAtk;
        }
        public void setSpellPower(float addSpellPower)
        {
            attackDamage += addSpellPower;
        }
        public float getSpellPower()
        {
            return spellPower;
        }
        public Animator getAnimator()
        {
            return myAni;
        }
        public ParentBT getTarget()
        {
            return target;
        }
        public float getMaxHP()
        {
            return maxHP;
        }
        public void setShield(float addShield)
        {
            currentHP += addShield;
        }

        public void setRecoveryCurrentHP(float Recovery)
        {
            currentHP += Recovery;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            
        }
        public UnitClass.Unit getUnitData()
        {
            return unitData;
        }
        public void setAttackRange(float attackRange)
        {
            this.attackRange = attackRange;
        }

        public void setState(STAGETYPE state)
        {
            this.stageType = state;
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

        public void setMyLocation()
        {
            myLocation = LocationControl.convertPositionToLocation(gameObject.transform.localPosition);
        }

        public float getAttackDamage()
        {
            return attackDamage;
        }

        public bool getIsDeath()
        {
            return die;
        }
        public List<ParentBT> getFindEnemies()
        {
            return enemies;
        }
        public List<ParentBT> getFindMyUnits()
        {
            return myUnits;
        }
        public string getMyType()
        {
            return myType;
        }
        #endregion

        public void initUnit()
        {
            transform.position = startposition;
        }

        private void Awake()
        {
            InitializingRootNode();
            specialRoot = initializingSpecialRootNode();
            rta = new RTAstar(myLocation,gameObject.name);
            myType = initializingMytype();
            initializingData();
            nickName = PhotonNetwork.NickName;
        }

        private void Start()
        {
            myAni = GetComponent<Animator>();
            enemies = new List<ParentBT>();
            myLocation = LocationControl.convertPositionToLocation(gameObject.transform.localPosition);

            StageControl sc = FindObjectOfType<StageControl>();
            //sc.changeStage += changeStage;
        }

        private void Update()
        {
            if(photonView.IsMine == false)
            {
                return;
            }

            if(die == true)
            {
                return;
            }

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
            myLocation = LocationControl.convertPositionToLocation(transform.localPosition);
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

        protected virtual INode initializingSpecialRootNode() { return null; }
        protected abstract string initializingMytype();
        protected abstract float setAttackRange();

        public void doDamage()
        {
            target.Damage(attackDamage);
        }

        public void doDamage(float damage)
        {
            target.Damage(damage);
        }

        private void initializingData()
        {
            if (TryGetComponent<UnitClass.Unit>(out unitData) == false)
            {
                Debug.LogError("Not Found Unit Data");
            }

            currentHP = unitData.totalMaxHp;
            maxMana = unitData.totalMaxMp;
            maxMana = 5f;
            manaRecovery += unitData.totalMpRecovery;
            attackRange = unitData.totalAttackRange;
            attackDamage = unitData.totalAtkDamage;
            spellPower = unitData.totalSpellPower;
            myAni.speed = unitData.totalAttackSpeed;
        }

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
                case "UnitAI":
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
                if (fieldAIObejects[i].myType.CompareTo(compare) == 0
                    || fieldAIObejects[i].photonView.IsMine == false)
                {
                    continue;
                }

                enemies.Add(fieldAIObejects[i]);
            }

            Debug.Log(enemies.Count);
        }

        private void addMyUnitList(ParentBT[] fieldAIObejects)
        {
            if (stageType == STAGETYPE.PVP
                || stageType == STAGETYPE.CLONE)
            {
                for (int i = 0; i < fieldAIObejects.Length; i++)
                {
                 
                    if (fieldAIObejects[i].nickName.CompareTo(enemyNickName) == 0)
                    {
                        continue;
                    }

                    if (fieldAIObejects[i].enabled == false)
                    {
                        continue;
                    }

                    if (fieldAIObejects[i].nickName.CompareTo(nickName) == 0)
                    {

                        enemies.Add(fieldAIObejects[i]);
                    }
                }
            }
            else
            {
                addEnemyList(fieldAIObejects, "UnitAI");
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

                    if (fieldAIObejects[i].enabled == false)
                    {
                        continue;
                    }

                    if (fieldAIObejects[i].nickName.CompareTo(enemyNickName) == 0)
                    {
                        enemies.Add(fieldAIObejects[i]);
                    }

                }
            }
            else
            {
                addEnemyList(fieldAIObejects, "UnitAI");
            }
        }

        protected virtual void searchingTarget()
        {
            float minDistance = 100000f;
            float temp = 0f;
            target = null;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    continue;
                }

                if ((enemies[i].transform.localPosition.z < 0 || enemies[i].transform.localPosition.z > 12.5f))
                {
                    continue;
                }

                if ((temp = Vector3.Distance(enemies[i].transform.localPosition,transform.localPosition)) <= minDistance)
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
                if (allUnits[i] == null)
                {
                    continue;
                }

                unitLocation = LocationControl.convertPositionToLocation(allUnits[i].gameObject.transform.localPosition);
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
                    myLocation = LocationControl.convertPositionToLocation(gameObject.transform.localPosition);
                    if (enemies.Count == 0)
                    {
                        if(isInit == false)
                        {
                            photonView.RPC("RPC_EnableThis",RpcTarget.All);
                            findEnemyFuncOnStart((allUnits = FindObjectsOfType<ParentBT>()));
                            searchingTarget();

                            next = rta.searchNextLocation(myLocation, target.getMyLocation());
                            nextPos = LocationControl.convertLocationToPosition(next);
                            dir = (nextPos - new Vector3(transform.localPosition.x, nextPos.y,transform.localPosition.z)).normalized;
                            
                            isInit = true;
                        }
                        else
                        {
                            // ½Â¸® ·ÎÁ÷
                        }
                    }
                };
            }
        }

        [PunRPC]
        public void RPC_EnableThis()
        {
            if(this.enabled == false)
            {
                this.enabled = true;
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
                        myAni.SetBool("isMove",false);
                        dir = Vector3.zero;
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
                    myAni.SetBool("isMove",true);
                    myLocation = LocationControl.convertPositionToLocation(transform.localPosition);
                    if (Vector3.Distance(nextPos, transform.position) <= 0.2f)
                    {
                        next = rta.searchNextLocation(myLocation, target.getMyLocation());
                        nextPos = LocationControl.convertLocationToPosition(next);
                        dir = (nextPos - new Vector3(transform.localPosition.x, nextPos.y,transform.localPosition.z)).normalized;
                    }

                    transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir), Time.deltaTime * 10f);
                    gameObject.transform.Translate(dir * 5f * Time.deltaTime,Space.World);
                };
            }
        }

        protected virtual Func<bool> isArangeIn
        {
            get
            {
                return () =>
                {
                    
                    myLocation = LocationControl.convertPositionToLocation(transform.localPosition);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].getIsDeath() == true) 
                        {
                            enemies[i] = null;
                            continue;
                        }
                        if (enemies[i].isActiveAndEnabled == false)
                        {
                            target = null;
                            continue;
                        }

                        if (Vector3.Distance(enemies[i].transform.localPosition, transform.localPosition) <= (LocationControl.radius * attackRange)
                        && checkIsOverlapUnits() == false)
                        {
                            rta.initCloseList();
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
                    this.transform.LookAt(target.transform);
                    myAni.SetBool("isMove",false);
                    photonView.RPC("RPC_SetTriggerAttack",RpcTarget.All);
                };
            }
        }

        [PunRPC]
        public void RPC_SetTriggerAttack()
        {
            if(myAni == null)
            {
                if(gameObject.TryGetComponent<Animator>(out myAni) == false)
                {
                    Debug.Log("Not Found Animator" + gameObject.name);
                    return;
                }
            }

            myAni.SetTrigger("isAttack");
        }

        protected virtual Func<bool> isDeath
        {
            get
            {
                return () =>
                {
                    if(currentHP <= 0f)
                    {
                        die = true;
                        return true;
                    }

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
                    photonView.RPC("RPC_SetTriggerDeath",RpcTarget.All);
                };
            }
        }

        [PunRPC]
        public void RPC_SetTriggerDeath()
        {
            myAni.SetTrigger("isDeath");
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

        protected void DestroyObject()
        {
            Destroy(gameObject);
        }

        #endregion

        public virtual void StartEffect()
        {
            //GameObject _effect = Instantiate<GameObject>(effect,transform.position,Quaternion.LookRotation(transform.forward));
        }

        public virtual void StartSkillEffect()
        {
            
        }
    }

}








