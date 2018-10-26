using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterInteraction : MonoBehaviour {

    GameManager gm;
    CharacterMovement ch;
    Inventory iv;

    public int currentMoney = 0;

    void Awake () {
        gm = FindObjectOfType<GameManager>();
        ch = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Inventory")) {
            gm.Inventory();
            ch.paralyzed = !ch.paralyzed;
        }
	}
}
