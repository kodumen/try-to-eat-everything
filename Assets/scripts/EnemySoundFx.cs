using UnityEngine;
using System.Collections;

public class EnemySoundFx : MonoBehaviour {

    public int normalSoundFxIndex;
    public int scaredSoundFxIndex;

    private EnemyMovement enemyMovement;

    void Start() {

        enemyMovement = GetComponent<EnemyMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if(!collision.gameObject.tag.Equals("Player"))
            return;

        int soundClip;
        if(enemyMovement.isScared)
            soundClip = scaredSoundFxIndex;
        else
            soundClip = normalSoundFxIndex;

        SoundFxPlayer.main.PlaySoundEffect(soundClip);
    }
}
