//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouseScript : MonoBehaviour {

	public float maximumLenght;

	private Ray rayMouse;
	private Vector3 pos;
	private Vector3 direction;
	private Quaternion rotation;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private GameObject directionIndicator;
	[SerializeField] private Camera cam;
	private WaitForSeconds updateTime = new WaitForSeconds (0.01f); 

	void Start () {
		cam = Camera.main;
	}

	public void StartUpdateRay (){
		StartCoroutine (UpdateRay());
	}

	IEnumerator UpdateRay (){
		if (cam) {
			RaycastHit hit;
			var mousePos = Input.mousePosition;
			var ray = cam.ScreenPointToRay(mousePos);
			
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask)) {
				mousePos = hit.point;
			} else {
				mousePos = ray.GetPoint(maximumLenght);
			}
			
			Debug.Log(mousePos);
			
			RotateToMouse (directionIndicator, mousePos);
			yield return updateTime;
			StartCoroutine (UpdateRay ());
		} else
			Debug.Log ("Camera not set");
	}

	public void RotateToMouse (GameObject obj, Vector3 destination ) {
		direction = destination - obj.transform.position;
		direction.y = 0;
		
		rotation = Quaternion.LookRotation (direction);
		obj.transform.forward = direction;
	}

	public Vector3 GetDirection () {
		return direction;
	}

	public Quaternion GetRotation () {
		return rotation;
	}
}
