//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShakeSimpleScript : MonoBehaviour {

	private bool _isRunning = false;
	public CinemachineVirtualCamera vcam;
	private CinemachineTransposer transposer;

	void Start () {
		vcam = GetComponent<CinemachineVirtualCamera> ();
		transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
		
		if (vcam == null) {
			Debug.LogError ("No virtual camera assigned to the CameraShakeSimpleScript script.");
		}
		
		if (transposer == null) {
			Debug.LogError ("No transposer found on the virtual camera.");
		}
	}

	public void ShakeCamera() {	
		ShakeCaller (0.25f, 0.05f);
	}

	//other shake option
	void ShakeCaller (float amount, float duration){
		StartCoroutine (Shake(amount, duration));
	}

	private IEnumerator Shake (float amount, float duration){
		_isRunning = true;

		Vector3 originalOffset = transposer.m_FollowOffset;
		int counter = 0;

		while (duration > 0.01f) {
			counter++;

			var x = Random.Range (-1f, 1f) * (amount/counter);
			var y = Random.Range (-1f, 1f) * (amount/counter);
			
			transposer.m_FollowOffset += new Vector3(x, y, 0);

			// transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (originalPos.x + x, originalPos.y + y, originalPos.z), 0.5f);

			duration -= Time.deltaTime;
			
			yield return new WaitForSeconds (0.02f);
		}

		transposer.m_FollowOffset = originalOffset;

		_isRunning = false;
	}
}
