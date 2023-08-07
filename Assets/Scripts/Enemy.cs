using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject explosionFXPrefab;
	[SerializeField] GameObject hitFXPrefab;
	// [SerializeField] Vector3 hitVFXOffset;
	[SerializeField] float enemyDestroyTime = 0f;
	[SerializeField] int increaseScore = 15;
	[SerializeField] int hitPoint = 2;
	[SerializeField] float particleDestroyTime = 1f;

	Score score;
	GameObject parentGameObject;

	private void Start()
	{
		score = FindObjectOfType<Score>();
		parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
	}

	private void OnParticleCollision(GameObject other)
	{
		Hit();
		if (hitPoint < 1)
			Explosion();
	}

	void Hit()
	{
		hitPoint -= 1;

		//* 피격 파티클, 피격 음향을 인스턴스화하고, 몇 초 뒤에 파괴한다.
		GameObject hitFX = Instantiate(hitFXPrefab, transform.position, Quaternion.identity);
		if (object.ReferenceEquals(parentGameObject, null) == false)
		{
			hitFX.transform.parent = parentGameObject.transform;
		}
		Destroy(hitFX, particleDestroyTime);
	}

	/// <summary>폭발 파티클을 실행하고, 점수를 올리고, 오브젝트를 파괴한다.</summary>
	private void Explosion()
	{
		Debug.Log($"{this.name} destroyed!");

		//* 폭발 파티클, 폭발 음향을 인스턴스화하고, 몇 초 뒤에 파괴한다.
		GameObject explosionFX = Instantiate(explosionFXPrefab, transform.position, Quaternion.identity);
		if (object.ReferenceEquals(parentGameObject, null) == false)
		{
			explosionFX.transform.parent = parentGameObject.transform;
		}
		Destroy(explosionFX, particleDestroyTime);

		//* 점수를 올린다.
		if (object.ReferenceEquals(score, null) == false)
			score.IncreaseScore(increaseScore);

		//* 오브젝트를 파괴한다.
		Destroy(gameObject, enemyDestroyTime);
	}
}
