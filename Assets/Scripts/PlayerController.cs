using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject[] lasers;
	[SerializeField] AudioClip shootSFX;
	[SerializeField] float moveSpeed = 30f;
	[SerializeField] float xRange = 15f;
	[SerializeField] float yRange = 10f;
	[SerializeField] float posPitchFactor = -2f;
	[SerializeField] float controlPitchFactor = -10f;
	[SerializeField] float posYawFactor = 2f;
	[SerializeField] float rotationRollFactor = -20f;

	AudioSource audioSource;
	GameObject parentObject;

	float xInput = 0f, yInput = 0f;
	Vector3 nextPos;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		parentObject = GameObject.FindWithTag("SpawnAtRuntime");
	}

	private void Update()
	{
		xInput = Input.GetAxis("Horizontal");
		yInput = Input.GetAxis("Vertical");

		Move();
		Rotate();

		if (Input.GetButton("Fire1"))
		{
			PlayShootSFX();
			SetActiveParticle(true);
		}
		else
		{
			SetActiveParticle(false);
		}
	}

	void SetActiveParticle(bool isActive)
	{
		foreach (GameObject laser in lasers)
		{
			var emissionModule = laser.GetComponent<ParticleSystem>().emission;
			emissionModule.enabled = isActive;
		}
	}

	void PlayShootSFX()
	{
		audioSource.Stop();
		audioSource.PlayOneShot(shootSFX);
	}

	private void Rotate()
	{
		//* Rotate
		float pitch = transform.localPosition.y * posPitchFactor + yInput * controlPitchFactor;
		float yaw = transform.localPosition.x * posYawFactor;
		float roll = xInput * rotationRollFactor;
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void Move()
	{
		//* Move Left, Right, Up, Down
		nextPos = transform.localPosition;

		float xOffset = xInput * Time.deltaTime * moveSpeed;
		float xRawPos = transform.localPosition.x + xOffset;
		nextPos.x = Mathf.Clamp(xRawPos, -xRange, xRange);
		float yOffset = yInput * Time.deltaTime * moveSpeed;
		float yRawPos = transform.localPosition.y + yOffset;
		nextPos.y = Mathf.Clamp(yRawPos, -yRange, yRange);

		transform.localPosition = nextPos;
	}
}
