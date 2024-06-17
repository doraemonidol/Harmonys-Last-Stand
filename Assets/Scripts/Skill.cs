using System.Collections;
using System.Collections.Generic;
using DTT.AreaOfEffectRegions;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private GameObject skillIndicator;

    [SerializeField] public float ChannelingTime;
    // [SerializeField] private
    private bool _isChanneling = false;
    
    private CircleRegion _circleRegion;
    private LineRegion _lineRegion;
    private ArcRegion _arcRegion;
    private ScatterLineRegion _scatterLineRegion;
    
    public RotateToMouseScript rotateToMouse;
    
    // Start is called before the first frame update
    void Start()
    {
        skillIndicator.SetActive(false);
        
        _circleRegion = skillIndicator.GetComponent<CircleRegion>();
        _lineRegion = skillIndicator.GetComponent<LineRegion>();
        _arcRegion = skillIndicator.GetComponent<ArcRegion>();
        _scatterLineRegion = skillIndicator.GetComponent<ScatterLineRegion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!skillIndicator)
        {
            return;
        }
        
        if (_isChanneling)
        {
            skillIndicator.SetActive(true);
            SetAngle(rotateToMouse.GetRotation().eulerAngles.y);
            if (_circleRegion)
            {
                _circleRegion.FillProgress += Time.deltaTime / ChannelingTime;
            } else if (_lineRegion)
            {
                _lineRegion.FillProgress += Time.deltaTime / ChannelingTime;
            } else if (_arcRegion)
            {
                _arcRegion.FillProgress += Time.deltaTime / ChannelingTime;
            } else if (_scatterLineRegion)
            {
                _scatterLineRegion.FillProgress += Time.deltaTime / ChannelingTime;
            }
        }
        else
        {
            skillIndicator.SetActive(false);
        }
    }
    
    public GameObject GetVFX()
    {
        return vfx;
    }
    
    public void StartChanneling(RotateToMouseScript rotateToMouse)
    {
        this.rotateToMouse = rotateToMouse;
        _isChanneling = true;
    }
    
    public void StopChanneling()
    {
        Time.timeScale = 1f;
        _isChanneling = false;
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
