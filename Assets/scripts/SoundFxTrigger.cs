using UnityEngine;
using System.Collections;

public class SoundFxTrigger : MonoBehaviour {

    public int soundFxIndex;    // Index of the audio clip to be played by the sound fx player

    void OnTriggerEnter2D(Collider2D collider2d) {

        SoundFxPlayer.main.PlaySoundEffect(this.soundFxIndex);
    }
}
