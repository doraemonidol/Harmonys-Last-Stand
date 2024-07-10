using System.Collections;
using UnityEngine;

public class HS_FrontAttack : MonoBehaviour
{
    public Transform pivot;
    public Vector3 startRotation;
    public float speed = 15f;
    public float drug = 1f;
    public GameObject craterPrefab;
    public ParticleSystem ps;
    public bool playPS;
    public float spawnRate = 1f;
    public float spawnDuration = 1f;
    public float positionOffset;
    public bool changeScale;

    [Space] [Header("Effect with Mesh animation")]
    public bool effectWithAnimation;

    public Animator[] anim;
    public float delay;
    public bool playMeshEffect;
    private float randomTimer;
    private float startSpeed;
    private Vector3 stepPosition;

    private void Update()
    {
        if (playMeshEffect)
        {
            StartCoroutine(MeshEffect());
            playMeshEffect = false;
        }
    }

    public void PrepareAttack(Vector3 targetPoint)
    {
        if (effectWithAnimation)
        {
            StartCoroutine(MeshEffect());
        }
        else
        {
            if (playPS) ps.Play();
            startSpeed = speed;
            transform.parent = null;
            transform.position = pivot.position;
            var lookPos = targetPoint - transform.position;
            lookPos.y = 0;
            if (!playPS)
                transform.rotation = Quaternion.LookRotation(lookPos);
            else
                transform.rotation = Quaternion.LookRotation(lookPos) * Quaternion.Euler(startRotation);
            stepPosition = pivot.position;
            randomTimer = 0;
            StartCoroutine(StartMove());
        }
    }

    public IEnumerator MeshEffect()
    {
        if (playPS) ps.Play();
        yield return new WaitForSeconds(delay);
        foreach (var animS in anim) animS.SetTrigger("Attack");
    }

    public IEnumerator StartMove()
    {
        while (true)
        {
            randomTimer += Time.deltaTime;
            startSpeed = startSpeed * drug;
            transform.position += transform.forward * (startSpeed * Time.deltaTime);

            var heading = transform.position - stepPosition;
            var distance = heading.magnitude;

            if (distance > spawnRate)
            {
                if (craterPrefab != null)
                {
                    var randomPosition = new Vector3(Random.Range(-positionOffset, positionOffset), 0,
                        Random.Range(-positionOffset, positionOffset));
                    var pos = transform.position + randomPosition * randomTimer * 2;

                    //to create effects on terrain
                    if (Terrain.activeTerrain != null) pos.y = Terrain.activeTerrain.SampleHeight(transform.position);

                    var craterInstance = Instantiate(craterPrefab, pos, Quaternion.identity);
                    if (changeScale)
                        craterInstance.transform.localScale +=
                            new Vector3(randomTimer * 2, randomTimer * 2, randomTimer * 2);
                    var craterPs = craterInstance.GetComponent<ParticleSystem>();
                    if (craterPs != null)
                    {
                        Destroy(craterInstance, craterPs.main.duration);
                    }
                    else
                    {
                        var flashPsParts = craterInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                        Destroy(craterInstance, flashPsParts.main.duration);
                    }
                }

                //distance = 0;
                stepPosition = transform.position;
            }

            if (randomTimer > spawnDuration)
            {
                transform.parent = pivot;
                transform.position = pivot.position;
                transform.rotation = Quaternion.Euler(startRotation);
                yield break;
            }

            yield return null;
        }
    }
}