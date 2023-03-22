using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timer;
    public float Timer => timer;

	[SerializeField]
	private CanvasGroup pauseScreen;
	[SerializeField]
	private CanvasGroup quitConfirmDialog;
	[SerializeField]
	private TextMeshProUGUI timerTxt;

	// Start is called before the first frame update
	void Start()
    {
        timer = 0;
        EnablePauseScreen(false);
        CloseQuitConfirm();
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SetTimerTxt();
	}

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        EnablePauseScreen(false);
        CloseQuitConfirm();
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
        EnablePauseScreen(true);
	}

    public void OpenQuitConfirm()
    {
        quitConfirmDialog.alpha = 1f;
        quitConfirmDialog.interactable = true;
        quitConfirmDialog.blocksRaycasts = true;
	}

    public void CloseQuitConfirm()
    {
        quitConfirmDialog.alpha = 0;
        quitConfirmDialog.blocksRaycasts = false;
        quitConfirmDialog.interactable = false;
	}

    public void ChangeScene()
    {
        SceneManager.LoadScene("GameOver_Scene", LoadSceneMode.Single);
    }

    void EnablePauseScreen(bool active)
    {
		pauseScreen.alpha = active ? 1f : 0f;
		pauseScreen.interactable = active;
		pauseScreen.blocksRaycasts = active;
	}

    void SetTimerTxt()
    {
        int mm = (int)timer / 60;
        int ss = (int)timer % 60;
        if (timer > 3600f)
        {
            int hh = (int)timer / 3600;
            mm = (int)(timer % 3600) / 60;
            timerTxt.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hh, mm, ss);
        }
		timerTxt.text = string.Format("{0:D2}:{1:D2}", mm, ss);
	}
}
