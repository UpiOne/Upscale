using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenState : UIState
{
    private TitleScreenController _screen;
    private InputAction _anyKeyAction;
    
    private bool _isTransitioning = false;

    public override void Enter()
    {
        _isTransitioning = false;
        
        _screen = UIManager.GetScreen<TitleScreenController>();
        if (_screen == null)
        {
            Debug.LogError("Экран TitleScreen не найден! Проверьте, добавлен ли префаб в UIManager.");
            return;
        }

        _screen.gameObject.SetActive(true);

        _anyKeyAction = new InputAction(type: InputActionType.Button, binding: "*/<button>");
        _anyKeyAction.performed += OnAnyKeyPressed;
        _anyKeyAction.Enable();
        
        Debug.Log("Вошли в состояние Title Screen. Нажмите любую кнопку.");
    }
    
    public override void Update()
    {
        if (_isTransitioning)
        {
            _isTransitioning = false;
            UIManager.ChangeState(new MainMenuState());
        }
    }

    private void OnAnyKeyPressed(InputAction.CallbackContext context)
    {
        if (_isTransitioning) return;

        Debug.Log($"Нажата кнопка: {context.control.path}. Готовимся к переходу в главное меню...");
        
        _isTransitioning = true;
    }

    public override void Exit()
    {
        if (_screen != null)
        {
            _screen.gameObject.SetActive(false);
        }

        if (_anyKeyAction != null)
        {
            _anyKeyAction.performed -= OnAnyKeyPressed;
            _anyKeyAction.Disable();
            _anyKeyAction.Dispose();
            _anyKeyAction = null;
        }
        
        Debug.Log("Вышли из состояния Title Screen.");
    }
}