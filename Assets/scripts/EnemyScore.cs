using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {

    public int score;
    public int bonusScore;  // Score when blinking

    private EnemySpriteController spriteController;

    void Start() {

        spriteController = GetComponent<EnemySpriteController>();
    }

    void OnCollisionEnter2D(Collision2D collision2D) {

        if(EnemySpawner.main.spawnScared && collision2D.gameObject.tag.Equals("Player")) {

            int score;
            if(spriteController != null && spriteController.isBlinking) {
                score = bonusScore;
            }
            else
                score = this.score;
            
            ScoreDisplay.main.AddToScore(score);
        }
    }
}
