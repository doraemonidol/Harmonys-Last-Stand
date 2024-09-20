using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HDRPDecalTransferMatrix : MonoBehaviour
{
    MaterialPropertyBlock propertyget;
    MeshRenderer rd;

    void Update()
    {
        Matrix4x4 invTransformMatrix = transform.worldToLocalMatrix;

        if (propertyget == null)
            propertyget = new MaterialPropertyBlock();

        propertyget.SetMatrix("_InverseTransformMatrix", invTransformMatrix);

        if (rd == null)
            rd = GetComponent<MeshRenderer>();

        //rd.GetPropertyBlock(propertyget);
        rd.SetPropertyBlock(propertyget);
    }
}
