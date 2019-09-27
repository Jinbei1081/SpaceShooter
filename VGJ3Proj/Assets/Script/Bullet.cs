using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public int damage = 1;

    [SerializeField] GameObject effect;

    public void Init(Vector3 direction, float speed)
    {
        direction.Normalize();
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (gameObject.tag == "EnemyBullet") return;

            Instantiate(effect,transform.position,Quaternion.identity);

            collision.GetComponent<Enemy>().hp -= damage;
            Destroy(gameObject);
        }
    }
}
