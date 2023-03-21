using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text;
    private void Start()
    {
    }

    private void Update()
    {
    }

    public void SetScore(float score)
    {
        
        text.text = "Score: " + score;
    }

    public void OnButtonClick()
    {
        Debug.Log("chuyen scene");
        SceneManager.LoadScene("Menu");
    }
}
