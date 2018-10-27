using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public enum Direction
{
    Left, Right
};

public enum GroundLevel
{
    Zero, One, Two, Three, Four
};

public class ExitScene : MonoBehaviour
{
    GameManager gm;

    public Direction targetDirection;
    public GroundLevel groundLevel;

    public bool collide = false;
    public new bool enabled = false;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (enabled)
        {
            if (collide && CrossPlatformInputManager.GetButtonDown("Interact"))
            {
                gm.Save();

                switch (groundLevel)
                {
                    case GroundLevel.Zero:
                        {
                            //gm.startPos.y = -3f;
                            PlayerPrefs.SetFloat("startPos.Y", -3f);
                            break;
                        }
                    case GroundLevel.One:
                        {
                            //gm.startPos.y = -1.5f;
                            PlayerPrefs.SetFloat("startPos.Y", -1.5f);
                            break;
                        }
                    case GroundLevel.Two:
                        {
                            //gm.startPos.y = 0f;
                            PlayerPrefs.SetFloat("startPos.Y", 0f);
                            break;
                        }
                    case GroundLevel.Three:
                        {
                            //gm.startPos.y = 1.5f;
                            PlayerPrefs.SetFloat("startPos.Y", 1.5f);
                            break;
                        }
                    case GroundLevel.Four:
                        {
                            //gm.startPos.y = 3f;
                            PlayerPrefs.SetFloat("startPos.Y", 3f);
                            break;
                        }
                }

                switch (targetDirection)
                {
                    case Direction.Left:
                        {
                            //gm.startPos.x = 8f;
                            PlayerPrefs.SetFloat("startPos.X", 8f);
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                            break;
                        }
                    case Direction.Right:
                        {
                            //gm.startPos.x = -8f;
                            PlayerPrefs.SetFloat("startPos.X", -8f);
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            break;
                        }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = true;
            //gm.UpdateInteraction("ExitScene");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collide = false;
            //gm.UpdateInteraction("Normal");
        }
    }
}
