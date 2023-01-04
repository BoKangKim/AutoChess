using UnitStats;
using static UnitStats.ApplyUnitStats;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    const int grade1MaxUnitCount = 20;
    class Zone1P
    {

    }

    enum SpeciesSynerge
    {
        SPECIESGRADE1DWARF,
        SPECIESGRADE2DWARF,
        SPECIESGRADE1UNDEAD,
        SPECIESGRADE2UNDEAD,
        SPECIESGRADE1SCORPION,
        SPECIESGRADE2SCORPION,
        SPECIESGRADE1ORC,
        SPECIESGRADE2ORC,
        SPECIESGRADE1MECHA,
        SPECIESGRADE2MECHA,
        SPECIESGRADE1WARRIOR,
        SPECIESGRADE2WARRIOR,  
    }

    enum ClassSynerge
    {
        CLASSGRADE1WARRIOR,
        CLASSGRADE2WARRIOR,
        CLASSGRADE1TANKER,
        CLASSGRADE2TANKER,
        CLASSGRADE1MAGICIAN,
        CLASSGRADE2MAGICIAN,
        CLASSGRADE1RANGEDEALER,
        CLASSGRADE2RANGEDEALER,
        CLASSGRADE1ASSASSIN,
        CLASSGRADE2ASSASSIN,
    }

    public void SetSynergy()
    {
        GetUnitStats();
    }

    
}
