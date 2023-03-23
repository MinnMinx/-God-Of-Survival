
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void Exit() {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
