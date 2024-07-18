using System.Collections.Generic;
using Common;
using MockUp;
using UnityEngine;

public class SimpleObjectMake : _ObjectMakeBase
{
    public Vector3 m_randomRotationValue;

    private void Start()
    {
        for (var i = 0; i < m_makeObjs.Length; i++)
        {
            var m_obj = Instantiate(m_makeObjs[i], transform.position, transform.rotation);
            m_obj.transform.parent = transform;
            m_obj.transform.rotation *= Quaternion.Euler(GetRandomVector(m_randomRotationValue));

            if (m_movePos)
                if (m_obj.GetComponent<MoveToObject>())
                {
                    var m_script = m_obj.GetComponent<MoveToObject>();
                    m_script.m_movePos = m_movePos;
                }
        }
        AssignSkillCollideInfo();
    }

    public void AssignSkillCollideInfo()
    {
        SkillColliderInfo skillColliderInfo = gameObject.GetComponent<SkillColliderInfo>();
        Stack<GameObject> stack = new Stack<GameObject>();
        stack.Push(gameObject);
        while (stack.Count > 0)
        {
            GameObject current = stack.Pop();
            if (current.GetComponent<SkillColliderInfo>() == null)
            {
                current.AddComponent<SkillColliderInfo>();
                current.GetComponent<SkillColliderInfo>().Attacker = (Identity)skillColliderInfo.Attacker;
                current.GetComponent<SkillColliderInfo>().Skill = (Identity)skillColliderInfo.Skill;
                current.GetComponent<SkillColliderInfo>().affectCooldown = skillColliderInfo.affectCooldown;
            }
            foreach (Transform child in current.transform)
            {
                stack.Push(child.gameObject);
            }
        }
    }
}