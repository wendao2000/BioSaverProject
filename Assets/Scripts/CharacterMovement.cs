using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour
{

    Rigidbody2D rb;

    //Movement
    [Header("Movement Stuff")]
    public float moveSpeed;
    float directionX;
    float directionY;

    //Jump
    [Header("Jumping Stuff")]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //General
    [Header("General Stuff")]
    //[HideInInspector]
    public bool paralyzed = false;
    public bool grounded = true;

    public LayerMask groundLayer;//the layer on which we can be grounded
    public float radius;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        directionX = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        directionY = CrossPlatformInputManager.GetAxisRaw("Vertical");

        #region BetterJump script
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            GroundCheck();
        }
        else if (rb.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        #endregion

        if (paralyzed)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
        }
        else
        {
            //return value of gravityScale
            rb.gravityScale = 1f;

            //horizontal direction movement a.k.a. running
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

            //vertical direction movement a.k.a. jumping
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                if (grounded)
                {
                    rb.AddForce(new Vector2(0f, jumpVelocity));
                    grounded = false;
                }
            }
        }
    }

    void GroundCheck()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, radius, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            if (directionY < 0 && collision.gameObject.GetComponent<ColliderModifier>().fallable)
            {
                collision.gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            collision.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }
}
