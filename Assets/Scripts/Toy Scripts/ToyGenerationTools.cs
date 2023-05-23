using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGenerationTools
{
    public ToyPair InstantiateToyPair(ToyPair toyPair, int key, int index, Vector3 pos1, Vector3 pos2, Transform toyParent)
    {
        ToyPair generatedPair = new ToyPair();
        generatedPair.leftToyPiece = InstantiatePiece(toyPair.leftToyPiece, key, index, pos1, PlatformPositions.Left, toyParent);
        generatedPair.rightToyPiece = InstantiatePiece(toyPair.rightToyPiece, key, index, pos2, PlatformPositions.Right, toyParent);

        return generatedPair;
    }

    private ToyPiece InstantiatePiece(ToyPiece piece, int key, int index, Vector3 position, PlatformPositions platformPosition, Transform toyParent)
    {
        ToyPiece generatedPiece = Object.Instantiate(piece, position, Quaternion.identity,toyParent);
        generatedPiece.SetToyPosition(platformPosition);
        generatedPiece.SetToyTypeKey(key);
        generatedPiece.SetInstantiationIndex(index);

        return generatedPiece;
    }
}
