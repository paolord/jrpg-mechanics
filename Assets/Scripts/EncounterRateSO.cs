using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EncounterItem
{
    public EncounterSO encounterType;
    public int encounterRate;
}

[CreateAssetMenu (fileName = "New EncounterRates", menuName = "Scriptable Objects/Encounter Rates")]
public class EncounterRateSO : ScriptableObject
{
    [SerializeField] public List<EncounterItem> encounters;
}
