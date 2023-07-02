using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] EnemyType _enemyType;

    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _collider;

    [SerializeField] float _startingXForce = 50;
    [SerializeField] float _floorCollisionBounceForce = 500;
    [SerializeField] float _platformCollisionBounceForce = 250;

    float _currentXForce = 0;

    public static event Action OnMonsterBorn;
    public static event Action<EnemyType, Vector3> OnMonsterDied;

    public void InitMovement(bool isFacingRight)
    {
        _currentXForce = isFacingRight ? _startingXForce : - _startingXForce;
        _rigidBody.AddForce(new Vector3(_currentXForce, 0, 0));
    }

    private void OnEnable()
    {
        OnMonsterBorn?.Invoke();
    }

    private void OnDisable()
    {
        Debug.Log($"LEVEL: Monster: OnDisable: ");
        OnMonsterDied?.Invoke(_enemyType, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter (Monster): {collision.collider.tag}");

        if (collision.collider.tag == "Floor")
        {
            //   Debug.Log("OnCollisionEnter Floor");

            _rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }

        if (collision.collider.tag == "Platform")
        {
            //   Debug.Log("OnCollisionEnter Platform");
            _rigidBody.AddForce(new Vector3(_currentXForce, _platformCollisionBounceForce, 0));
        }

        if (collision.collider.tag == "Wall")
        {
            //   Debug.Log("OnCollisionEnter Wall");
            _currentXForce = -_currentXForce;

            _rigidBody.velocity = new Vector3(-_rigidBody.velocity.x, _rigidBody.velocity.y, 0);

            //_rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }


        if (collision.collider.tag == "PlayerProjectile")
        {
            Debug.Log($"IMPACT: Monster: OnCollisionEnter: {collision.collider.tag}");
            Die();//todo:revert after test
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
