using UnityEngine.InputSystem;

public class LoadSaveState : UIState
{
    private LoadSaveController _screen;

    public override void Enter()
    {
        _screen = UIManager.GetScreen<LoadSaveController>();
        if (_screen == null) return;

        _screen.gameObject.SetActive(true);
        _screen.Initialize(UIManager);
        ServiceLocator.Get<InputManager>().InputActions.UI.Cancel.performed += OnCancelPressed;
    }

    public override void Exit()
    {
        if (ServiceLocator.Get<InputManager>() != null)
            ServiceLocator.Get<InputManager>().InputActions.UI.Cancel.performed -= OnCancelPressed;
        
        if (_screen != null)
            _screen.gameObject.SetActive(false);
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        UIManager.ChangeState(new MainMenuState());
    }
}