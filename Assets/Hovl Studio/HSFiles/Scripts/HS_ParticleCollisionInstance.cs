/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/

using System.Collections.Generic;
using UnityEngine;

public class HS_ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset;
    public Vector3 rotationOffset = new(0, 0, 0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect;
    private readonly List<ParticleCollisionEvent> collisionEvents = new();
    private ParticleSystem part;
    private ParticleSystem ps;

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        var numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (var i = 0; i < numCollisionEvents; i++)
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset,
                    new Quaternion());
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation)
                {
                    instance.transform.LookAt(transform.position);
                }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset)
                {
                    instance.transform.rotation = Quaternion.Euler(rotationOffset);
                }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }

                Destroy(instance, DestroyTimeDelay);
            }

        if (DestoyMainEffect) Destroy(gameObject, DestroyTimeDelay + 0.5f);
    }
}