using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(ToyPieceMovement))]
[DisallowMultipleComponent]
public class ToyPiece : MonoBehaviour
{
    public ToyPieceMovement toyMovement;

    private WaitForSeconds tagWaitTime = new WaitForSeconds(1);

    public void SetToyTypeKey(int key) => _toyTypeKey = key;
    public int ToyTypeKey => _toyTypeKey;
    int _toyTypeKey;

    public IEnumerator SetTagToUntagged()
    {
        yield return tagWaitTime;
        gameObject.tag = "Untagged";
    }
    public void SetInstantiationIndex(int index) => _instantiationIndex = index;
    public void DecrementIndex() => _instantiationIndex--;
    public int InstantiationIndex => _instantiationIndex;
    int _instantiationIndex;

    public void SetToyPosition(PlatformPositions position) => _toyPosition = position;
    public PlatformPositions ToyPosition { get { return _toyPosition; } }
    public PlatformPositions _toyPosition;
    
    public bool IsOnPlatform => toyMovement.ToyOnPlatform;


    private void Awake()
    {
        toyMovement = GetComponent<ToyPieceMovement>();
    }

   



}