using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameManager gm;
    Transform ch;

    Vector3 velo = Vector3.zero;

    Vector3 targetPos;

    public Vector2 clampMin;
    public Vector2 clampMax;
    

    public float smoothTime = 0.2f;

    void Awake()
    {
        gm = GameManager.GetInstance();
        ch = GameObject.Find("Character").transform;
        targetPos = ch.position;
    }

    void FixedUpdate () {
        targetPos.x = Mathf.Clamp(ch.position.x, clampMin.x, clampMax.x);

        targetPos.y = Mathf.Clamp(ch.position.y, clampMin.y, clampMax.y);

        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velo, smoothTime);
	}
}
