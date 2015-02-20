using UnityEngine;
using System.Collections;

public class MenuEvents : MonoBehaviour {

    public void Play() {

        PlayerPrefs.SetInt("score", 0); // Reset the score
        Time.timeScale = 1f;
        Application.LoadLevel(1);
    }

    public void Resume() {

        PauseCanvas.main.Hide();
    }

    public void Restart() {

        Time.timeScale = 1f;
        Play();
    }

    public void Quit() {
        Application.Quit();
    }

    public void ToMainMenu() {

        Application.LoadLevel(0);
    }
}
