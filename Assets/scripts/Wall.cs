// Generate a wall around the camera to prevent it from going offscreen

using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public GameObject wall;

	void Start () {
	    // Get dimensions of the camera
        // Multiplied by 2 because the result of conversion results in only half of the actual width;
        float height = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 2;
        float width = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 2;

        // Instantiate walls
        GameObject wallInstance;
        // Wall 1 - The wall on top of the camera.
        wallInstance = Instantiate(wall) as GameObject;
        wallInstance.transform.localScale = new Vector3(width, 1, 1);
        wallInstance.transform.position = new Vector2(transform.position.x, transform.position.y + (height / 2) + 0.5f);
        // Wall 2 - The wall right of the camera.
        wallInstance = Instantiate(wall) as GameObject;
        wallInstance.transform.localScale = new Vector3(1, height, 1);
        wallInstance.transform.position = new Vector2(transform.position.x + (width / 2) + 0.5f, transform.position.y);
        // Wall 3 - The wall below the camera.
        wallInstance = Instantiate(wall) as GameObject;
        wallInstance.transform.localScale = new Vector3(width, 1, 1);
        wallInstance.transform.position = new Vector2(transform.position.x, transform.position.y - (height / 2) - 0.5f);
        // Wall 4 - The wall left the camera.
        wallInstance = Instantiate(wall) as GameObject;
        wallInstance.transform.localScale = new Vector3(1, height, 1);
        wallInstance.transform.position = new Vector2(transform.position.x - (width / 2) - 0.5f, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
