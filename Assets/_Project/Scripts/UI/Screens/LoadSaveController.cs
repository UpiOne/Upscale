using UnityEngine;
using UnityEngine.UI;

public class LoadSaveController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    
    public void Initialize(UIManager uiManager)
    {
        backButton.onClick.AddListener(() => uiManager.ChangeState(new MainMenuState()));
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }
}