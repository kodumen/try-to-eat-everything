using UnityEngine;
using System.Collections;

public class PauseCanvas : MonoBehaviour {

    //public KeyCode pauseKey;
    public bool useRMB;

    public static PauseCanvas main;

    public bool isShown { get { return _isShown; } }

    private Canvas canvas;
    private bool _isShown;

    void Start() {

        main = PauseCanvas.main == null ? this : main;
        canvas = GetComponent<Canvas>();
        _isShown = false;
    }

	void Update () {
        // KeyCode.Escape is mapped as Back button on Android as well
        if(Input.GetKeyDown(KeyCode.Escape) || (useRMB && Input.GetMouseButtonDown(1))) {

            if(!canvas.enabled)
                Show();
            else
                Hide();
        }
	}

    /// <summary>
    /// Pauses the game and show the pause menu.
    /// </summary>
    public void Show() {

        canvas.enabled = true;
        Time.timeScale = 0;
        _isShown = true;
    }

    /// <summary>
    /// Hides the pause menu and resumes the game.
    /// </summary>
    public void Hide() {

        canvas.enabled = false;
        Time.timeScale = 1;
        _isShown = false;
    }
}
