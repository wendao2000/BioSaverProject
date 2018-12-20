using UnityEngine;

public class ColliderModifier : MonoBehaviour
{

    //Where character actually stands on a platform
    public float groundOffset = -0.05f;

    //Trigger
    public float groundCheck = 0.5f;

    EdgeCollider2D edgeCollider2D;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Vector2[] points = edgeCollider2D.points;
        points[0] = new Vector2(-(GetComponent<SpriteRenderer>().size.x / 2), 0);
        points[1] = new Vector2(GetComponent<SpriteRenderer>().size.x / 2, 0);
        edgeCollider2D.offset = new Vector2(0f, GetComponent<SpriteRenderer>().size.y / 2 + groundOffset);
        edgeCollider2D.points = points;

        boxCollider2D.offset = new Vector2(0f, GetComponent<SpriteRenderer>().size.y / 2);
        boxCollider2D.size = new Vector2(GetComponent<SpriteRenderer>().size.x, groundCheck);
    }
}
