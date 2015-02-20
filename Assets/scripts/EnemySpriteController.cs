using UnityEngine;
using System.Collections;

public class EnemySpriteController : MonoBehaviour {

    public Sprite scaredSprite;
    public float requiredRemainingTime; // The time remaining of the effect before blinking effect is activated
    public float blinkRate; // Number of times to change sprite per second

    private Sprite origSprite;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private float timePerBlink;
    private bool _isBlinking;

    public bool isBlinking { get { return _isBlinking; } }

	void Start () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        origSprite = spriteRenderer.sprite;
        _isBlinking = false;
        timePerBlink = 1 / blinkRate;
	}
	
	// Update is called once per frame
	void Update () {
        if(EnemySpawner.main.spawnScared) {

            // Change to scaredSprite if not blinking;
            spriteRenderer.sprite = !_isBlinking ? scaredSprite : spriteRenderer.sprite;

            // Activate blinking effect during the remaining time of effect
            if(requiredRemainingTime >= EnemySpawner.main.remainingEffectTime) {

                _isBlinking = true;
                timer += Time.deltaTime;
                if(timer >= timePerBlink) {
                    
                    timer = 0;
                    spriteRenderer.sprite = spriteRenderer.sprite == origSprite ? scaredSprite : origSprite;
                }
            }
            else {

                _isBlinking = false;
                timer = 0;
            }
            
        }
        else {

            spriteRenderer.sprite = origSprite;
        }
	}
}
