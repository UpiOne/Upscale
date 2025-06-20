using UnityEngine;
using UnityEngine.SceneManagement;

public class AppBootstrapper : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(transform.root.gameObject);

        ServiceLocator.Register(GetComponentInChildren<InputManager>());
        ServiceLocator.Register(GetComponentInChildren<UIManager>());
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        SceneManager.LoadScene("Main");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main")
        {
            var uiManager = ServiceLocator.Get<UIManager>();
            if (uiManager != null)
            {
                uiManager.Initialize();
            }
            else
            {
                Debug.LogError("Не удалось получить UIManager из ServiceLocator после загрузки сцены Main!");
            }
            
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}