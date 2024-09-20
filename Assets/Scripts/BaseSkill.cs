using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using MockUp;
using Presentation;
using Presentation.Projectiles;
using Presentation.UI;
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

    [Header("Skill Timings")] 
    [SerializeField] protected SkillDetailUI SkillDetailUI;
    [SerializeField] protected Sprite skillIcon;
    [SerializeField] protected Sprite newSkillIcon;
    [SerializeField] protected SkillState _skillState = SkillState.Idle;

    [Space] [Header("Attack Direction")] private GameObject _firePoint;
    private GameObject _target;
    [SerializeField] private bool useTarget = false;

    [Space] [Header("Sound effects")] [SerializeField]
    private AudioClip castingSound;

    [SerializeField] private AudioClip hitSound;

    public Sprite GetIcon()
    {
        return skillIcon;
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
        if (virtualCamera)
            _cameraShakeSimpleScript = virtualCamera.GetComponent<CameraShakeSimpleScript>();
    }
    
    public void AttachSkillDetailUI(SkillDetailUI skillDetailUI)
    {
        SkillDetailUI = skillDetailUI;
    }

    private void OnCastEnd()
    {
        _skillState = SkillState.Idle;
    }

    public void StartCasting()
    {
        if (newSkillIcon == null)
        {
            newSkillIcon = skillIcon;
        }
        
        if (_skillState == SkillState.Casting)
        {
            ProjectileMovement projectileMovement = _currentInstantiatedVfx.GetComponent<ProjectileMovement>();
            if (projectileMovement.hasUltimate)
            {
                isReady = false;
                StartCoroutine(CustomDelay(0.5f, () =>
                {
                    isReady = true;
                }));
                projectileMovement.Ultimate();
            }
        }
        else
        {
            StartCoroutine(PlayVFX());
        }
    }
    
    private IEnumerator CustomDelay(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    public IEnumerator PlayVFX()
    {
        _skillState = SkillState.PreCasting;
        
        if (SkillDetailUI)
        {
            SkillDetailUI.SetIcon(newSkillIcon);
        }

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
        
        _skillState = SkillState.Idle;
        if (SkillDetailUI)
        {
            SkillDetailUI.SetIcon(skillIcon);
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
                    {"affectCooldown", vfx.affectCooldown}
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
}