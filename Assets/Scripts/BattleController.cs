using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    private StartBattleEventSO _battleEvent;
    private EncounterSO _encounter;

    private void Awake()
    {
        _battleEvent = Resources.Load<StartBattleEventSO>("ScriptableObjects/StartBattleEvent");

        DontDestroyOnLoad(this.gameObject);
    }

    public void StoreEncounter(EncounterSO encounter)
    {
        _encounter = encounter;
    }

    private void Update()
    {
        // TODO: Call ATBEvent to give encounter data
    }

    public void StartBattle()
    {
        Debug.Log(_encounter);
        //SceneManager.LoadScene("BattleScene");
    }

    private void OnEnable()
    {
        _battleEvent.RegisterBattleManager(this);
    }

    private void OnDisable()
    {
        _battleEvent.UnregisterBattleManager();
    }
}
