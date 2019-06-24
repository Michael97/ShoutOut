//Michael Thomas

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighscoresController : MonoBehaviour {

    //public TextMesh Highscore;
    public TextMeshPro CurrentScore1;
    public TextMeshPro CurrentScore2;
    public TextMeshPro CurrentScore3;
    public TextMeshPro CurrentScore4;
    public int CurrentHighScore;
    public int CurrentScoreInt;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public int i;

    private void Start()
    {
        //Highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        CurrentScore1.text = "0";
        CurrentScore2.text = "0";
        CurrentScore3.text = "0";
        CurrentScore4.text = "0";
        CurrentScoreInt = 0;
        CurrentHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    //Called by scripts dying that requires score to be added to the player
    //This then updates the totalscore and the score on the screen
    public void AddScore(int _score)
    {
        CurrentScoreInt += _score;
        CurrentScore1.text = CurrentScoreInt.ToString();
        CurrentScore2.text = CurrentScoreInt.ToString();
        CurrentScore3.text = CurrentScoreInt.ToString();
        CurrentScore4.text = CurrentScoreInt.ToString();

        //If we are larger than the current highscore then update it
        if (CurrentScoreInt > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScoreInt);
            i++;

            if (i == 1)
            {
                int index = Random.Range(0, audioClips.Length);
                AudioClip playClip = audioClips[index];
                audioSource.clip = playClip;
                audioSource.Play();
            }
        }
    }

    //Used to reset all of the highscores
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
