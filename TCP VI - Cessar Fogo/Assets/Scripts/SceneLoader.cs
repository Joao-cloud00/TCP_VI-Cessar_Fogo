using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Carrega a cena pelo nome
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // garante que o jogo não fique pausado
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Carrega a cena pelo índice do Build Settings
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Recarrega a cena atual
    /// </summary>
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Sai do jogo
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
