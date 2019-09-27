using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBulletBurst : MonoBehaviour
{

    [SerializeField] public int BulletVolume;

    GameObject text;

    private void Start()
    {
        text = GameObject.Find("Canvas/burst_left");
        GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            text.GetComponent<TextMeshProUGUI>().text = BulletVolume.ToString();
            Destroy(gameObject);
        }
    }
}
