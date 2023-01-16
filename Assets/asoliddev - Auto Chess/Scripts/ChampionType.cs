using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a group a champion can belong to and what bonuses a player can get
/// </summary>
[CreateAssetMenu(fileName = "DefauultChampionType", menuName = "AutoChess/ChampionType", order = 2)]
public class ChampionType : ScriptableObject
{
    ///Displayed name on UI
    public string displayName = "name";

    ///Displayed sprite on the UI
    public Sprite icon;

    ///Bonuses this ChampionType has
    public ChampionBonus championBonus;

}
