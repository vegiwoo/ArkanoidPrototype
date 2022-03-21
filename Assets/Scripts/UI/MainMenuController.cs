using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;


    private void OnEnable()
    {
        newGameButton.onClick.AddListener(OnClickNewGameButtonHandler);
        settingsButton.onClick.AddListener(OnClickSettingsButtonHandler);
        exitButton.onClick.AddListener(OnClickExitButtonHandler);
    }

    private void OnDisable()
    {
        newGameButton.onClick.RemoveListener(OnClickNewGameButtonHandler);
        settingsButton.onClick.RemoveListener(OnClickSettingsButtonHandler);
        exitButton.onClick.RemoveListener(OnClickExitButtonHandler);
    }

    private void OnClickNewGameButtonHandler()
    {
        print("newGameButton click");
    }

    private void OnClickSettingsButtonHandler()
    {
        print("settingsButton click");
    }

    private void OnClickExitButtonHandler()
    {
        print("exitButton click");
    }
}
