using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public float spawnAreaScale;
    public float spawnInterval;// In seconds
    //public float spawnStart;
    public int maxChildren;
    public int startChildren;
    public int enemyIncrement;
    public int enemyIncrementInterval; // In each enemy spawns
    public GameObject enemy;
    public bool spawnScared;

    // For spawn area
    private Vector2 upperLeft;
    private Vector2 lowerRight;

    // Spawning
    private float timer;
    private float scaredTimer;    // For scared enemy spawning
    private float scaredTimeLimit;
    private int spawnsSinceInrement;
    private float halfHypotenuse;
    private GameObject player;
    private int enemyToSpawnCount;

    // For check if offscreen
    private Plane[] camPlane;

    // Accessors
    public static EnemySpawner main { get; set; }
    public float remainingEffectTime { get { return scaredTimeLimit - scaredTimer; } }

	void Start () {
        // For spawn area
        float halfWidth = Screen.width / 2;
        float halfWidthScaled = halfWidth * spawnAreaScale;
        float halfHeight = Screen.height / 2;
        float halfHeightScaled = halfHeight * spawnAreaScale;

        upperLeft = new Vector2(halfWidth - halfWidthScaled, halfHeight + halfHeightScaled);
        lowerRight = new Vector2(halfWidth + halfWidthScaled, halfHeight - halfHeightScaled);
        // Convert to world space so we can use it to plot the position of those to be spawned
        upperLeft = Camera.main.ScreenToWorldPoint(upperLeft);
        lowerRight = Camera.main.ScreenToWorldPoint(lowerRight);

        // For enemy positioning
        halfHypotenuse = Mathf.Sqrt(Mathf.Pow(halfWidth, 2) + Mathf.Pow(halfHeight, 2));
        player = GameObject.FindGameObjectWithTag("Player");
        spawnsSinceInrement = 0;
        enemyToSpawnCount = startChildren;
        camPlane = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        main = main != null ? main : this;

        // Start level 0!
        SpawnEnemy(startChildren);
	}
	
    void Update () {

        scaredTimer = spawnScared ? scaredTimer + Time.deltaTime : 0;
        if(spawnScared && scaredTimer >= scaredTimeLimit) {

            StopSpawnScared();
        }

        timer += Time.deltaTime;
        if(timer >= spawnInterval) {

            if(spawnsSinceInrement >= enemyIncrementInterval) {

                enemyToSpawnCount += enemyIncrement;
                spawnsSinceInrement = 0;
            }
            RemoveAllOffscreen();
            SpawnEnemy(enemyToSpawnCount);
            timer = 0;
        }

        // Debug
        //Vector2 upperRight = new Vector2(lowerRight.x, upperLeft.y);
        //Vector2 lowerLeft = new Vector2(upperLeft.x, lowerRight.y);
        //Debug.DrawLine(upperLeft, upperRight);
        //Debug.DrawLine(upperRight, lowerRight);
        //Debug.DrawLine(lowerRight, lowerLeft);
        //Debug.DrawLine(lowerLeft, upperLeft);
	}

    private void SpawnEnemy(int count) {

        int remainingSpawnables = maxChildren - transform.childCount;
        if(remainingSpawnables <= 0)
            return;

        count = remainingSpawnables < count ? remainingSpawnables : count;

        for(int i = 0; i < count; i++) {

            float angleFromCenter = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float posX = Mathf.Clamp(Mathf.Cos(angleFromCenter) * halfHypotenuse, upperLeft.x, lowerRight.x);
            float posY = Mathf.Clamp(Mathf.Sin(angleFromCenter) * halfHypotenuse, upperLeft.y, lowerRight.y);
            Vector2 pos = new Vector2(posX, posY);  // Position where enemy will spawn

            float targetX = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), 0)).x;
            float targetY = Camera.main.ScreenToWorldPoint(new Vector2(0, Random.Range(0, Screen.height))).y;
            float tangent = Mathf.Atan2(posY - targetY, posX - targetX);
            float angle = Mathf.Rad2Deg * tangent - 90;
            Quaternion targetAngle = Quaternion.Euler(new Vector3(0, 0, angle)); // Direction where the spawned enemy is going

            GameObject spawnedEnemy = Instantiate(enemy, pos, targetAngle) as GameObject;
            EnemyMovement enemyMovement = spawnedEnemy.GetComponent<EnemyMovement>();
            enemyMovement.player = player;
            enemyMovement.isScared = spawnScared;
            spawnedEnemy.transform.parent = gameObject.transform;
        }

        spawnsSinceInrement++;
    }

    public void RemoveAllOffscreen() {

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach(Collider2D childCollider in colliders) {

            if(!GeometryUtility.TestPlanesAABB(camPlane, childCollider.bounds)) {

                Destroy(childCollider.gameObject);
                //Debug.Log("Enemy deleted.");
            }
        }
    }

    public void StartSpawnScared(float time) {

        // Set all children as scared
        EnemyMovement[] enemies = GetComponentsInChildren<EnemyMovement>();

        foreach(EnemyMovement enemy in enemies) {

            enemy.isScared = true;
        }
        // Spawn scared enemies until time ends
        spawnScared = true;
        scaredTimeLimit = time;
        scaredTimer = 0;
    }

    public void StopSpawnScared() {

        // Set all children as not scared
        EnemyMovement[] enemies = GetComponentsInChildren<EnemyMovement>();

        foreach(EnemyMovement enemy in enemies) {

            enemy.isScared = false;
        }

        spawnScared = false;
        scaredTimeLimit = 0;
        scaredTimer = 0;
    }
}
