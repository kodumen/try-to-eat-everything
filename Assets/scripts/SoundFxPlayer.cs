// Plays all sound effects

using UnityEngine;
using System.Collections;

public class SoundFxPlayer : MonoBehaviour {

    public AudioClip[] audioClips;
    public static SoundFxPlayer main;

    void Start() {

        main = main == null ? this : main;
    }

    public void PlaySoundEffect(int index) {

        AudioSource.PlayClipAtPoint(audioClips[index], Vector2.zero);
    }
}
