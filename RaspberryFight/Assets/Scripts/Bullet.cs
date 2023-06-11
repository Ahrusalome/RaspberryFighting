using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public int bounce = 0;
    public int damage;
    public bool hack = false;

    public void Launch(Vector3 direction)
    {
        direction = -transform.right+direction;
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (bounce > 0 && other.gameObject.name == "wallLeft") {
            rb.AddForce(new Vector2(200f,100f));
            bounce--;
        } else if (bounce > 0 && other.gameObject.name == "wallRight") {
            rb.AddForce(new Vector2(-200f,100f));
            bounce--;
        } else if (bounce > 0 && other.gameObject.name == "wallUp") {
            rb.AddForce(new Vector2(0,-100f));
            bounce--;
        } else if (other.gameObject.name == gameObject.name) {
        }
        else {
            Destroy(gameObject);
        }
    }
}
