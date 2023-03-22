using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    private float Score;
    private void Start()
    {
    }

    private void Update()
    {
        SetScore();
    }

    void OnEnable()
    {
        Score = PlayerPrefs.GetFloat("score");
    }

    public void SetScore()
    {
        
        text.text = "Score: " + Score;
    }

    public void OnButtonClick()
    {
        Debug.Log("chuyen scene");
        SceneManager.LoadScene("Menu");
    }
}
