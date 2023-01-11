using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores basic Game Data
/// </summary>
public class GameData : MonoBehaviour
{
    ///Store all available champion, all champions must be assigned from the Editor to the Script GameObject
    public Champion[] championsArray;

    ///Store all available championType, all championTypes must be assigned from the Editor to the Script GameObject
    public ChampionType[] championTypesArray; 
}
