using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    public static int hp = 100;

    public float kickBackForce;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity += moveSpeed * Time.fixedDeltaTime * movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Vector2 kickBackVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(kickBackVector * kickBackForce);
            hp -= 10;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log(hp);
    }
}
