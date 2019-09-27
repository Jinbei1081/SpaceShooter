using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    [SerializeField] float speed;
    GameObject player;
    Rigidbody2D rb;

    [SerializeField] ParticleSystem steam;

    bool isChase = true;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("yukari");
        rb = GetComponent<Rigidbody2D>();
        Invoke("ChaseCancel", 7);
    }

    void ChaseCancel()
    {
        isChase = false;
        rb.gravityScale = 30;
        steam.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isChase) return;
        Vector3 diff = (player.transform.position - this.transform.position);
        diff.Normalize();
        rb.velocity = new Vector3(diff.x * speed, diff.y * speed);
        //回転
        transform.localRotation = Quaternion.FromToRotation(Vector3.left, diff);

        if (GetComponent<Enemy>().hp <= 0) steam.Stop();
    }

}
