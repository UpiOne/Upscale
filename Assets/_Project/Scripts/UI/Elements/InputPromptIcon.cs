using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InputPromptIcon : MonoBehaviour
{
    [Header("Sprites for Control Schemes")]
    [SerializeField] private Sprite keyboardMouseSprite;
    [SerializeField] private Sprite xboxGamepadSprite;
    [SerializeField] private Sprite playstationGamepadSprite;

    private Image _iconImage;
    
    private void Awake()
    {
        _iconImage = GetComponent<Image>();
        InputManager.OnControlSchemeChanged += UpdateIcon;
    }
    
    private void OnDestroy()
    {
        InputManager.OnControlSchemeChanged -= UpdateIcon;
    }
    
    private void OnEnable()
    {
        if (ServiceLocator.Get<InputManager>() != null)
        {
            UpdateIcon(ServiceLocator.Get<InputManager>().CurrentScheme);
        }
    }
    
    private void UpdateIcon(ControlSchemeType scheme)
    {
        switch (scheme)
        {
            case ControlSchemeType.KeyboardMouse:
                _iconImage.sprite = keyboardMouseSprite;
                break;

            case ControlSchemeType.Gamepad_Xbox:
                _iconImage.sprite = xboxGamepadSprite;
                break;

            case ControlSchemeType.Gamepad_PS:
                _iconImage.sprite = playstationGamepadSprite;
                break;
            
            case ControlSchemeType.Gamepad_Generic:
                _iconImage.sprite = xboxGamepadSprite;
                break;
        }
    }
}