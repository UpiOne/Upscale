using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIState _currentState;

    [Header("UI Screen Prefabs")]
    [SerializeField] private GameObject titleScreenPrefab;
    [SerializeField] private GameObject mainMenuPrefab;
    [SerializeField] private GameObject settingsScreenPrefab;
    [SerializeField] private GameObject loadSaveScreenPrefab;
    [SerializeField] private GameObject creditsScreenPrefab;
    
    // Словарь для хранения инстанцированных экранов
    private readonly Dictionary<System.Type, GameObject> _screenInstances = new Dictionary<System.Type, GameObject>();

    private Transform _canvasTransform;

    // Этот метод теперь будет вызываться из AppBootstrapper ПОСЛЕ загрузки сцены Main
    public void Initialize()
    {
        // Находим Canvas в сцене Main. Теперь это безопасно.
        _canvasTransform = FindObjectOfType<Canvas>()?.transform;

        if (_canvasTransform == null)
        {
            Debug.LogError("На сцене Main не найден Canvas!");
            return;
        }

        // Создаем экземпляры всех экранов
        InstantiateAndCacheScreen(typeof(TitleScreenController), titleScreenPrefab);
        InstantiateAndCacheScreen(typeof(MainMenuController), mainMenuPrefab);
        InstantiateAndCacheScreen(typeof(SettingsController), settingsScreenPrefab);
        InstantiateAndCacheScreen(typeof(LoadSaveController), loadSaveScreenPrefab);
        InstantiateAndCacheScreen(typeof(CreditsController), creditsScreenPrefab);
        // Начинаем игру с экрана "Title Screen"
        ChangeState(new TitleScreenState());
    }

    private void Update()
    {
        // Вызываем Update активного состояния каждый кадр
        _currentState?.Update();
    }

    public void ChangeState(UIState newState)
    {
        // Выходим из текущего состояния
        _currentState?.Exit();
        // Устанавливаем новое состояние
        _currentState = newState;
        // Передаем ссылку на себя в новое состояние
        _currentState.SetUIManager(this);
        // Входим в новое состояние
        _currentState.Enter();
    }

    // Метод для создания и кэширования префаба экрана
    private void InstantiateAndCacheScreen(System.Type controllerType, GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning($"Префаб для {controllerType.Name} не назначен в UIManager.");
            return;
        }
        
        GameObject instance = Instantiate(prefab, _canvasTransform);
        _screenInstances[controllerType] = instance;
        instance.SetActive(false);
    }
    
    // Generic-метод, чтобы состояниям было удобно получать свои экраны
    public T GetScreen<T>() where T : MonoBehaviour
    {
        if (_screenInstances.TryGetValue(typeof(T), out GameObject screenInstance))
        {
            // Дополнительная проверка на случай, если объект был уничтожен
            if (screenInstance != null)
            {
                return screenInstance.GetComponent<T>();
            }
        }
        return null;
    }
}