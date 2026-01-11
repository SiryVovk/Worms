using UnityEngine;
using UnityEngine.SceneManagement;

public class MeinMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject meinMenu;

    private const int MainSceneIndex = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(MainSceneIndex);
    }

    public void Settings()
    {
        meinMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void BackToMeinMenu()
    {
        settingMenu.SetActive(false);
        meinMenu.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
