using UnityEngine;

public class HS_TargetProjectile : MonoBehaviour
{
    public float speed = 15f;
    public GameObject hit;
    public GameObject flash;
    public GameObject[] Detached;
    public bool LocalRotation;
    public float sideAngle = 25;
    public float upAngle = 20;
    private float randomSideAngle;

    [Space] [Header("PROJECTILE PATH")] private float randomUpAngle;

    private float startDistanceToTarget;
    private Transform target;
    private Vector3 targetOffset;

    private void Start()
    {
        FlashEffect();
        newRandom();
    }

    private void Update()
    {
        if (target == null)
        {
            foreach (var detachedPrefab in Detached)
                if (detachedPrefab != null)
                    detachedPrefab.transform.parent = null;
            Destroy(gameObject);
            return;
        }

        var distanceToTarget = Vector3.Distance(target.position + targetOffset, transform.position);
        var angleRange = (distanceToTarget - 10) / 60;
        if (angleRange < 0) angleRange = 0;

        var saturatedDistanceToTarget = distanceToTarget / startDistanceToTarget;
        if (saturatedDistanceToTarget < 0.5)
            saturatedDistanceToTarget -= 0.5f - saturatedDistanceToTarget;
        saturatedDistanceToTarget -= angleRange;
        if (saturatedDistanceToTarget <= 0)
            saturatedDistanceToTarget = 0;

        var forward = target.position + targetOffset - transform.position;
        var crossDirection = Vector3.Cross(forward, Vector3.up);
        var randomDeltaRotation = Quaternion.Euler(0, randomSideAngle * saturatedDistanceToTarget, 0) *
                                  Quaternion.AngleAxis(randomUpAngle * saturatedDistanceToTarget, crossDirection);
        var direction = randomDeltaRotation * forward;

        var distanceThisFrame = Time.deltaTime * speed;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void newRandom()
    {
        randomUpAngle = Random.Range(0, upAngle);
        randomSideAngle = Random.Range(-sideAngle, sideAngle);
    }

    //Link from another script
    //TARGET POSITION + TARGET OFFSET
    public void UpdateTarget(Transform targetPosition, Vector3 Offset)
    {
        target = targetPosition;
        targetOffset = Offset;
        startDistanceToTarget = Vector3.Distance(target.position + targetOffset, transform.position);
    }

    private void FlashEffect()
    {
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
    }

    private void HitTarget()
    {
        if (hit != null)
        {
            var hitRotation = transform.rotation;
            if (LocalRotation) hitRotation = Quaternion.Euler(0, 0, 0);
            var hitInstance = Instantiate(hit, target.position + targetOffset, hitRotation);
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }

        foreach (var detachedPrefab in Detached)
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
                Destroy(detachedPrefab, 1);
            }

        Destroy(gameObject);
    }
}