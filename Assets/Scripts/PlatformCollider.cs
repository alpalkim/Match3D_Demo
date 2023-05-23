using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    [SerializeField] PlatformManager platformManager;
    [SerializeField] ToyPhysics toyPhysics;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PickedToy))
        {
            ToyPiece piece = other.GetComponent<ToyPiece>();
            other.gameObject.tag = "Untagged";
            toyPhysics.SuckObjectToLit(piece);
            GameReferenceHandler.instance.ToySelector.DeselectToy();
            platformManager.ToyDroppedOnPlatform(piece);
        }
    }
}
