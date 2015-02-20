// Spawn normal food.
// After eating a sufficient amount of normal food,
// spawn bonus food.
// Once eaten, increase amount of normal food to be eaten

using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour {

    public float spawnAreaScale;    // 1 is same as screen size, 0.5 is half, 2 is double, you get the idea
    public int normalFoodIncrement;
    public int normalFoodStart;
    public float minFoodDistance;
    public GameObject normalFood;
    public GameObject bonusFood;

    private Vector2 upperLeft;  // The upper left most point in the spawn area
    private Vector2 lowerRight; // Lower right most point

    // Accessors
    public int normalFoodEaten { get; set; }
    public int normalFoodRequired { get; set; }

	void Start () {
        
        // Spawn area calculations
        float halfWidth = Screen.width / 2;
        float halfWidthScaled = halfWidth * spawnAreaScale;
        float halfHeight = Screen.height / 2;
        float halfHeightScaled = halfHeight * spawnAreaScale;

        upperLeft = new Vector2(halfWidth - halfWidthScaled, halfHeight + halfHeightScaled);
        lowerRight = new Vector2(halfWidth + halfWidthScaled, halfHeight - halfHeightScaled);
        // Convert to world space so we can use it to plot the position of those to be spawned
        upperLeft = Camera.main.ScreenToWorldPoint(upperLeft);
        lowerRight = Camera.main.ScreenToWorldPoint(lowerRight);


        // Spawn one food now then reset normalFoodEaten
        normalFoodEaten = -1;   // Starts with -1 so that when SpawnFood() is called, it would go to zero
        normalFoodRequired = normalFoodStart;
        SpawnFood(Vector2.zero);
	}
	
	void Update () {

        // Debug
        //Vector2 upperRight = new Vector2(lowerRight.x, upperLeft.y);
        //Vector2 lowerLeft = new Vector2(upperLeft.x, lowerRight.y);
        //Debug.DrawLine(upperLeft, upperRight);
        //Debug.DrawLine(upperRight, lowerRight);
        //Debug.DrawLine(lowerRight, lowerLeft);
        //Debug.DrawLine(lowerLeft, upperLeft);
	}

    /// <summary>
    /// Should be called only when a food is eaten.
    /// </summary>
    public void SpawnFood(Vector2 lastFoodPosition) {

        normalFoodEaten++;

        GameObject food;
        if(normalFoodEaten >= normalFoodRequired) {

            // Spawn bonus food
            food = bonusFood;
            normalFoodEaten = 0;
            normalFoodRequired += normalFoodIncrement;
        }
        else {
            food = normalFood;
        }

        Vector2 spawnPosition;
        do {
            float posX = Random.Range(upperLeft.x, lowerRight.x);
            float posY = Random.Range(upperLeft.y, lowerRight.y);
            spawnPosition = new Vector2(posX, posY);
        } while(Vector2.Distance(spawnPosition, lastFoodPosition) <= minFoodDistance);
        GameObject spawnedFood = Instantiate(food, spawnPosition, Quaternion.identity) as GameObject;
        spawnedFood.transform.parent = gameObject.transform;
    }
}
