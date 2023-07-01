using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class KinematicPlayer : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Vector2 _horizontalMovement;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(_horizontalMovement * 5 * Time.deltaTime);
    }

    public void OnFire(InputValue inputValue)
    {

        Debug.Log($"INPUT: firing!");
    }

    public void OnMove(InputValue inputValue)
    {
        _horizontalMovement = inputValue.Get<Vector2>();
        Debug.Log($"INPUT: moving! [{_horizontalMovement.x}, {_horizontalMovement.y}");
    }
}
