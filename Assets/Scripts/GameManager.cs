using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject endingPanel;
	[SerializeField] TextMeshProUGUI scoreTxt;

	CollisionHandler collisionHandler;
	Score score;

	private void Start()
	{
		collisionHandler = FindObjectOfType<CollisionHandler>();
		score = FindObjectOfType<Score>();
	}

	public void OnEndingPanel()
	{
		endingPanel.SetActive(true);
		ShowScore();
	}

	public void ShowScore()
	{
		scoreTxt.text = "Your Score : " + (score.score).ToString();
	}

	public void ClickReplayBtn()
	{
		collisionHandler.ReloadCurrentScene();
	}

	public void ClickQuitBtn()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
	}
}
