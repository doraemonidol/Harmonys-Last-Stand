using System.Collections.Generic;
using DTT.AreaOfEffectRegions;
using Logic;
using UnityEngine;

public class PlayerNormalSkill : PlayerSkill
{
    [Space]
    [Header("Channeling Info")]
    [SerializeField] public float channelingTime;
    private bool isChanneling;
    
    [Space]
    [Header("Attack Indicator Regions")]
    [SerializeField] private GameObject skillIndicator;
    private CircleRegion _circleRegion;
    private LineRegion _lineRegion;
    private ArcRegion _arcRegion;
    private ScatterLineRegion _scatterLineRegion;
    public override void Start()
    {
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

    public override void Update()
    {
        if (skillIndicator == null)
        {
            return;
        }
        
        if (isChanneling)
        {
            skillIndicator.SetActive(true);
            SetAngle(Rotator.GetRotation().eulerAngles.y);
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

    public override void UpdateChannelingTime(float timeScaleFactor)
    {
        channelingTime *= timeScaleFactor;
    }

    public override void StartChanneling()
    {
        Debug.Log("Start Channeling");
        isChanneling = true;
    }
    
    public override void StopChanneling()
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

    public override double GetChannelingTime()
    {
        return channelingTime;
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