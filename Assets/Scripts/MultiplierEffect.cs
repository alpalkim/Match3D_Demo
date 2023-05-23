using UnityEngine;

public class MultiplierEffect
{
    public Vector3 baseRotation, firstRotation, constantRotation;
    public Vector3 baseScale,firsScale,constantScale;
    
  
    public MultiplierEffect()
    {
        baseRotation = Vector3.zero;
        firstRotation = new Vector3(0, 0, 25);
        constantRotation = Vector3.zero;
        
        baseScale = Vector3.zero;
        firsScale = Vector3.one * 1.25f;
        constantScale = Vector3.one;
    }
}
