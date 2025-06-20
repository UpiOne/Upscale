using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SettingsController : MonoBehaviour
{
    [Header("Tabs & Panels")]
    [SerializeField] private Button[] tabButtons; 
    [SerializeField] private GameObject[] contentPanels;
    
    [Header("Controls")]
    [SerializeField] private Button backButton;

    private int _currentTabIndex = -1;
    
    public Button FirstSelected => tabButtons.Length > 0 ? tabButtons[0] : backButton;

    public void Initialize(UIManager uiManager)
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i;
            tabButtons[i].onClick.AddListener(() => SelectTab(index));
        }
        
        backButton.onClick.AddListener(() => uiManager.ChangeState(new MainMenuState()));
        
        var inputActions = ServiceLocator.Get<InputManager>().InputActions.UI;
        inputActions.TabNavigateLeft.performed += OnTabNavigateLeft;
        inputActions.TabNavigateRight.performed += OnTabNavigateRight;
        
        SelectTab(0);
    }
    
    private void OnTabNavigateLeft(InputAction.CallbackContext context) => NavigateTabs(-1);
    private void OnTabNavigateRight(InputAction.CallbackContext context) => NavigateTabs(1);

    private void NavigateTabs(int direction)
    {
        int nextIndex = _currentTabIndex + direction;
        
        if (nextIndex < 0) nextIndex = tabButtons.Length - 1;
        if (nextIndex >= tabButtons.Length) nextIndex = 0;
        
        SelectTab(nextIndex);
    }
    
    private void SelectTab(int index)
    {
        if (_currentTabIndex == index) return;

        _currentTabIndex = index;
        
        for (int i = 0; i < contentPanels.Length; i++)
        {
            contentPanels[i].SetActive(i == index);
        }
        
        EventSystem.current.SetSelectedGameObject(tabButtons[index].gameObject);
    }

    private void OnDestroy()
    {
        foreach (var button in tabButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        backButton.onClick.RemoveAllListeners();
        
        if (ServiceLocator.Get<InputManager>() != null)
        {
            var inputActions = ServiceLocator.Get<InputManager>().InputActions.UI;
            inputActions.TabNavigateLeft.performed -= OnTabNavigateLeft;
            inputActions.TabNavigateRight.performed -= OnTabNavigateRight;
        }
    }
}