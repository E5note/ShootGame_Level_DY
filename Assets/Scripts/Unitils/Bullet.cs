using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float speed = 30f;

    public int maxBounces = 3;
    private int bounceCount;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 5f); // 5秒后自动销毁
    }

    public void Shoot(Vector2 direction)
    {
        this.direction = direction;
        rb.velocity = this.direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        var firstContact = collision.contacts[0];
        Vector2 newVelocity = Vector2.Reflect(direction.normalized, firstContact.normal);
        Shoot(newVelocity.normalized);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Die();
        }
        else
        {
            bounceCount++;
            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
}