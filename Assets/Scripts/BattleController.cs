using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    private StartBattleEventSO _battleEvent;
    private EncounterSO _encounter;
    private bool _start;
    private bool _battleSceneLoaded;

    private void Awake()
    {
        _battleEvent = Resources.Load<StartBattleEventSO>("ScriptableObjects/StartBattleEvent");
        _start = false;
        _battleSceneLoaded = false;

        DontDestroyOnLoad(this.gameObject);
    }

    public void StoreEncounter(EncounterSO encounter)
    {
        _encounter = encounter;
    }

    private void Update()
    {
        if (_encounter != null && _start && !_battleSceneLoaded)
        {
            _battleSceneLoaded = true;
            //Debug.Log(_encounter);
            SceneManager.LoadScene("BattleScene");
        }
        // TODO: Call ATBEvent to give encounter data
    }

    public void StartBattle()
    {
        _start = true;
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
