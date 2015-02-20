// Handles enemy ai movement

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float moveSpeed;
    public float playerRange;
    public GameObject player;
    public bool isScared;   // If true, move away from player.

	void Start () {

	}

	void Update () {

        // Move forward
        transform.Translate(-Vector2.up * moveSpeed * Time.deltaTime);

        // Check if player within range
        Vector2 playerPosition = player.transform.position;
        float playerDistance = Vector2.Distance(transform.position, playerPosition);
        if(playerRange >= playerDistance) {

            float tangent = Mathf.Atan2(transform.position.y - playerPosition.y, transform.position.x - playerPosition.x);
            float angle = Mathf.Rad2Deg * tangent - 90 + (isScared ? 180 : 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Debug
        //Debug.Log(playerDistance);
	}
}
