using UnityEngine;

public class Music : MonoBehaviour
{
	private void Start()
	{
		int numMusic = FindObjectsOfType<Music>().Length;
		if (numMusic > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
