using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DynamicPlayer : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Vector2 _horizontalMovement;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    //works when non kinematic only
    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector3(_horizontalMovement.x * 500 * Time.fixedDeltaTime, 0, 0);        
    }

    //same, but uneven movement
/*    private void FixedUpdate()
    {
        _rigidBody.AddForce(_horizontalMovement * 500 *  Time.deltaTime);        
    }*/

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
