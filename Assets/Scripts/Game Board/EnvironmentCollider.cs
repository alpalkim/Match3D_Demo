using UnityEngine;

public class EnvironmentCollider : MonoBehaviour
{
 
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ToyPiece>(out ToyPiece toyPiece) && !collision.gameObject.CompareTag(Tags.PickedToy))
        {
            toyPiece.toyMovement.ThrowToyToPool(GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInGenerationPool(), delegate { toyPiece.toyMovement.ThrowToyToPool(GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInPool()); });
        }
    }
}
