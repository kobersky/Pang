using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject _bigMonsterPrefab;
    [SerializeField] GameObject _mediumMonsterPrefab;
    [SerializeField] GameObject _smallMonsterPrefab;

    [SerializeField] List<Transform> _startingPositions;

    private int _monsterCount;

    public static event Action OnAllMonstersKilled;

    private void Awake()
    {
        Monster.OnMonsterBorn += AddToMonsterCount;
        Monster.OnMonsterDied += DeductDromMonsterCount;
    }

    private void OnEnable()
    {
        foreach (Transform currentTransform in _startingPositions)
        {
            CreateEnemy(_bigMonsterPrefab, currentTransform.position, true);
        }
    }

    private void OnDisable()
    {
        Debug.Log($"LEVEL: EnemyManager: OnDisable: ");
        Monster.OnMonsterBorn -= AddToMonsterCount;
        Monster.OnMonsterDied -= DeductDromMonsterCount;
    }

    private void CreateEnemy(GameObject prefab, Vector3 currentTransform, bool isFacingRight)
    {
        Debug.Log($"LEVEL: EnemyManager: CreateEnemy: {prefab.gameObject.name}");
        var monster = Instantiate(prefab, currentTransform, Quaternion.identity);
        monster.GetComponent<Monster>().InitMovement(isFacingRight);
    }

    private void AddToMonsterCount()
    {
        _monsterCount++;
        Debug.Log($"LEVEL: EnemyManager: _monsterCount: {_monsterCount}");
    }

    private void DeductDromMonsterCount(EnemyType enemyType, Vector3 position)
    {
        _monsterCount--;
        Debug.Log($"LEVEL: EnemyManager: _monsterCount: {_monsterCount}, isActiveInHierarchy: {isActiveAndEnabled}");

        if (enemyType == EnemyType.BigMonster)
        {
            CreateEnemy(_mediumMonsterPrefab, position, true);
            CreateEnemy(_mediumMonsterPrefab, position, false);
        }

        else if (enemyType == EnemyType.MediumMonster)
        {
            CreateEnemy(_smallMonsterPrefab, position, true);
            CreateEnemy(_smallMonsterPrefab, position, false);
        }

        else if (_monsterCount == 0)
        {
            OnAllMonstersKilled?.Invoke();
        }
    }
}
