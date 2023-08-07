using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float reloadTime = 1f;
	[SerializeField] GameObject explosionFXPrefab;
	[SerializeField] GameObject clearFXPrefab;
	[SerializeField] float particleDestroyTime = 1f;

	PlayerController playerController;
	MeshRenderer meshRenderer;
	BoxCollider boxCollider;
	GameManager gameManager;


	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		meshRenderer = GetComponent<MeshRenderer>();
		boxCollider = GetComponent<BoxCollider>();
		gameManager = FindObjectOfType<GameManager>();
	}

	private void Update()
	{
		// Testing();
	}

	private void OnCollisionEnter(Collision other)
	{

	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("Finish"))
			PlayClear();
		else
			PlayExplosion();
		OffPlayer();
	}

	private void PlayClear()
	{
		GameObject clearFX = Instantiate(clearFXPrefab, transform.position, Quaternion.identity);
		Destroy(clearFX, particleDestroyTime);
		gameManager.OnEndingPanel();
	}

	private void OffPlayer()
	{
		playerController.enabled = false;
		meshRenderer.enabled = false;
		boxCollider.enabled = false;
	}

	private void PlayExplosion()
	{
		//* 폭발 파티클, 폭발 음향을 인스턴스화한다.
		GameObject explosionFX = Instantiate(explosionFXPrefab, transform.position, Quaternion.identity);
		Destroy(explosionFX, particleDestroyTime);

		//* ending Panel을 띄운다.
		OffPlayer();
		gameManager.OnEndingPanel();
	}

	public void ReloadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void LoadNextScene()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
		SceneManager.LoadScene(nextSceneIndex);
	}

	/*
        testing
    */

	void Testing()
	{
		ReloadCurrentSceneByRKey();
		LoadNextSceneByNKey();
		ColliderToggleByOKey();
	}

	void ReloadCurrentSceneByRKey()
	{
		if (Input.GetKey(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	void LoadNextSceneByNKey()
	{
		if (Input.GetKey(KeyCode.N))
		{
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
			SceneManager.LoadScene(nextSceneIndex);
		}
	}

	void ColliderToggleByOKey()
	{
		if (Input.GetKey(KeyCode.O))
		{
			if (boxCollider.enabled)
			{
				Debug.Log("Player Collider off");
			}
			else
			{
				Debug.Log("Player Collider on");
			}
			boxCollider.enabled = !boxCollider.enabled;
		}
	}
}
