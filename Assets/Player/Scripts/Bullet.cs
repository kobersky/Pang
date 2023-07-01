using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 20f;
    Rigidbody shotRigidBody;

    private void Awake()
    {
        shotRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        shotRigidBody.AddForce(Vector3.up * projectileSpeed);
    }

    /*    private void FixedUpdate()
        {
            shotRigidBody.velocity = new Vector3(0, projectileSpeed * 10, 0);
        }*/

    //moving up, but no trigger
    /*    private void Update()
        {
            transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
        }*/

    private void OnTriggerEnter(Collider collider)
    {
/*        Debug.Log("BARK: OnTriggerEnter2D:" + ", of layer: " + LayerMask.LayerToName(collision.gameObject.layer) + ", object name: " + collision.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer(LayerTypes.ENEMIES) ||
            collision.gameObject.layer == LayerMask.NameToLayer(LayerTypes.FOREGROUND) ||
            collision.gameObject.layer == LayerMask.NameToLayer(LayerTypes.HAZARDS) ||
            collision.gameObject.layer == LayerMask.NameToLayer(LayerTypes.PLATFORMS) ||
            collision.gameObject.layer == LayerMask.NameToLayer(LayerTypes.TRAMPOLINES))
        {

            shotRigidBody.velocity = new Vector2(0, 0);
            shotAnimator.SetTrigger(AnimationKeys.EnemyAnimationKeys.HIT_SURFACE);
            AudioSource.PlayClipAtPoint(hitSurfaceSound, Camera.main.transform.position);
        }*/

    }

    public void OnBarkEnd()
    {
        Destroy(gameObject);
    }
}
