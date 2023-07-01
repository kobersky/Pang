using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterPhysicsPure : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] Animator _animator;
    [SerializeField] Collider _collider;

    [SerializeField] float _startingXForce = 50;
    [SerializeField] float _floorCollisionBounceForce = 500;

    float _currentXForce = 0;

    float _lastYSpeed;
    private void Start()
    {
        _currentXForce = _startingXForce;
        _rigidBody.AddForce(new Vector3(_currentXForce, 0, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
         //   Debug.Log("OnCollisionEnter Floor");

            _rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }

        if (collision.collider.tag == "Wall")
        {
         //   Debug.Log("OnCollisionEnter Wall");
            _currentXForce = -_currentXForce;

            _rigidBody.velocity = new Vector3(-_rigidBody.velocity.x, _rigidBody.velocity.y, 0);

            //_rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }
    }
}
