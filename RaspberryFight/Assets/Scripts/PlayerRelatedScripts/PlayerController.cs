using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Vector2 movement = Vector2.zero;
    public float speed;
    private Animator animator;
    public bool grounded = false;
    public bool frontSpecialAttack = false;
    public bool isGuarding;

    public Vector3 ennemyPosition;

    void Start()
    {
        speed = GetComponent<Stats>().speed;
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Player sprite reduce to 0.3f ?
        this.transform.rotation = Quaternion.Euler(new Vector3(0, isFlipped() ? 180 : 0, 0));
        rbody.velocity = new Vector2(movement.x * speed/1.5f, rbody.velocity.y);
        animator.SetBool("Running", movement.x != 0);
        frontSpecialAttack = (movement.x != 0);
        if ((movement.x < 0 && this.GetComponent<RectTransform>().localScale.x > 0) ||
        (movement.x > 0 && this.GetComponent<RectTransform>().localScale.x < 0)){
            isGuarding = true;
        } else {
            isGuarding = false;
        }
    }

    void OnJump()
    {
        if (grounded)
        {
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            grounded = false;
            animator.SetBool("InTheAir", true);
        }
    }

    void OnMove(InputValue val)
    {
        movement = val.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.8f)
            {
                grounded = true;
                animator.SetBool("InTheAir", false);
            }
        }
    }

    private bool isFlipped()
    {
        Vector3 ennemyPosition = Vector3.zero;
        for (int i = 0; i < LevelManager.instance.characterPositions.Count; i++)
        {
            if (LevelManager.instance.characterPositions[i] != this.transform.position)
            {
                ennemyPosition = LevelManager.instance.characterPositions[i];
            }
        }

        bool isRight = ennemyPosition.x > this.transform.position.x;
        return isRight;
    }
    
}
