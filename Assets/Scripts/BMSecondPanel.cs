using UnityEngine;

public class BMSecondPanel : MonoBehaviour
{
    BattleManager bm;
    [HideInInspector]
    public Transform layout;

    void OnEnable()
    {
        bm = FindObjectOfType<BattleManager>();
        layout = transform.GetChild(0);
    }


    public void SecondPanel(string input)
    {
        while (layout.childCount > 0)
        {
            DestroyImmediate(layout.GetChild(0).gameObject);
        }

        switch (input)
        {
            case "Attack":
                bm.SpawnEnemyButton();
                break;

            case "Magic":

                break;

            case "Item":

                break;
        }
    }
}
