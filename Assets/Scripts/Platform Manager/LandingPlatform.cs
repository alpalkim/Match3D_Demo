using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LandingPlatform
{
    public Transform platformTransform;
    [System.NonSerialized] public Vector3 landingPosition;
    public ToyPiece toyOnPlatform;

    public void FillPlatform(ToyPiece piece)
    {
        toyOnPlatform = piece;
    }

    public void ClearPlatform()
    {
        toyOnPlatform = null;
    }
}
