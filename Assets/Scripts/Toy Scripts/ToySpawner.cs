using System.Collections.Generic;
using UnityEngine;

public class ToySpawner
{
    Vector3 pos1 => GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInGenerationPool();
    Vector3 pos2 => GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInGenerationPool();

    Transform toyParent;

    public ToySpawner(Transform toyParent)
    {
        this.toyParent = toyParent;
    }



    /// <summary>
    /// Takes in a toyPool and instantiates toyCount number of random toys into random positions in the toy pool
    /// </summary>
    /// <param name="toyPool"></param>
    /// <param name="toyCount"></param>
    /// <returns></returns>
    public List<ToyPair> GenerateToys(Dictionary<int, ToyPair> toyPool, int toyCount)
    {
        List<ToyPair> instantiatedToyPairs = new List<ToyPair>();

        Dictionary<int, ToyPair> randomPool = Tools.GenerateRandomToyPool(toyPool, toyCount, toyPool.Count - 1);

        int index = 0;
        foreach (var pair in randomPool)
        {
            ToyPair toyPair = InstantiateToyPair(pair.Value, pair.Key, index, pos1, pos2);
            toyPair.Index = index;
            instantiatedToyPairs.Add(toyPair);
            index++;
        }

        return instantiatedToyPairs;

    }


    ToyPair InstantiateToyPair(ToyPair toyPair, int key, int index, Vector3 pos1, Vector3 pos2)
    {
        ToyPair generatedPair = new ToyPair();
        generatedPair.leftToyPiece = InstantiatePiece(toyPair.leftToyPiece, key, index, pos1, PlatformPositions.Left);
        
        generatedPair.rightToyPiece = InstantiatePiece(toyPair.rightToyPiece, key, index, pos2, PlatformPositions.Right);

        return generatedPair;
    }

    ToyPiece InstantiatePiece(ToyPiece piece, int key, int index, Vector3 position, PlatformPositions platformPosition)
    {
        ToyPiece generatedPiece = Object.Instantiate(piece, position, Quaternion.identity, toyParent);
        generatedPiece.SetToyPosition(platformPosition);
        generatedPiece.SetToyTypeKey(key);
        generatedPiece.SetInstantiationIndex(index);

        return generatedPiece;
    }

}
