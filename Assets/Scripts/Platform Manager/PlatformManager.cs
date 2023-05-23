using UnityEngine;
using System;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] PlatformToyPlacer toyPlacer;
    [SerializeField] ToyTracker toyTracker;
    [SerializeField] LandingPlatform leftPlatform;
    [SerializeField] LandingPlatform rightPlatform;
    public event Action OnToysMatched;

    private void Start()
    {
        leftPlatform.landingPosition = leftPlatform.platformTransform.position;
        rightPlatform.landingPosition = rightPlatform.platformTransform.position;
    }

    public void ToyDroppedOnPlatform(ToyPiece toy)
    {
        if (toy.ToyPosition == PlatformPositions.Left)
        {
            toyPlacer.HandleToyPlacement(leftPlatform, rightPlatform, toy);
        }
        else if (toy.ToyPosition == PlatformPositions.Right)
        {
            toyPlacer.HandleToyPlacement(rightPlatform, leftPlatform, toy);

        }
    }

    public LandingPlatform GetPlatform(PlatformPositions position)
    {
        if (position == PlatformPositions.Left)
        {
            return leftPlatform;
        }
        else
        {
            return rightPlatform;
        }
    }

    public void OnToyGrabed(ToyPiece toy)
    {
        if (toy.IsOnPlatform)
        {
            if (toy.ToyPosition == PlatformPositions.Left)
            {
                leftPlatform.toyOnPlatform = null;
            }
            else
            {
                rightPlatform.toyOnPlatform = null;
            }
            
            toyTracker.ResetTracking();
        }
    }

    public void ToysMatched()
    {
        toyTracker.SetMatchedPair(leftPlatform.toyOnPlatform);
        Destroy(leftPlatform.toyOnPlatform.gameObject);
        Destroy(rightPlatform.toyOnPlatform.gameObject);
        leftPlatform.ClearPlatform();
        rightPlatform.ClearPlatform();
        OnToysMatched?.Invoke();
    }
}