using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collisionGameObject)
    {   

            if (collisionGameObject.gameObject.tag.Equals("Enemy"))
            {
                EnemyBehaviour1 enemyHealth = collisionGameObject.gameObject.GetComponent<EnemyBehaviour1>();
                enemyHealth.DinoGreenHit();
                Destroy(gameObject);
            }
            else if (collisionGameObject.gameObject.tag.Equals("Destructable"))
            {
                Destroy(collisionGameObject.gameObject);
                Destroy(gameObject);
            }
            else if (collisionGameObject.gameObject.tag.Equals("jumpPower") || collisionGameObject.gameObject.tag.Equals("speedPower") || collisionGameObject.gameObject.tag.Equals("speedjumpPower"))
            {
                // do nothing
            }
    }

   }