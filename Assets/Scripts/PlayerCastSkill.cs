using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCastSkill: MonoBehaviour
{
    [SerializeField] private List<PlayerNormalSkill> normalSkills;
    [SerializeField] private List<PlayerSpecialSkill> specialSkills;
    private float _beginChannelingTime = 0f;
    private int _currentSkill = -1;
    public RotateToMouseScript rotateToMouse;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject skillContainer;

    [SerializeField] public float timeScaleFactor = 0.3f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    void Start()
    {
        if (normalSkills.Count + specialSkills.Count != 4)
        {
            Debug.LogError("Please assign 4 skills to the player");
        }
        
        rotateToMouse = GetComponent<RotateToMouseScript>();    
        if (!rotateToMouse)
        {
            Debug.LogError("Please assign RotateToMouseScript to the player");
        }
        
        UpdateCurrentSkills();
    }

    void UpdateCurrentSkills()
    {
        for (int i = 0; i < normalSkills.Count; i++)
        {
            normalSkills[i].channelingTime *= timeScaleFactor;
            normalSkills[i].AttachSkillContainer(skillContainer);
            normalSkills[i].AttachRotateToMouse(rotateToMouse);
            normalSkills[i].AttachFirePoint(firePoint);
            normalSkills[i].AttachTarget(target);
            normalSkills[i].AttachVirtualCamera(virtualCamera);
        }
        
        for (int i = 0; i < specialSkills.Count; i++)
        {
            specialSkills[i].AttachSkillContainer(skillContainer);
            specialSkills[i].AttachRotateToMouse(rotateToMouse);
            specialSkills[i].AttachFirePoint(firePoint);
            specialSkills[i].AttachTarget(target);
            specialSkills[i].AttachVirtualCamera(virtualCamera);
        }
    }
    
    void Update()
    {
        for (int i = 0; i <= 1; i++)
        {
            if (Input.GetMouseButtonDown(i) && _currentSkill == -1 && !normalSkills[i].IsOnCooldown())
            {
                normalSkills[i].StartCasting();
                if (!specialSkills[i].IsOnCooldown())
                {
                    _currentSkill = i;
                    _beginChannelingTime = Time.time;
                    normalSkills[i].StartChanneling();
                    Debug.Log("Start Channeling Skill " + i);
                }
            }

            if (_currentSkill != -1 && Time.time - _beginChannelingTime >= 0.1f)
            {
                Channeling();
            }

            if (Input.GetMouseButtonUp(i) && _currentSkill == i)
            {
                normalSkills[i].StopChanneling();
                if (Time.time - _beginChannelingTime >= normalSkills[i].channelingTime)
                {
                    Debug.Log("Cast Skill " + i);
                    specialSkills[i].StartCasting();
                }

                _currentSkill = -1;
            }
        }
    }

    private void Channeling()
    {
        Time.timeScale = timeScaleFactor;
    }
}