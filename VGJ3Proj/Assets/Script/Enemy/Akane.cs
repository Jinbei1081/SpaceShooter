using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akane : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    GameObject player;

    public bool Alone { get; set; }
    bool one = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("yukari");
        InvokeRepeating("ShotBullet",0,2);
    }

    private void Update()
    {
        if (Alone)
        {
            if (!one)
            {
                one = true;
                CancelInvoke("ShotBullet");
                InvokeRepeating("ShotBullet", 0, 1f);
            }
        }
    }

    void ShotBullet()
    {
        var diff = (player.transform.position - this.transform.position);
        var m_bullet = Instantiate(bullet, transform.position, Quaternion.identity);
        m_bullet.GetComponent<Bullet>().Init(diff, 5);
    }
}
