using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance;

    [Header(" Fade Settings ")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private bool isFading = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToScene(int sceneIndex) => StartCoroutine(DoSceneTransition(sceneIndex));
    public void TransitionToSceneByName(string sceneName) => StartCoroutine(DoSceneTransition(sceneName));
    public void TransitionToLeaderBoard() => StartCoroutine(DoSceneTransition("Leaderboard"));
    public void TransitionToMainMenu() => StartCoroutine(DoSceneTransition("MainMenu"));
    public void QuitGame() => Application.Quit();


    private IEnumerator DoSceneTransition(int sceneIndex)
    {
        yield return Fade(1f);
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return Fade(0f);
    }

    private IEnumerator DoSceneTransition(string sceneName)
    {
        yield return Fade(1f);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return Fade(0f);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        if (isFading) yield break;

        isFading = true;
        float startAlpha = fadeCanvasGroup.alpha;
        float timer = 0f;

        fadeCanvasGroup.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        isFading = false;

        fadeCanvasGroup.blocksRaycasts = targetAlpha > 0.01f;
    }
}
