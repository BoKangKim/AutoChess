using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChampionBonusType {Damage, Defense, Stun, Heal};
public enum BonusTarget {Self, Enemy};

/// <summary>
/// Controls the bonuses to get when have enough champions of the same type
/// </summary>
[System.Serializable]
public class ChampionBonus
{
    ///How many champions needed to get the bonus effect
    public int championCount = 0;

    ///Type of the bonus
    public ChampionBonusType championBonusType;

    ///Target of the bonus
    public BonusTarget bonusTarget;

    ///Float value of the bonus
    public float bonusValue = 0;

    ///How many secounds bonus lasts
    public float duration;

    ///Prefab to instantiate when bonus occours
    public GameObject effectPrefab;

   /// <summary>
   /// Calculates bonuses of a champion when attacking
   /// </summary>
   /// <param name="champion"></param>
   /// <param name="targetChampion"></param>
   /// <returns></returns>
    public float ApplyOnAttack(ChampionController champion, ChampionController targetChampion)
    {
        
        float bonusDamage = 0;
        bool addEffect = false;
        switch (championBonusType)
        {
            case ChampionBonusType.Damage :
                bonusDamage += bonusValue;
                break;
            case ChampionBonusType.Stun:
                int rand = Random.Range(0, 100);
                if (rand < bonusValue)
                {
                    targetChampion.OnGotStun(duration);
                    addEffect = true;
                }
                break;
            case ChampionBonusType.Heal:
                champion.OnGotHeal(bonusValue);
                addEffect = true;
                break;
            default:
                break;
        }


        if (addEffect)
        {
            if (bonusTarget == BonusTarget.Self)
               champion.AddEffect(effectPrefab, duration);
            else if (bonusTarget == BonusTarget.Enemy)
               targetChampion.AddEffect(effectPrefab, duration);
        }
      

        return bonusDamage;
    }

    /// <summary>
    /// Calculates bonuses of a Champion when got hit
    /// </summary>
    /// <param name="champion"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public float ApplyOnGotHit(ChampionController champion, float damage)
    {
        switch (championBonusType)
        {        
            case ChampionBonusType.Defense:
                damage = ((100 - bonusValue) / 100) * damage;
                break;   
            default:
                break;
        }

        return damage;
    }
}
