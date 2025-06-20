using UnityEngine;
using UnityEngine.EventSystems;

public class UIFocusManager : MonoBehaviour
{
    private EventSystem _eventSystem;
    private GameObject _lastSelected;

    void Start()
    {
        _eventSystem = EventSystem.current;
        if (_eventSystem == null)
        {
            Debug.LogError("На сцене нет EventSystem!");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (_eventSystem.currentSelectedGameObject == null)
        {
            var input = ServiceLocator.Get<InputManager>().InputActions.UI.Navigate.ReadValue<Vector2>();
            
            if (Mathf.Abs(input.x) > 0.1f || Mathf.Abs(input.y) > 0.1f)
            {
                if (_lastSelected != null && _lastSelected.activeInHierarchy)
                {
                    Debug.Log($"[UIFocusManager] Фокус потерян. Восстанавливаю на: {_lastSelected.name}");
                    _eventSystem.SetSelectedGameObject(_lastSelected);
                }
            }
        }
        else
        {
            _lastSelected = _eventSystem.currentSelectedGameObject;
        }
    }
}