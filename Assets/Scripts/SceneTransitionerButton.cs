using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionerButton : MonoBehaviour
{
    public enum TransitionType
    {
        SceneByIndex,
        SceneByName,
        MainMenu,
        Leaderboard,
        Quit
    }

    [Header("Transition Settings")]
    public TransitionType transitionType = TransitionType.SceneByIndex;
    public int sceneIndex;
    public string sceneName;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (SceneTransitioner.Instance == null)
        {
            Debug.LogError("No SceneTransitioner found in scene!");
            return;
        }

        switch (transitionType)
        {
            case TransitionType.SceneByIndex:
                SceneTransitioner.Instance.TransitionToScene(sceneIndex);
                break;
            case TransitionType.SceneByName:
                SceneTransitioner.Instance.TransitionToSceneByName(sceneName);
                break;
            case TransitionType.MainMenu:
                SceneTransitioner.Instance.TransitionToMainMenu();
                break;
            case TransitionType.Leaderboard:
                SceneTransitioner.Instance.TransitionToLeaderBoard();
                break;
            case TransitionType.Quit:
                SceneTransitioner.Instance.QuitGame();
                break;
        }
    }
}
