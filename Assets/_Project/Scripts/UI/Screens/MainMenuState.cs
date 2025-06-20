using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuState : UIState
{
    private MainMenuController _screen;

    public override void Enter()
    {
        Debug.Log("[MainMenuState] -> Enter() вызван.");

        _screen = UIManager.GetScreen<MainMenuController>();
        if (_screen == null)
        {
            Debug.LogError("[MainMenuState] -> Не удалось найти MainMenuController! Проверьте, добавлен ли префаб в UIManager.");
            return;
        }

        _screen.gameObject.SetActive(true);
        _screen.Initialize(UIManager);
        
        GameObject firstButton = _screen.FirstSelected?.gameObject;
        if (firstButton != null)
        {
            Debug.Log($"[MainMenuState] -> Пытаюсь установить выбранным объектом: {firstButton.name}");
            EventSystem.current.SetSelectedGameObject(firstButton);
            
            if (EventSystem.current.currentSelectedGameObject == firstButton)
            {
                Debug.Log($"[MainMenuState] -> УСПЕХ! Объект {firstButton.name} теперь выбран.");
            }
            else
            {
                Debug.LogError("[MainMenuState] -> ОШИБКА! Не удалось установить выбранный объект. Проверьте, активен и интерактивен ли он.");
            }
        }
        else
        {
            Debug.LogError("[MainMenuState] -> FirstSelected вернул null! Проверьте, назначена ли первая кнопка в инспекторе MainMenuController.");
        }
    }

    public override void Exit()
    {
        Debug.Log("[MainMenuState] -> Exit() вызван. Сбрасываю выбранный объект.");
        EventSystem.current.SetSelectedGameObject(null);
        
        if (_screen != null)
        {
            _screen.gameObject.SetActive(false);
        }
    }
}