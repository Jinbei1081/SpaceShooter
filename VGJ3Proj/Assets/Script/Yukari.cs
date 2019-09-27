using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Yukari : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new SpriteRenderer renderer;
    private new PolygonCollider2D collider;
    [SerializeField] ParticleSystem damageParticle;
    public float speed = 15f;

    [SerializeField] int left = 5;
    [SerializeField] bool isDamaged = false;
    [SerializeField] bool playInvincibleEffect = false;
    [SerializeField] int shotType;
    float repeatSpan = 0.06f;
    [Header("NWay")]
    [SerializeField] int angle;
    [SerializeField] int num;
    [Space]
    [Space]
    public GameObject bullet;

    [SerializeField] Image back_normal;
    [SerializeField] Image back_burst;
    [SerializeField] Image back_3way;

    TextMeshProUGUI burstVolume;
    TextMeshProUGUI wayVolume;

    GameObject spawner;

    bool one;

    float elapsedTime;
    float interval = 0.4f;

    enum ShotType
    {
        Normal, Repeating, Way
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();

        shotType = (int)ShotType.Normal;
        back_normal.enabled = true;
        back_burst.enabled = false;
        back_3way.enabled = false;

        burstVolume = GameObject.Find("Canvas/burst_left").GetComponent<TextMeshProUGUI>();
        wayVolume = GameObject.Find("Canvas/3way_left").GetComponent<TextMeshProUGUI>();

        spawner= GameObject.Find("Spawner");

    }

    private void Update()
    {
        if (left <= 0)
        {
            CancelInvoke();
            spawner.GetComponent<Spawner>().Stop();
            if (!one)
            {
                SceneManager.LoadScene("Result", LoadSceneMode.Additive);
                one = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            back_normal.enabled = true;
            back_burst.enabled = false;
            back_3way.enabled = false;
            shotType = (int)ShotType.Normal;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            back_normal.enabled = false;
            back_burst.enabled = true;
            back_3way.enabled = false;
            shotType = (int)ShotType.Repeating;

            ResetShot(burstVolume);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            back_normal.enabled = false;
            back_burst.enabled = false;
            back_3way.enabled = true;
            shotType = (int)ShotType.Way;

            ResetShot(wayVolume);
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDamaged)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody.velocity = Vector2.right * speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidbody.velocity = Vector2.left * speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rigidbody.velocity = Vector2.up * speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rigidbody.velocity = Vector2.down * speed;
            }
        }
        if (playInvincibleEffect)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 5));
            renderer.color = new Color(1f, 1f, 1f, level);
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime < interval) return;

        elapsedTime = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            switch (shotType)
            {
                case (int)ShotType.Normal:
                    ShotBullet();
                    break;
                case (int)ShotType.Repeating:
                    if (int.Parse(burstVolume.text) <= 0)
                    {
                        ResetShot(burstVolume);
                        break;
                    }
                    StartCoroutine("ShotNTime", 3);
                    burstVolume.text = (int.Parse(burstVolume.text) - 1).ToString();
                    break;
                case (int)ShotType.Way:
                    if (int.Parse(wayVolume.text) <= 0)
                    {
                        ResetShot(wayVolume);
                        break;
                    }
                    ShotNWay(angle, num);
                    wayVolume.text = (int.Parse(wayVolume.text) - 1).ToString();
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        if (isDamaged) return;

        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.03f, 0.1f));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.97f, 0.83f));

        var pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void ShotBullet()
    {
        var pos = transform.position;
        pos.x += 0.324f;
        pos.y += -0.197f;
        var m_bullet = Instantiate(bullet, pos, Quaternion.identity);
        m_bullet.GetComponent<Bullet>().Init(Vector2.right, 3);
    }

    void ShotBullet(int _angle = 0)
    {
        var pos = transform.position;
        pos.x += 0.324f;
        pos.y += -0.197f;
        var m_bullet = Instantiate(bullet, pos, Quaternion.identity);
        m_bullet.GetComponent<Bullet>().Init(Quaternion.Euler(new Vector3(0, 0, _angle)) * Vector3.right, 3);
    }

    IEnumerator ShotNTime(int _times)
    {
        for (int i = 0; i < _times; i++)
        {
            ShotBullet();
            yield return new WaitForSeconds(repeatSpan);
        }
    }

    void ShotNWay(int _angle, int _num)
    {
        int perAngle = _angle / (_num - 1);
        int shotAngle = -(_angle / 2);
        for (int i = 0; i < _num; i++)
        {

            ShotBullet(shotAngle);
            shotAngle += perAngle;
        }

    }

    private void ResetShot(TextMeshProUGUI _text)
    {
        if (int.Parse(_text.text) <= 0)
        {
            back_normal.enabled = true;
            back_burst.enabled = false;
            back_3way.enabled = false;
            shotType = (int)ShotType.Normal;
        }
    }

    void Revival()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0;
        rigidbody.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
        transform.position = new Vector3(-10, 0, 0);
        damageParticle.Stop();
        Invoke("RevivalMove", 1f);
    }

    void RevivalMove()
    {
        isDamaged = false;
        rigidbody.drag = 25;
        Invoke("RevivalComplete", 1f);
    }

    void RevivalComplete()
    {
        playInvincibleEffect = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);
        collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((collision.tag == "Enemy") || (collision.tag == "EnemyBullet"))) return;
        isDamaged = true;
        playInvincibleEffect = true;
        left--;
        GameObject.Find("Left").GetComponent<RawImage>().uvRect = new Rect(0.2f * (5 - left), 0, 1, 1);
        rigidbody.gravityScale = 1;
        rigidbody.drag = 0;
        rigidbody.velocity = Vector2.zero;
        collider.enabled = false;
        damageParticle.Play();
        Invoke("Revival", 2);
    }

}
