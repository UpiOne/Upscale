using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    public Button FirstSelected
    {
        get
        {
            if (newGameButton == null)
            {
                Debug.LogError("[MainMenuController] -> newGameButton не назначен в инспекторе!");
            }
            return newGameButton;
        }
    }
    public void Initialize(UIManager uiManager)
    {
        newGameButton.onClick.AddListener(() => Debug.Log("New Game Clicked!"));
        
        loadButton.onClick.AddListener(() => uiManager.ChangeState(new LoadSaveState()));
        settingsButton.onClick.AddListener(() => uiManager.ChangeState(new SettingsState()));
        creditsButton.onClick.AddListener(() => uiManager.ChangeState(new CreditsState()));
        
        quitButton.onClick.AddListener(() => {
            Debug.Log("Quit Game Clicked!");
            Application.Quit();
        });
    }
    
    private void OnDestroy()
    {
        newGameButton.onClick.RemoveAllListeners();
        loadButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }
}