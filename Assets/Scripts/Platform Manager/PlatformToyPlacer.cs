using UnityEngine;

public class PlatformToyPlacer: MonoBehaviour
{
    
    [SerializeField] ToyTracker toyTracker;
    [SerializeField] PlatformManager platformManager;
    PlatformToyMatcher identifier = new PlatformToyMatcher();

    public void HandleToyPlacement(LandingPlatform platform, LandingPlatform oppositePlatform, ToyPiece toy)
    {
        if (platform.toyOnPlatform)
        {
            ThrowToy(platform, toy);
            
        }
        else
        {
            if (oppositePlatform.toyOnPlatform)
            {
                if (identifier.CheckToyEquality(oppositePlatform.toyOnPlatform, toy))
                {
                    MatchToys(platform, toy);


                }
                else
                {
                    ThrowToy(platform, toy);
                }
            }
            else
            {
                PlaceToy(platform, toy);
            }
        }
    }

    private void MatchToys(LandingPlatform platform, ToyPiece toy)
    {
        toy.toyMovement.PlaceToyOnPlatform(platform, delegate { platformManager.ToysMatched(); });
    }

    private void ThrowToy(LandingPlatform platform, ToyPiece toy)
    {
        toy.toyMovement.ThrowToyToPool(GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInPool());
    }

    private void PlaceToy(LandingPlatform platform, ToyPiece toy)
    {
        toy.toyMovement.PlaceToyOnPlatform(platform);
        toyTracker.SetPlacedToy(toy);
    }
}
