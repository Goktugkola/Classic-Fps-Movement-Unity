using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerInput playerInput;
    public static bool GameIsPaused = false;
    public GameObject text;
    public GameObject OptionsMenu;
    [Header("Buttons")]
    public GameObject resumeButton;
    public GameObject menuButton;
    public GameObject quitButton;
    public GameObject restartButton;
    public GameObject optionsButton;
    private void Start()
    {
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        restartButton.SetActive(false);
        optionsButton.SetActive(false);
        text.SetActive(false);

    }
    void Update()
    {
        if (playerInput.actions["Pause"].triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Resume()
    {
        playerInput.actions["Inventory"].Enable();
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        restartButton.SetActive(false);
        optionsButton.SetActive(false);
        text.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Pause()
    {
        playerInput.actions["Inventory"].Disable();
        resumeButton.SetActive(true);
        menuButton.SetActive(true);
        quitButton.SetActive(true);
        restartButton.SetActive(true);
        optionsButton.SetActive(true);
        text.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void activateOptions()
    {
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        restartButton.SetActive(false);
        optionsButton.SetActive(false);
        text.SetActive(false);
        OptionsMenu.SetActive(true);
    }
        public void deactivateOptions()
    {
        resumeButton.SetActive(true);
        menuButton.SetActive(true);
        quitButton.SetActive(true);
        restartButton.SetActive(true);
        optionsButton.SetActive(true);
        text.SetActive(true);
        OptionsMenu.SetActive(false);
    }
    public void LoadMenu(string Scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Scene); // Replace "Menu" with your menu scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}