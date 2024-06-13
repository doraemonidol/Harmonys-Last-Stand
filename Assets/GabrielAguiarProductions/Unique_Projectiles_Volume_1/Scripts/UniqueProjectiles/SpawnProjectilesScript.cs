//
//NOTES:
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//This is just a basic example.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {

    public bool useTarget;
	public bool use2D;
	public bool cameraShake;
	// public Text effectName;
	public RotateToMouseScript rotateToMouse;
	public GameObject firePoint;
    public GameObject target;
    public List<GameObject> VFXs = new List<GameObject> ();

	private int count = 0;
	private float timeToFire = 0f;
	private GameObject effectToSpawn;
	[SerializeField] private Camera mainCamera;

	void Start () {

		if(VFXs.Count>0)
			effectToSpawn = VFXs[0];
		else
			Debug.Log ("Please assign one or more VFXs in inspector");
		
		// if (effectName != null) effectName.text = effectToSpawn.name;

		rotateToMouse.StartUpdateRay ();
        if (useTarget && target != null)
        {
            var collider = target.GetComponent<BoxCollider>();
            if (!collider)
            {
                target.AddComponent<BoxCollider>();
            }
        }
    }

	void Update () {
		if (Input.GetMouseButton (0) && Time.time >= timeToFire) {
			timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
			SpawnVFX ();	
		}

		if (Input.GetKeyDown (KeyCode.Q))
			Next ();
		if (Input.GetKeyDown(KeyCode.E))
			Previous();
	}

	public void SpawnVFX () {
		GameObject vfx;

		var cameraShakeScript = mainCamera.GetComponent<CameraShakeSimpleScript> ();

		if (cameraShake && cameraShakeScript)
			cameraShakeScript.ShakeCamera ();

		if (firePoint) {
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (!useTarget)
            {
                if (rotateToMouse)
                {
                    vfx.transform.localRotation = rotateToMouse.GetRotation();
                }
                else Debug.Log("No RotateToMouseScript found on firePoint.");
            }
            else
            {
                if (target)
                {                    
                    vfx.GetComponent<ProjectileMoveScript>().SetTarget(target, rotateToMouse);
                    rotateToMouse.RotateToMouse(vfx, target.transform.position);                    
                }
                else
                {
                    Destroy(vfx);
                    Debug.Log("No target assigned.");
                }
            }
		}
		else
			vfx = Instantiate (effectToSpawn);		
	}

	public void Next () {
		count++;

		if (count > VFXs.Count)
			count = 0;

		for(int i = 0; i < VFXs.Count; i++){
			if (count == i)	effectToSpawn = VFXs [i];
			// if (effectName != null)	effectName.text = effectToSpawn.name;
		}
	}

	public void Previous () {
		count--;

		if (count < 0)
			count = VFXs.Count;

		for (int i = 0; i < VFXs.Count; i++) {
			if (count == i) effectToSpawn = VFXs [i];
			// if (effectName )	effectName.text = effectToSpawn.name;
		}
	}

	public void CameraShake () {
		cameraShake = !cameraShake;
	}
}
