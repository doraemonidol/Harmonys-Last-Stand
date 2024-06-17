using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell: MonoBehaviour
{
    [SerializeField] private List<Skill> skills;
    private float _beginChannelingTime = 0f;
    private int _currentSkill = -1;
    private SpawnProjectilesScript _spawnProjectilesScript;
    public RotateToMouseScript rotateToMouse;

    [SerializeField] public float timeScaleFactor = 0.3f;
    
    void Start()
    {
        if (skills.Count < 4)
        {
            Debug.LogError("Please assign 4 skills to the player");
        }
        
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].ChannelingTime *= timeScaleFactor;
        }
        
        _spawnProjectilesScript = GetComponent<SpawnProjectilesScript>();
        if (!_spawnProjectilesScript)
        {
            Debug.LogError("Please assign SpawnProjectilesScript to the player");
        }
        rotateToMouse = GetComponent<RotateToMouseScript>();    
        if (!rotateToMouse)
        {
            Debug.LogError("Please assign RotateToMouseScript to the player");
        }
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentSkill == -1)
        {
            _currentSkill = 0;
            _beginChannelingTime = Time.time;
            skills[0].StartChanneling(rotateToMouse);
            Debug.Log("Start Channeling");
        }
        
        if (Input.GetMouseButtonDown(1) && _currentSkill == -1)
        {
            _currentSkill = 2;
            _beginChannelingTime = Time.time;
            skills[2].StartChanneling(rotateToMouse);
            Debug.Log("Start Channeling");
        }

        if (_currentSkill != -1 && Time.time - _beginChannelingTime >= 0.1f)
        {
            Channeling();
        }
        
        if (Input.GetMouseButtonUp(0) && _currentSkill == 0)
        {
            skills[0].StopChanneling();
            if (Time.time - _beginChannelingTime >= skills[1].ChannelingTime)
            {
                Debug.Log("Cast Skill 1");
                CastSpell(1);
            }
            else
            {
                Debug.Log("Normal Attack");
                CastSpell(0);
            }
            _currentSkill = -1;
        }
        
        if (Input.GetMouseButtonUp(1) && _currentSkill == 2)
        {
            skills[2].StopChanneling();
            if (Time.time - _beginChannelingTime >= skills[3].ChannelingTime)
            {
                Debug.Log("Cast Skill 3");
                CastSpell(3);
            }
            else
            {
                Debug.Log("Cast Skill 2");
                CastSpell(2);
            }
            _currentSkill = -1;
        }
    }

    private void Channeling()
    {
        Time.timeScale = timeScaleFactor;
    }

    private void CastSpell(int skillIndex)
    {
        _spawnProjectilesScript.SpawnVFX(skills[skillIndex].GetVFX());
    }
}