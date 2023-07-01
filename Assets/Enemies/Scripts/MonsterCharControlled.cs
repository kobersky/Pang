using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterCharControlled : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private CharacterController _characterController;

    private bool isLeft = true;
    private float _startingSpeed = 5;
    private float _currentSpeed = 0;

    private Vector3 _startingSpeedVector;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _startingSpeedVector = new Vector3(5, -5, 0);
        _currentSpeed = _startingSpeed;
    }

    private void FixedUpdate()
    {
        _characterController.Move(_startingSpeedVector * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit controllerColliderHit)
    {
        if (controllerColliderHit.collider.tag == "Wall")
        {
            Debug.Log("OnCollisionEnter Wall");

            // _rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }

        if (controllerColliderHit.collider.tag == "Floor")
        {
            Debug.Log("OnCollisionEnter Floor");

           // _rigidBody.AddForce(new Vector3(_currentXForce, _floorCollisionBounceForce, 0));
        }

    }
}
