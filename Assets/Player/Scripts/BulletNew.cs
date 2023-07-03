using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BulletNew generic behaviour of a player projectile - movement, detection by colliders, etc. */

public class BulletNew : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 20f;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _characterController.Move(Vector3.up * projectileSpeed * Time.fixedDeltaTime);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        Destroy(gameObject);

    }
}
