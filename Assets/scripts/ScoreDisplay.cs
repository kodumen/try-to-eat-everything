using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    public static ScoreDisplay main { get; set; }
    public int score;
    public bool displayHiScore;

    private Text text;

	void Start () {
        main = main != null ? main : this;
        text = GetComponent<Text>();

        score = displayHiScore ? PlayerPrefs.GetInt("hiScore") : PlayerPrefs.GetInt("score");
        text.text = score.ToString();
	}

    public void AddToScore(int score) {

        this.score += score;
        text.text = this.score.ToString();
    }
}
