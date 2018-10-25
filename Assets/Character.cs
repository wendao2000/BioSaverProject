using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Character : MonoBehaviour
{

    Rigidbody2D rb;

    //Movement
    public float moveSpeed;
    float direction;

    //Jump
    public float jumpVelocity;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    //general
    [HideInInspector]
    public bool paralyzed = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void FixedUpdate()
    {
        if (paralyzed)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            //horizontal direction movement a.k.a. running
            direction = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

            //vertical direction movement a.k.a. jumping
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                rb.AddForce(new Vector2(0f, jumpVelocity));
            }
        }
    }
}
