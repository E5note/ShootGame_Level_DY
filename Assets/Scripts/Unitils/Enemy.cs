using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    private Animator _anim;

    void Start()
    {
        _anim =  GetComponent<Animator>();
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            _anim.SetBool(nameof(Die), true);
            GameManager.Instance.EnemyDestroyed();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检测坠落物体
        if (collision.relativeVelocity.magnitude > 5f &&
            collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }
}