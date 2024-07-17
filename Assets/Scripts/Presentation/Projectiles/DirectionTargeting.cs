using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Projectiles
{
    public class DirectionTargeting : ProjectileMovement
    {
        public bool rotate = false;
        public float rotateAmount = 45;
        public float speed;
        [Tooltip("From 0% to 100%")]
        public float accuracy;
        private bool _collided = false;
        private Vector3 _offset;
        private Rigidbody _rb;
        private RotateToDirection _rotateToMouse;
        private GameObject _target;
        
        public void Start()
        {
            base.Start();
            StartPos = transform.position;
            _rb = GetComponent<Rigidbody>();
            
            if (!Mathf.Approximately(accuracy, 100))
            {
                accuracy = 1 - (accuracy / 100);

                for (int i = 0; i < 2; i++)
                {
                    var val = 1 * Random.Range(-accuracy, accuracy);
                    var index = Random.Range(0, 2);
                    if (i == 0)
                    {
                        if (index == 0)
                            _offset = new Vector3(0, -val, 0);
                        else
                            _offset = new Vector3(0, val, 0);
                    }
                    else
                    {
                        if (index == 0)
                            _offset = new Vector3(0, _offset.y, -val);
                        else
                            _offset = new Vector3(0, _offset.y, val);
                    }
                }
            }
            
            if (muzzlePrefab != null) {
                var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.parent = transform;
                muzzleVFX.transform.forward = gameObject.transform.forward + _offset;
                var ps = muzzleVFX.GetComponent<ParticleSystem>();
                if (ps != null)
                    Destroy (muzzleVFX, ps.main.duration);
                else {
                    if (muzzleVFX.transform.childCount > 0)
                    {
                        var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(muzzleVFX, psChild.main.duration);
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            if (_target != null)
                _rotateToMouse.RotateTo (gameObject, _target.transform.position);
            if (rotate)
                transform.Rotate(0, 0, rotateAmount, Space.Self);
            if (speed != 0 && _rb != null)
                _rb.position += (transform.forward + _offset) * (speed * Time.deltaTime);  
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Bullet") && !_collided)
            {
                _collided = true;
                Debug.Log("Collided with " + other.gameObject.name);

                if (trails.Count > 0)
                {
                    for (int i = 0; i < trails.Count; i++)
                    {
                        trails[i].transform.parent = null;
                        var ps = trails[i].GetComponent<ParticleSystem>();
                        if (ps != null)
                        {
                            ps.Stop();
                            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                        }
                    }
                }

                speed = 0;
                GetComponent<Rigidbody>().isKinematic = true;

                ContactPoint contact = other.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                if (hitPrefab != null)
                {
                    var hitVFX = Instantiate(hitPrefab, pos, rot) as GameObject;
                    
                    hitVFX.transform.parent = other.transform;

                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(hitVFX, psChild.main.duration);
                    }
                    else
                        Destroy(hitVFX, ps.main.duration);
                }

                StartCoroutine(DestroyParticle(0f));
            }
        }

        public override void Ultimate()
        {
            throw new System.NotImplementedException();
        }

        private IEnumerator DestroyParticle (float waitTime) {

            if (transform.childCount > 0 && waitTime != 0) {
                List<Transform> tList = new List<Transform> ();

                foreach (Transform t in transform.GetChild(0).transform) {
                    tList.Add (t);
                }		

                while (transform.GetChild(0).localScale.x > 0) {
                    yield return new WaitForSeconds (0.01f);
                    transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
                    for (int i = 0; i < tList.Count; i++) {
                        tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
                    }
                }
            }
		
            yield return new WaitForSeconds (waitTime);
            Destroy (gameObject);
        }

        public void SetTarget (GameObject trg, RotateToMouseScript rotateTo)
        {
            _target = trg;
            _rotateToMouse = rotateTo;
        }
    }
}