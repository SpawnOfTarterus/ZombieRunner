using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Player player;

    public void ToggleGameOverMenu()
    {
        gameOverMenu.SetActive(!gameOverMenu.activeSelf);
        player.TogglePlayerControls();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
