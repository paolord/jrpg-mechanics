using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterController : MonoBehaviour
{
    [SerializeField] private float _baseEncounterRate;
    [SerializeField] private EncounterRateSO encounterRates;

    public EncounterSO currentEncounter;
    public UnityEvent StartBattle;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void RandomEncounter()
    {
        int encounterRoll = 0;
        int randomEncounterRoll = Mathf.FloorToInt(Random.value * 100);
        Debug.Log(randomEncounterRoll);
        // 20% Base Encounter rate;
        if (randomEncounterRoll >= 0 && randomEncounterRoll <= _baseEncounterRate)
        {
            foreach (EncounterItem encounter in encounterRates.encounters)
            {
                encounterRoll = Mathf.FloorToInt(Random.value * 100);
                if (encounterRoll >= 0 && encounterRoll <= encounter.encounterRate)
                {
                    currentEncounter = encounter.encounterType;
                    currentEncounter.ApplyLevels();
                    Debug.Log(currentEncounter.Mob[0].label);
                    Debug.Log(currentEncounter.Mob[1].label);
                    break; // break out of foreach, encounter type has been selected
                }
            }
            StartBattle.Invoke();
        }
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        currentEncounter = null; // clear encounter on scene change
    }
}
