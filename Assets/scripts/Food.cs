using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

    public enum FoodType {
        Normal,
        Bonus
    };

    public FoodType foodType;
    public float bonusEffectTimeLimit;
    public int score;
	
	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider2D) {

        if(GameOverTrigger.isGameOver)
            return;

        if(collider2D.tag.Equals("Player")) {

            if(foodType == FoodType.Bonus) {

                EnemySpawner.main.StartSpawnScared(bonusEffectTimeLimit);
            }

            //Debug.Log("EAT");
            FoodSpawner foodSpawner = GetComponentInParent<FoodSpawner>();
            foodSpawner.SpawnFood(transform.position);
            Destroy(gameObject);
        }

        // Add score
        ScoreDisplay.main.AddToScore(score);
    }
}
