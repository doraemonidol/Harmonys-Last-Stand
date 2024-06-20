using System.Collections;
using Cinemachine;
using DTT.AreaOfEffectRegions;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseSkill : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private GameObject preCastVfx;
    [SerializeField] private float preCastTime;
    [SerializeField] private Transform preCastPosition;
    [SerializeField] private GameObject castVfx;
    [SerializeField] private float castTime;
    [SerializeField] private Transform castPosition;
    [SerializeField] private GameObject postCastVfx;
    [SerializeField] private float postCastTime;
    [SerializeField] private Transform postCastPosition;
    
    
    [SerializeField] protected GameObject skillContainer;
    [SerializeField] protected bool stickToCaster;
    
    [SerializeField] private bool cameraShake;
    private CameraShakeSimpleScript _cameraShakeSimpleScript;

    [Header("Skill Timings")]
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField] private SkillState skillState = SkillState.Idle;
    
    [Space]
    [Header("Attack Direction")]
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject target;
    
    [Space]
    [Header("Sound effects")]
    [SerializeField] private AudioClip castingSound;
    [SerializeField] private AudioClip hitSound;
    
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Base Class Start");
    }
    
    public void AttachFirePoint(GameObject firePoint)
    {
        this.firePoint = firePoint;
    }
    
    public void AttachTarget(GameObject target)
    {
        this.target = target;
    }
    
    public void AttachVirtualCamera(CinemachineVirtualCamera virtualCamera)
    {
        _cameraShakeSimpleScript = virtualCamera.GetComponent<CameraShakeSimpleScript>();
    }

    // Update is called once per frame
    public void Update()
    {
    }
    
    public void StartCasting()
    {
        StartCoroutine(PlayVFX());
    }
    
    private IEnumerator PlayVFX()
    {
        skillState = SkillState.PreCasting;
        
        if (cameraShake)
        {
            _cameraShakeSimpleScript.ShakeCamera();
        }
        
        if (preCastVfx)
        {
            StartCoroutine(RenderVFX(preCastVfx, preCastPosition.position));
            yield return new WaitForSeconds(preCastTime);
        }
        
        skillState = SkillState.Casting;
        if (castVfx)
        {
            StartCoroutine(RenderVFX(castVfx, castPosition.position));
            yield return new WaitForSeconds(castTime);
        }
        
        skillState = SkillState.PostCasting;
        if (postCastVfx)
        {
            StartCoroutine(RenderVFX(postCastVfx, postCastPosition.position));
            yield return new WaitForSeconds(postCastTime);
        }

        skillState = SkillState.OnCooldown;
        
        StartCoroutine(Cooldown());
    }

    private IEnumerator RenderVFX(GameObject effectToRender, Vector3 position)
    {
        GameObject vfx = Instantiate(effectToRender, position, Quaternion.identity);
        
        if (stickToCaster)
            vfx.transform.parent = skillContainer.transform;
        
        if (!vfx)
        {
            Debug.LogError("No VFX found");
            yield break;
        }

        StartCoroutine(DestroyVfx(vfx));
    }

    private IEnumerator DestroyVfx(GameObject vfx)
    {
        yield return new WaitForSeconds(vfx.GetComponent<ParticleSystem>().main.duration);
        Destroy(vfx);
    }
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        skillState = SkillState.Idle;
    }
    
    public bool IsOnCooldown()
    {
        return skillState == SkillState.OnCooldown;
    }
}