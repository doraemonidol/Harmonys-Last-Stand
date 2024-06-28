using System.Collections;
using System.Diagnostics;
using Cinemachine;
using DTT.AreaOfEffectRegions;
using Presentation.MainCharacter;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public abstract class BaseSkill : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] protected VFX preCastVfx;
    [SerializeField] protected VFX projectileVfx;
    [SerializeField] protected VFX postCastVfx;
    
    [SerializeField] private bool cameraShake;
    [SerializeField] private bool rotateToDirection = true;
    private CameraShakeSimpleScript _cameraShakeSimpleScript;
    protected RotateToDirection Rotator;

    [Header("Skill Timings")]
    [SerializeField] private float cooldownTime = 1f;
    private SkillState _skillState = SkillState.Idle;
    
    [Space]
    [Header("Attack Direction")]
    private GameObject _firePoint;
    private GameObject _target;
    [SerializeField] private bool useTarget = false;
    
    [Space]
    [Header("Sound effects")]
    [SerializeField] private AudioClip castingSound;
    [SerializeField] private AudioClip hitSound;
    
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Base Class Start");
    }

    public void AttachRotator(RotateToDirection rotator)
    {
        this.Rotator = rotator;
    }
    
    public void AttachFirePoint(GameObject firePoint)
    {
        this._firePoint = firePoint;
    }
    
    public void AttachTarget(GameObject target)
    {
        this._target = target;
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
        _skillState = SkillState.PreCasting;
        
        if (cameraShake)
        {
            _cameraShakeSimpleScript.ShakeCamera();
        }
        
        if (preCastVfx.HasVFX())
        {
            StartCoroutine(RenderVFX(preCastVfx));
            yield return new WaitForSeconds(preCastVfx.duration);
        }
        
        _skillState = SkillState.Casting;
        if (projectileVfx.HasVFX())
        {
            StartCoroutine(RenderVFX(projectileVfx));
            yield return new WaitForSeconds(projectileVfx.duration);
        }
        
        _skillState = SkillState.PostCasting;
        if (postCastVfx.HasVFX())
        {
            StartCoroutine(RenderVFX(postCastVfx));
            yield return new WaitForSeconds(postCastVfx.duration);
        }

        _skillState = SkillState.OnCooldown;
        
        StartCoroutine(Cooldown());
    }

    private IEnumerator RenderVFX(VFX vfx)
    {
        GameObject instantiatedVfx = Instantiate(vfx.vfx, vfx.GetPosition(), Quaternion.identity);
        
        if (!instantiatedVfx)
        {
            Debug.LogError("No VFX found");
            yield break;
        }
                
        if (Rotator != null && rotateToDirection)
        {
            instantiatedVfx.transform.localRotation = Rotator.GetRotation();
        }

        if (vfx.autoDestroy)
        {
            Destroy(instantiatedVfx, vfx.duration);
        }
    }
    
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        _skillState = SkillState.Idle;
    }
    
    public bool IsOnCooldown()
    {
        return _skillState == SkillState.OnCooldown;
    }
}