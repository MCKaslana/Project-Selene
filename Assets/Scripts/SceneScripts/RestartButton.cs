using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _restartButton;

    private void Awake()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(() =>
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.name);
        });
    }
}
