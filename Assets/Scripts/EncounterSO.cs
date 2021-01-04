using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MobItem
{
    public CharacterBaseSO characterBase;
    public string label;
    public int level;
}

[CreateAssetMenu (fileName = "New Encounter", menuName = "Scriptable Objects/Encounter")]
public class EncounterSO : ScriptableObject
{
    [SerializeField] public List<MobItem> Mob;

    private void OnEnable()
    {
        
    }

    public void ApplyLevels()
    {
        foreach(MobItem monster in Mob)
        {
            monster.characterBase.ApplyLevel(monster.level);
        }
    }
}
