using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HitPoints))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private HitPoints hitPoints;


    public float moveSpeed = 5f;

    public float dodgeCooldownTime = 1f;
    private bool canDodge;

    Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitPoints = GetComponent<HitPoints>();

        canDodge = true;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetMouseButtonDown(1) && canDodge)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
            Vector3 lookDir3D = new Vector3(lookDir.x, lookDir.y, 0).normalized;
            transform.position += lookDir3D * 5;
            StartCoroutine(DodgeCooldown());
        }

    }

    IEnumerator DodgeCooldown()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCooldownTime);
        canDodge = true;

    }
    private void FixedUpdate()
    {
        rb.velocity += moveSpeed * Time.fixedDeltaTime * movement.normalized;
    }
    public void PlayerHit(HitEventData data)
    {
        rb.AddForce(data.explosionForce);
        hitPoints.TakeDamage(data.damage);
        GetComponent<AudioSource>().Play();
    }
}
