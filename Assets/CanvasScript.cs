using UnityEngine;

public class CanvasScript : MonoBehaviour {

    private static CanvasScript instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
