using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* EnemyManager spawns enemies, counts active enemies 
 * and reports when all of tem are killed */

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

    private void OnDestroy()
    {
        Monster.OnMonsterBorn -= AddToMonsterCount;
        Monster.OnMonsterDied -= DeductDromMonsterCount;
    }

    private void CreateEnemy(GameObject prefab, Vector3 currentTransform, bool isFacingRight)
    {
        var monster = Instantiate(prefab, currentTransform, Quaternion.identity);
        monster.GetComponent<Monster>().InitMovement(isFacingRight);
    }

    private void AddToMonsterCount()
    {
        _monsterCount++;
    }

    private void DeductDromMonsterCount(EnemyType enemyType, Vector3 position)
    {
        _monsterCount--;

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
