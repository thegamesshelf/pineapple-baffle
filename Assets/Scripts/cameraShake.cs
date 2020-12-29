using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{

	Vector3 cameraInitialPosition;
	public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
	public Camera mainCamera;
	public bool playerCanMove = true;

	public void ShakeIt()
	{
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating("StartCameraShaking", 0f, 0.005f);
		Invoke("StopCameraShaking", shakeTime);
	}

	void StartCameraShaking()
	{
		playerCanMove = false;
		float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		//float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		//print(cameraIntermadiatePosition.x);
		//cameraIntermadiatePosition.y += cameraShakingOffsetY;
		mainCamera.transform.position = cameraIntermadiatePosition;
	}

	void StopCameraShaking()
	{
		CancelInvoke("StartCameraShaking");
		// remove below to prevent camera glitch
		mainCamera.transform.position = cameraInitialPosition;
		playerCanMove = true;
	}

}
