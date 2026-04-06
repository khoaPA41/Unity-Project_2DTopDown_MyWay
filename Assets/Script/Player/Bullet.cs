using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    PooledObject pooledObject;
    Rigidbody2D bulletRB;

    public Vector2 diretion { get; set; }
    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
        bulletRB = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(ReleaseBulletByTime());
    }

    IEnumerator ReleaseBulletByTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Debug.Log("Release Bullet");
        pooledObject.Release();
    }

    void FixedUpdate()
    {
        bulletRB.MovePosition((Vector2)transform.position + (diretion * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PooledObject enemy = collision.GetComponent<PooledObject>();
            enemy.Release();
            pooledObject.Release();
        }
    }
}
