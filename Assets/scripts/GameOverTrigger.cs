using UnityEngine;
using System.Collections;

public class GameOverTrigger : MonoBehaviour {

	public static bool isGameOver;
    public string targetTag;
    public float loadLevelDelay;

    private float timer;

	void Start () {
        isGameOver = false;
	}

    void Update() {

        if(isGameOver) {
            
            timer += Time.deltaTime;
            if(timer >= loadLevelDelay) {

                Application.LoadLevel(2);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision2d) {

        if(collision2d.gameObject.tag.Equals(targetTag) && !EnemySpawner.main.spawnScared) {

            if(!isGameOver) {

                // Save score for display
                PlayerPrefs.SetInt("score", ScoreDisplay.main.score);
                // Save if higher than hi-score
                int hiScore = PlayerPrefs.GetInt("hiScore");
                if(ScoreDisplay.main.score > hiScore) {
                    PlayerPrefs.SetInt("hiScore", ScoreDisplay.main.score);
                }

                //Debug.Log("Game over");
                isGameOver = true;
            }
        }

        //Debug.Log("Score: " + PlayerPrefs.GetInt("score").ToString());
        //Debug.Log("HiScore: " + PlayerPrefs.GetInt("hiScore").ToString());
    }
}
