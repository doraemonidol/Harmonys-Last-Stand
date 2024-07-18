using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using MockUp;
using Presentation;
using Presentation.Projectiles;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public abstract class BaseSkill : PSkill
{
    [Header("VFX")] [SerializeField] protected VFX preCastVfx;
    [SerializeField] protected VFX projectileVfx;
    [SerializeField] protected VFX postCastVfx;
    private GameObject _currentInstantiatedVfx;

    [SerializeField] private bool cameraShake;
    [SerializeField] private bool rotateToDirection = true;
    private CameraShakeSimpleScript _cameraShakeSimpleScript;
    protected RotateToDirection Rotator;

    [Header("Skill Timings")] [SerializeField]

    private SkillState _skillState = SkillState.Idle;

    [Space] [Header("Attack Direction")] private GameObject _firePoint;
    private GameObject _target;
    [SerializeField] private bool useTarget = false;

    [Space] [Header("Sound effects")] [SerializeField]
    private AudioClip castingSound;

    [SerializeField] private AudioClip hitSound;

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
        if (virtualCamera)
            _cameraShakeSimpleScript = virtualCamera.GetComponent<CameraShakeSimpleScript>();
    }

    public void StartCasting()
    {
        if (_skillState == SkillState.Casting)
        {
            ProjectileMovement projectileMovement = _currentInstantiatedVfx.GetComponent<ProjectileMovement>();
            if (projectileMovement.hasUltimate)
            {
                projectileMovement.Ultimate();
            }
        }
        else
        {
            StartCoroutine(PlayVFX());
        }
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

        // Debug.Log("Skill State: Casting");
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
    }

    private IEnumerator RenderVFX(VFX vfx)
    {
        GameObject instantiatedVfx = Instantiate(vfx.vfx, vfx.GetPosition(), Quaternion.identity);

        if (!instantiatedVfx)
        {
            Debug.LogError("No VFX found");
            yield break;
        }

        if (instantiatedVfx.GetComponent<ProjectileMovement>())
        {
            instantiatedVfx.GetComponent<ProjectileMovement>().AssignSkillCollideInfo(
                new Dictionary<string, object>()
                {
                    {"Attacker", this.Owner},
                    {"Skill", this.LogicHandle},
                }
            );
        }

        _currentInstantiatedVfx = instantiatedVfx;
                
        if (Rotator != null && rotateToDirection)
        {
            instantiatedVfx.transform.localRotation = Rotator.GetRotation();
        }

        if (vfx.autoDestroy)
        {
            Destroy(instantiatedVfx, vfx.duration);
        }
    }
    
    public bool IsOnCooldown()
    {
        return _skillState == SkillState.OnCooldown;
    }
}