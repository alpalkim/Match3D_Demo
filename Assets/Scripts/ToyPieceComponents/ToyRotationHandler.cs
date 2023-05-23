using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyRotationHandler : MonoBehaviour
{
    public enum RotationType { Identitiy, XAxis90, XAxis90YAxis90, XAxis20, YAxis90ZAxis90}

    [SerializeField] RotationType rotationType;
    public Quaternion RotationValue { get; private set; }
    public Vector3 EulerRotation { get; private set; }



    private void Awake()
    {
        SetRotationValue();
    }

    private void SetRotationValue()
    {
        if (rotationType == RotationType.Identitiy)
        {
            EulerRotation = Vector3.zero;
            RotationValue = Quaternion.Euler(EulerRotation);
        }
        else if (rotationType == RotationType.XAxis90)
        {
            EulerRotation = new Vector3(-115, 0, 0);
            RotationValue = Quaternion.Euler(EulerRotation);
        }
        else if (rotationType == RotationType.XAxis90YAxis90)
        {
            EulerRotation = new Vector3(-90, 0, -90);
            RotationValue = Quaternion.Euler(EulerRotation);
        }
        else if (rotationType == RotationType.XAxis20)
        {
            EulerRotation = new Vector3(-25, 0, 0);
            RotationValue = Quaternion.Euler(EulerRotation);
        }
        else if (rotationType == RotationType.YAxis90ZAxis90)
        {
            EulerRotation = new Vector3(0, 90, -90);
            RotationValue = Quaternion.Euler(EulerRotation);
        }
    }
}
