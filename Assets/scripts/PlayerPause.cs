using UnityEngine;
using System.Collections;

public class PlayerPause : MonoBehaviour {

    private PlayerMovement playerMovement;

	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMovement.enabled = !PauseCanvas.main.isShown && !GameOverTrigger.isGameOver;
	}
}
