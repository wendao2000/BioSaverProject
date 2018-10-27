using UnityEngine;

public class ExitDoorScript : MonoBehaviour {

    private static ExitDoorScript instance;

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
