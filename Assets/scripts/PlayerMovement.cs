using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;

	void Start () {
	
	}
	
	void Update () {

        if(Input.touchSupported) {

            // Allow only single finger on screen
            if(Input.touchCount == 1) {

                Touch touch = Input.GetTouch(0);
                Move(touch.position);
            }
        }
        // Desktop  - Left mouse button
        else if(Input.GetMouseButton(0)) {

            Move(Input.mousePosition);           
        }
	}

    private void Move(Vector2 pointer) {

        pointer = Camera.main.ScreenToWorldPoint(pointer);

        if(!Physics2D.Raycast(pointer, Vector3.forward, Mathf.Infinity, 1 << gameObject.layer)) {

            // MOVE FORWARD
            transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);

            // ROTATE
            float tangent = Mathf.Atan2(transform.position.y - pointer.y, transform.position.x - pointer.x);
            float angle = Mathf.Rad2Deg * tangent - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        GameObject other = collision.gameObject;

        if(other.tag.Equals("Enemy")) {

            if(EnemySpawner.main.spawnScared) {

                Destroy(collision.gameObject);
                return;
            }

            //Debug.Log("Enemy entered");
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovement.enabled = false;
            enabled = false;
            // Stop both objects
        }
    }
}
