using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenProjectileScript : MonoBehaviour
{
    GameObject playerShip;
    Vector3 target;
    Rigidbody2D projectileRigidBody;

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindWithTag("Player");

        projectileRigidBody = GetComponent<Rigidbody2D>();
        target = playerShip.transform.position;

        Vector3 movementDir = target - transform.position;

        RotateGameObject(target, 200, 100);
        projectileRigidBody.velocity = movementDir;
    }

    private void RotateGameObject(Vector3 target, float RotationSpeed, float offset)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }
}
