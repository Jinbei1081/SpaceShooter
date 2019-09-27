using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] public int hp=1;
    [SerializeField] int score=0;

    [SerializeField] ParticleSystem dethParticle;
    bool playParticle = false;

    GameObject text;

    [SerializeField] [Range(0, 100)] int burstDrop; 
    [SerializeField] [Range(0, 100)] int wayDrop;
    [SerializeField] GameObject burst;
    [SerializeField] GameObject way;


    new SpriteRenderer renderer;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        text = GameObject.Find("Canvas/score");
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x <= -10.5f) || (transform.position.y <= -6.5f))
        {
            Destroy(gameObject);
            return;
        }
        if (!dethParticle.isPlaying && playParticle)
        {
            //スコア加算
            text.GetComponent<Score>().AddScore(score);

            var rand = Random.Range(0, 100);
            if((rand > 0) && (rand <= burstDrop)) { Instantiate(burst, transform.position, Quaternion.identity); }
            if((rand > burstDrop) && (rand <= wayDrop+burstDrop)) { Instantiate(way, transform.position, Quaternion.identity); }

            Destroy(gameObject);
            return;
        }

        if (hp <= 0)
        {
            if (!playParticle) {
                dethParticle.Play();
                playParticle = true;

                //当たり判定を消す
                Destroy(GetComponent<PolygonCollider2D>());
                Destroy(GetComponent<EdgeCollider2D>());

                rb.velocity = Vector2.zero;
            }

            var col=renderer.color;
            col.a -= 0.05f;
            renderer.color = col;
        }
        else
        {
            //左に移動
            rb.velocity = Vector2.left * speed;
        }
        
    }
}
