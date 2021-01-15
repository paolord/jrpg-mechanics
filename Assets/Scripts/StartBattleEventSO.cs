using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StartBattleEvent", menuName = "Scriptable Objects/Start Battle Event")]
public class StartBattleEventSO : ScriptableObject
{
    private BattleController _battleManager;

    public void EncounterDetermined(EncounterSO encounter)
    {
        if (_battleManager != null)
        {
            _battleManager.StoreEncounter(encounter);
        }
    }

    public void TransitionAnimationEnd()
    {
        if (_battleManager != null)
        {
            _battleManager.StartBattle();
        }
    }

    public void RegisterBattleManager(BattleController battleManager)
    {
        _battleManager = battleManager;
    }

    public void UnregisterBattleManager()
    {
        _battleManager = null;
    }
}
