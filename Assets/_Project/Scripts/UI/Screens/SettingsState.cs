using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SettingsState : UIState
{
    private SettingsController _screen;

    public override void Enter()
    {
        _screen = UIManager.GetScreen<SettingsController>();
        if (_screen == null)
        {
            Debug.LogError("Экран Settings не найден!");
            return;
        }

        _screen.gameObject.SetActive(true);
        _screen.Initialize(UIManager);
        
        EventSystem.current.SetSelectedGameObject(_screen.FirstSelected.gameObject);
        
        ServiceLocator.Get<InputManager>().InputActions.UI.Cancel.performed += OnCancelPressed;
        
        Debug.Log("Вошли в состояние Settings.");
    }

    public override void Exit()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (ServiceLocator.Get<InputManager>() != null)
        {
            ServiceLocator.Get<InputManager>().InputActions.UI.Cancel.performed -= OnCancelPressed;
        }

        if (_screen != null)
        {
            _screen.gameObject.SetActive(false);
        }
        Debug.Log("Вышли из состояния Settings.");
    }
    
    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Нажата кнопка Cancel. Возвращаемся в главное меню.");
        UIManager.ChangeState(new MainMenuState());
    }
}