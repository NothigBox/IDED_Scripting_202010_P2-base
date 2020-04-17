using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Singleton

    public static UIController instance;
    public static UIController Instance
    {
        get
        {
            if(instance == null) instance = new UIController();
            return instance;
        } 
    }

    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(Instance.gameObject);
        }

        instance = this;
    }

    #endregion

    private Player playerRef;

    [SerializeField]
    private Image[] lifeImages;

    [SerializeField]
    private Text scoreLabel;

    [SerializeField]
    private Button restartBtn;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        ToggleRestartButton(false);

        UpdateScore("0");
        UpdateLifeImages(Player.PLAYER_LIVES);
    }

    private void ToggleRestartButton(bool val)
    {
        if (restartBtn != null)
        {
            restartBtn.gameObject.SetActive(val);
        }
    }

    public void UpdateLifeImages(int lives)
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] != null && lifeImages[i].enabled)
            {
                lifeImages[i].gameObject.SetActive(lives >= i + 1);
            }
        }
    }

    public void UpdateScore(string score)
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = score;
        }
    }

    public void ActivateDeathScreen()
    {
        CancelInvoke();
        if (scoreLabel != null)
        {
            scoreLabel.text = "Game Over";
        }

        ToggleRestartButton(true);
    }
}