using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Common;
using MockUp;
using Presentation;
using UnityEngine;
using UnityEngine.Serialization;

public class CloneCastSkill: MonoBehaviour
{
    [SerializeField]private List<PlayerSkill> normalSkills;
    [SerializeField]private List<PlayerSkill> specialSkills;
    [SerializeField] List<PWeapon> weapons;
    [SerializeField] private int _activeWeapon = 0;
    private RotateToMouseScript _rotateToMouse;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject target;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public Identity CloneLogicHandle;

    [SerializeField] private AureliaMockUp aurelia;
    
    void Start()
    {
        aurelia = GameObject.FindGameObjectWithTag("Player").GetComponent<AureliaMockUp>();
        aurelia.cloneCastSkill = this;
        
        _rotateToMouse = GetComponent<RotateToMouseScript>();
        if (!_rotateToMouse)
        {
            Debug.LogError("Please assign RotateToMouseScript to the player");
        }
        
        _rotateToMouse = GetComponent<RotateToMouseScript>();    
        if (!_rotateToMouse)
        {
            Debug.LogError("Please assign RotateToMouseScript to the player");
        }
        
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        if (!virtualCamera)
        {
            Debug.LogError("Please assign CinemachineVirtualCamera to the player");
        }
        
        UpdateCurrentSkills();
    }

    public void UpdateCurrentSkills()
    {
        _activeWeapon = aurelia.GetActiveWeapon();
        weapons[_activeWeapon].SetOwner(aurelia.LogicHandle);
            
        for (int i = 0; i < normalSkills.Count; i++)
        {
            normalSkills[i] = (PlayerNormalSkill)weapons[_activeWeapon].GetNormalSkills()[i];
            normalSkills[i].AttachRotator(_rotateToMouse);
            normalSkills[i].AttachFirePoint(firePoint);
            normalSkills[i].AttachTarget(target);
            normalSkills[i].AttachVirtualCamera(virtualCamera);
        }
        
        for (int i = 0; i < specialSkills.Count; i++)
        {
            specialSkills[i] = (PlayerSpecialSkill)weapons[_activeWeapon].GetSpecialSkills()[i];
            specialSkills[i].AttachRotator(_rotateToMouse);
            specialSkills[i].AttachFirePoint(firePoint);
            specialSkills[i].AttachTarget(target);
            specialSkills[i].AttachVirtualCamera(virtualCamera);
        }
    }
    
    public void CastNormalSkill(int skillIndex)
    {
        Debug.Log("Clone Cast Normal Skill");
        normalSkills[skillIndex].StartCasting();
    }
    
    public void CastSpecialSkill(int skillIndex)
    {
        if (weapons[_activeWeapon].entityType == EntityTypeEnum.VIOLIN && skillIndex == 0)
        {
            Debug.Log("Clone Can't Cast Special Skill");
            return;
        }
        Debug.Log("Clone Cast Special Skill");
        specialSkills[skillIndex].StartCasting();
    }

    private void OnDestroy()
    {
        aurelia.cloneCastSkill = null;
    }
}
