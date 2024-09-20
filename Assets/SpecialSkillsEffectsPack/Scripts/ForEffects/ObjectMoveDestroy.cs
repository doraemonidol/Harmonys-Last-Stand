using UnityEngine;

public class ObjectMoveDestroy : MonoBehaviour
{
    public GameObject m_gameObjectMain;
    public GameObject m_gameObjectTail;
    public Transform m_hitObject;
    public float maxLength;
    public bool isDestroy;
    public float ObjectDestroyTime;
    public float TailDestroyTime;
    public float HitObjectDestroyTime;
    public float maxTime = 1;
    public float MoveSpeed = 10;
    public bool isCheckHitTag;
    public string mtag;
    public bool isShieldActive;
    public bool isHitMake = true;
    private bool ishit;
    private GameObject m_makedObject;
    private float m_scalefactor;

    private float time;

    private void Start()
    {
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor; //transform.parent.localScale.x;
        time = Time.time;
    }

    private void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * m_scalefactor);
        if (!ishit)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength))
                HitObj(hit);
        }

        if (isDestroy)
            if (Time.time > time + ObjectDestroyTime)
            {
                MakeHitObject(transform);
                Destroy(gameObject);
            }
    }

    private void MakeHitObject(RaycastHit hit)
    {
        if (isHitMake == false)
            return;
        m_makedObject = Instantiate(m_hitObject, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
        m_makedObject.transform.parent = transform.parent;
        m_makedObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void MakeHitObject(Transform point)
    {
        if (isHitMake == false)
            return;
        m_makedObject = Instantiate(m_hitObject, point.transform.position, point.rotation).gameObject;
        m_makedObject.transform.parent = transform.parent;
        m_makedObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void HitObj(RaycastHit hit)
    {
        if (isCheckHitTag)
            if (hit.transform.tag != mtag)
                return;
        ishit = true;
        if (m_gameObjectTail)
            m_gameObjectTail.transform.parent = null;
        MakeHitObject(hit);

        if (isShieldActive)
        {
            var m_sc = hit.transform.GetComponent<ShieldActivate>();
            if (m_sc)
                m_sc.AddHitObject(hit.point);
        }

        Destroy(gameObject);
        Destroy(m_gameObjectTail, TailDestroyTime);
        Destroy(m_makedObject, HitObjectDestroyTime);
    }
}