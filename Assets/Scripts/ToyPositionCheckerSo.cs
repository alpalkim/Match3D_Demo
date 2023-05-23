using UnityEngine;

[CreateAssetMenu(fileName = "Toy Position checker", menuName = "Toy Position Checker")]
public class ToyPositionChecker : ScriptableObject
{
    Collider _toyPoolCollider;
    Collider _toyGenerationCollider;
    Collider _storageCollider;

    Vector3 storagePoolMin;
    Vector3 storagePoolMax;

    Vector3 movementPoolMin;
    Vector3 movementPoolMax;
    Vector3 movementPoolCenter;


    Vector3 _toyGenerationMin;
    Vector3 _toyGenerationMax;

    
    public void Init(Collider tpc, Collider tgc, Collider sc)
    {
        _toyPoolCollider = tpc;
        _toyGenerationCollider = tgc;
        _storageCollider = sc;
        
        SetMinMaxPoints();
    }

    private void SetMinMaxPoints()
    {
        movementPoolMax = _toyPoolCollider.bounds.max;
        movementPoolMin = _toyPoolCollider.bounds.min;
        movementPoolCenter = _toyPoolCollider.bounds.center;

        _toyGenerationMin = _toyGenerationCollider.bounds.min;
        _toyGenerationMax = _toyGenerationCollider.bounds.max;

        storagePoolMin = _storageCollider.bounds.min;
        storagePoolMax = _storageCollider.bounds.max;
    }

    public bool CheckPositionInsidePool(Vector3 pos)
    {
        pos.y = movementPoolCenter.y;

        if (_toyPoolCollider.bounds.Contains(pos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 ClampToyPosition(Vector3 pos)
    {
        if (CheckPositionInsidePool(pos))
        {
            return pos;
        }
        else
        {
            float x = Mathf.Clamp(pos.x, movementPoolMin.x, movementPoolMax.x);
            float z = Mathf.Clamp(pos.z, movementPoolMin.z, movementPoolMax.z);

            return new Vector3(x, pos.y, z);
        }
    }

    public Vector3 GetRandomPositionInPool()
    {
        float x = Random.Range(storagePoolMin.x, storagePoolMax.x);
        float y = Random.Range(storagePoolMin.y, storagePoolMax.y);
        float z = Random.Range(storagePoolMin.z, storagePoolMax.z);

        return new Vector3(x, y, z);
    }

    public Vector3 GetRandomPositionInGenerationPool()
    {
        float x = Random.Range(_toyGenerationMin.x, _toyGenerationMax.x);
        float y = Random.Range(_toyGenerationMin.y, _toyGenerationMax.y);
        float z = Random.Range(_toyGenerationMin.z, _toyGenerationMax.z);

        return new Vector3(x, y, z);
    }

  
}
