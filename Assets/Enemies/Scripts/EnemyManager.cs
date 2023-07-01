using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _monsterCount;

    public static event Action OnAllMonstersKilled;

    private void Awake()
    {
        MonsterPhysicsPure.OnMonsterBorn += AddToMonsterCount;
        MonsterPhysicsPure.OnMonsterDied += DeductDromMonsterCount;
    }

    private void OnDestroy()
    {
        MonsterPhysicsPure.OnMonsterBorn -= AddToMonsterCount;
        MonsterPhysicsPure.OnMonsterDied -= DeductDromMonsterCount;
    }

    private void AddToMonsterCount()
    {
        _monsterCount++;
        Debug.Log($"LEVEL: EnemyManager: _monsterCount: {_monsterCount}");
    }

    private void DeductDromMonsterCount(Vector3 position)
    {
        _monsterCount--;
        Debug.Log($"LEVEL: EnemyManager: _monsterCount: {_monsterCount}");
        if (_monsterCount == 0)
        {
            OnAllMonstersKilled?.Invoke();
        }
    }
}
