using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
	public int score = 0;

	TextMeshProUGUI textMeshProUGUI;

	private void Start()
	{
		textMeshProUGUI = GetComponent<TextMeshProUGUI>();
		textMeshProUGUI.text = "Start!!";
	}

	public void IncreaseScore(int increaseScoreNum)
	{
		score += increaseScoreNum;
		textMeshProUGUI.text = "Score: " + score.ToString();
	}
}
