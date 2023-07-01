using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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

/*    
 *    not called
 *    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"IMPACT: BulletNew: OnTriggerEnter: {collider.tag}");

    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"IMPACT: BulletNew: OnCollisionEnter: {collision.collider.tag}");
        Destroy(gameObject);
    }

    public void OnBarkEnd()
    {
        Destroy(gameObject);
    }
}
