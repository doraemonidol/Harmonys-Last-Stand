using System.Collections.Generic;
using DTT.AreaOfEffectRegions;
using UnityEngine;

public class PlayerNormalSkill : PlayerSkill
{
    [Header("Channeling Info")]
    private bool isChanneling;
    [SerializeField] public float channelingTime;
    
    [Space]
    [Header("Attack Indicator Regions")]
    [SerializeField] private GameObject skillIndicator;
    private CircleRegion _circleRegion;
    private LineRegion _lineRegion;
    private ArcRegion _arcRegion;
    private ScatterLineRegion _scatterLineRegion;
    public void Start()
    {
        base.Start();
        isChanneling = false;
        if (skillIndicator)
        {
            skillIndicator.SetActive(false);
        
            _circleRegion = skillIndicator.GetComponent<CircleRegion>();
            _lineRegion = skillIndicator.GetComponent<LineRegion>();
            _arcRegion = skillIndicator.GetComponent<ArcRegion>();
            _scatterLineRegion = skillIndicator.GetComponent<ScatterLineRegion>();
        }
    }

    void Update()
    {
        base.Update();
        if (!skillIndicator)
        {
            return;
        }
        
        if (isChanneling)
        {
            skillIndicator.SetActive(true);
            SetAngle(rotateToMouse.GetRotation().eulerAngles.y);
            if (_circleRegion)
            {
                _circleRegion.FillProgress += Time.deltaTime / channelingTime;
            } else if (_lineRegion)
            {
                _lineRegion.FillProgress += Time.deltaTime / channelingTime;
            } else if (_arcRegion)
            {
                _arcRegion.FillProgress += Time.deltaTime / channelingTime;
            } else if (_scatterLineRegion)
            {
                _scatterLineRegion.FillProgress += Time.deltaTime / channelingTime;
            }
        }
        else
        {
            skillIndicator.SetActive(false);
        }
    }
    
    public void StartChanneling()
    {
        Debug.Log("Start Channeling");
        isChanneling = true;
    }
    
    public void StopChanneling()
    {
        Time.timeScale = 1f;
        isChanneling = false;
        if (_circleRegion)
        {
            _circleRegion.FillProgress = 0;
        } else if (_lineRegion)
        {
            _lineRegion.FillProgress = 0;
        } else if (_arcRegion)
        {
            _arcRegion.FillProgress = 0;
        } else if (_scatterLineRegion)
        {
            _scatterLineRegion.FillProgress = 0;
        }
    }
    
    public void SetAngle(float angle)
    {
        if (_arcRegion)
        {
            _arcRegion.Angle = angle;
        } else if (_lineRegion)
        {
            _lineRegion.Angle = angle;
        }
    }
}