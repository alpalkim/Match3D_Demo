using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ToyManager
{
    private int _toyPickupHeight = 5;
    public int ToyPickupHeight => _toyPickupHeight;
    ToySpawner spawner;
    ToyPooler toyPooler;

    public List<ToyPair> ToyPool { get; private set; } = new List<ToyPair>();

    public ToyManager(ToySpawner toySpawner, ToyPooler pooler)
    {
        spawner = toySpawner;
        toyPooler = pooler;
    }

    public void SetToyPool(int count)
    {
        ToyPool = spawner.GenerateToys(toyPooler.WholeToyPairs, count);
    }

    public void ResetToyPool()
    {
        foreach (ToyPair pair in ToyPool)
        {
            Object.Destroy(pair.leftToyPiece.gameObject);
            Object.Destroy(pair.rightToyPiece.gameObject);
        }

        ToyPool.Clear();
    }


    public void DisableToyRigidbody()
    {
        foreach (ToyPair pair in ToyPool)
        {
            pair.leftToyPiece.toyMovement.DisableRigidbody();
            pair.rightToyPiece.toyMovement.DisableRigidbody();
        }
    }

    public void EnableToyRigidbody()
    {
        foreach (ToyPair pair in ToyPool)
        {
            pair.leftToyPiece.toyMovement.EnableRigidbody();
            pair.rightToyPiece.toyMovement.EnableRigidbody();
        }
    }

}