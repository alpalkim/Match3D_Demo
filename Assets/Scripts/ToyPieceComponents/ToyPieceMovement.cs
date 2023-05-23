using UnityEngine;
using DG.Tweening;
using System;

public class ToyPieceMovement : MonoBehaviour
{
    Vector3[] directions = new Vector3[2];
    ToyRotationHandler rotationHandler;
    ToyPiece thisPiece;
    Rigidbody _rigidbody;
    MeshCollider _collider;
    BoxCollider _boxCollider;

    public bool IsFollowing => isFollowing;
    bool isFollowing;

    public bool ToyOnPlatform => _toyOnPlatform;
    bool _toyOnPlatform;

    Vector3 colliderStartSize;
    Vector3 scaledSize;

    public ToyTweenDurations durations = new ToyTweenDurations()
    {
        pickUpDuration = 0.08f,
        rotationDuration = 0.17f,
        controlDuration = 0.01f,
        platformLandingDuration = 0.05f,
        poolThrowDuration = 0.3f,
        platformThrowDuration = 0.25f

        
    };

    void Awake()
    {
        rotationHandler = GetComponent<ToyRotationHandler>();
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<MeshCollider>();
        thisPiece = GetComponent<ToyPiece>();

        _collider.convex = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        _rigidbody.mass = 10f;

        colliderStartSize = _boxCollider.size;
        scaledSize = colliderStartSize * 2f;
    }

    private void Start()
    {
        Vector3 direction1 = Vector3.back;
        Vector3 direction2 = Vector3.forward;
        directions[0] = direction1;
        directions[1] = direction2;
    }
    
    public void DisableRigidbody()
    {
        _rigidbody.isKinematic = true;
    }

    public void EnableRigidbody()
    {
        _rigidbody.isKinematic = false;
        
    }


    /// <summary>
    /// Tweens the toy to the pickup position
    /// </summary>
    /// <param name="pickupPosition"></param>
    public void PickupToy(Vector3 pickupPosition)
    {
        SetToyControlState(true); //Collider Kapanıyor, rigidbody kinematic oluyor. 
        
        transform.DOMove(pickupPosition , durations.pickUpDuration).SetEase(Ease.Linear).OnComplete(delegate { isFollowing = true; });
    }
    
    /// <summary>
    /// Tweens the toy each frame to handle movement
    /// </summary>
    /// <param name="movePosition"></param>
    public void FollowPlayer(Vector3 movePosition)
    {
        if (!IsFollowing) { return; }
        _rigidbody.DOMove(movePosition, durations.controlDuration).SetEase(Ease.OutQuart);
    }

    public void SuckObjectToPlatform()
    {
        transform.DOKill();
        SetToyControlState(true);
        isFollowing = false;
        if (ToyOnPlatform) { _toyOnPlatform = false; }
    }
    
    public void ThrowToy(Vector3 Throwforce)
    {
        transform.DOKill();
        SetToyControlState(false);
        _rigidbody.AddForce(Throwforce, ForceMode.Impulse);
        
        isFollowing = false;
        
        if (ToyOnPlatform) { _toyOnPlatform = false;}
    }
 

    /// <summary>
    /// Places the toy to the given landing platform and fills the platform
    /// </summary>
    /// <param name="platform"></param>
    public void PlaceToyOnPlatform(LandingPlatform platform)
    {
        _rigidbody.DORotate(rotationHandler.EulerRotation, 0.4f);
        _rigidbody.DOMove(platform.landingPosition, durations.platformLandingDuration);
        platform.FillPlatform(thisPiece);
        _toyOnPlatform = true;
        _collider.enabled = true;
    }


    public void PlaceToyOnPlatform(LandingPlatform platform, Action onCompleteMethod)
    {
        _rigidbody.DORotate(rotationHandler.EulerRotation, 0.4f);
        _rigidbody.DOMove(platform.landingPosition, durations.platformLandingDuration).SetEase(Ease.Linear).OnComplete(delegate { onCompleteMethod(); });
        platform.FillPlatform(thisPiece);
        _toyOnPlatform = true;
        _collider.enabled = true;
    }

    /// <summary>
    /// Throws the toy with tween to the given point in the pool 
    /// </summary>
    /// <param name="poolThrowPoint"></param>
    public void ThrowToyToPool(Vector3 poolThrowPoint)
    {
        _rigidbody.DOMove(poolThrowPoint, durations.poolThrowDuration).SetEase(Ease.Linear).OnComplete(() => ThrowToy(Vector3.zero));
    }

    public void ThrowToyToPool(Vector3 poolThrowPoint, Action onComplete)
    {
        _rigidbody.DOMove(poolThrowPoint, durations.poolThrowDuration).SetEase(Ease.Linear).OnComplete(delegate { ThrowToy(Vector3.zero); onComplete(); });
    }

    /// <summary>
    /// Throws the toy to the given platform and invokes the given method after the toy has landed on to platform
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="onCompleteMethod"></param>
    public void ThrowToyToPlatform(LandingPlatform platform, Action onCompleteMethod)
    {
        SetToyControlState(true);
        transform.DORotateQuaternion(Quaternion.identity, 0.1f).SetEase(Ease.InExpo);
        _rigidbody.DOMove(platform.landingPosition, durations.platformThrowDuration).SetEase(Ease.Linear).OnComplete(delegate 
        {
            _rigidbody.DOMove(platform.landingPosition, durations.platformLandingDuration);
            _toyOnPlatform = true;
            _collider.enabled = true;
            onCompleteMethod();
        });

    }


    /// <summary>
    /// Turns off the collider and turns on the rigidbody kinematic if the toy is no longer controlled reversed for true
    /// </summary>
    /// <param name="isControlled"></param>
    public void SetToyControlState(bool isControlled)
    {
        if (isControlled)
        {
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }
        else
        {
            _rigidbody.isKinematic = false;
            _collider.enabled = true; 
        }
    }


    public void ScaleHitBoxSize()
    {
        _boxCollider.size = scaledSize;


    }

    public void ResetHitBoxSize()
    {
        _boxCollider.size = colliderStartSize;
    }

    public void SetHitboxCollider(bool isEnabled)
    {
        _boxCollider.enabled = isEnabled;
    }


    public void AddRandomDirectionForce()
    {
        int randomDirection = UnityEngine.Random.Range(0, 2);

        _rigidbody.AddForce(directions[randomDirection] * 90f, ForceMode.VelocityChange);
    }


    public void OnDestroy()
    {
        _rigidbody.DOKill();
    }

}



