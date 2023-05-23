using UnityEngine;

public class ToyPhysics : MonoBehaviour
{
    ToySelector toySelector;
    [SerializeField] PlatformManager platformManager;

    private Vector3 startPos, endPos, deltaPos, memorizedPos;
    private bool throwObject;

    [SerializeField] private float _maxForce, currentForce;
    [SerializeField] private float _forceChanger;
    [SerializeField] private float _forceDecreaser; 
    [SerializeField] private float forceIncreaser;

    private bool isDragging;

    private void Start()
    {
        toySelector = GameReferenceHandler.instance.ToySelector;
    }
    
    private void Update()
    {
        
#if UNITY_EDITOR
            ThrowingForceCalculatorPC(toySelector.SelectedToy);
#else
            ThrowingForceCalculatorMobile(toySelector.SelectedToy);
#endif
            ControlToy(toySelector.SelectedToy);
    }

    public void SuckObjectToLit(ToyPiece toy)
    {
        toy.toyMovement.SuckObjectToPlatform();
    }

    private Vector3 RaycastHitPos()
    {
        if (GameReferenceHandler.instance.Raycaster.CheckRaycastPlane(out float hit, out Vector3 hitPos))
        {
            return hitPos;
        }
        return Vector3.zero;
    }

    private void ChangeCurrentForce(float magnitude)
    {
        if (magnitude < _forceChanger && currentForce>= 0)
        {
            currentForce -= _forceDecreaser;
        }
        else
        {
            if (currentForce <= _maxForce)
            {
                currentForce += forceIncreaser;
            }
        }
    }

    private void ThrowingForceCalculatorPC(ToyPiece toy)
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            GrabToy();
            currentForce = _maxForce;
            startPos = RaycastHitPos();
        }

        if (Input.GetMouseButton(0))
        {
            endPos = RaycastHitPos();
            memorizedPos = startPos;
            deltaPos = endPos - memorizedPos;
            startPos = RaycastHitPos();
            
            ChangeCurrentForce(deltaPos.magnitude);
        }
        
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (currentForce < 0)
            {
                currentForce = 0;
            }

            if (toySelector.SelectedToy)
            {
                if (toySelector.SelectedToy.IsOnPlatform)
                {
                    toySelector.SelectedToy.toyMovement.SetHitboxCollider(false);
                    toySelector.SelectedToy.toyMovement.ThrowToyToPool(GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInPool(), () => toySelector.SelectedToy.toyMovement.SetHitboxCollider(true));
                    toySelector.DeselectToy();
                }
                else
                {
                    toy?.toyMovement?.ThrowToy(deltaPos.normalized * currentForce);
                    toySelector.DeselectToy();
                }
            }
        }
        
    }
    
    private void ThrowingForceCalculatorMobile(ToyPiece toy)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                GrabToy();
                currentForce = _maxForce;
                startPos = RaycastHitPos();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                endPos = RaycastHitPos();
                memorizedPos = startPos;
                deltaPos = endPos - memorizedPos;
                startPos = RaycastHitPos();

                ChangeCurrentForce(deltaPos.magnitude);
            }


            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                if (currentForce < 0)
                {
                    currentForce = 0;
                }

                if (toySelector.SelectedToy)
                {
                    if (toySelector.SelectedToy.IsOnPlatform)
                    {
                        toySelector.SelectedToy.toyMovement.SetHitboxCollider(false);
                        toySelector.SelectedToy.toyMovement.ThrowToyToPool(GameReferenceHandler.instance.ToyPositionCheckerSo.GetRandomPositionInPool(), () => toySelector.SelectedToy.toyMovement.SetHitboxCollider(true));
                        toySelector.DeselectToy();
                    }
                    else
                    {
                        toy?.toyMovement?.ThrowToy(deltaPos.normalized * currentForce);
                        toySelector.DeselectToy();
                    }
                }
            }
            
        }
        

    }

    public void GrabToy()
    {
        toySelector.OnScreenClick();

        if(!toySelector.SelectedToy) { return; }

        if (GameReferenceHandler.instance.Raycaster.CheckRaycastPlane(out float hitPoint, out Vector3 hitpos))
        {
            toySelector.SelectedToy.toyMovement.PickupToy(hitpos);
        }

        platformManager.OnToyGrabed(toySelector.SelectedToy);
    }
    
    

    /// <summary>
    /// Drags the object to the raycast hit position each frame
    /// </summary>
    /// <param name="toy"></param>
    public void ControlToy(ToyPiece toy)
    {
        
        if (isDragging && toy) //Pick and follow
        {
            toySelector.SelectedToy.gameObject.tag = "PickedToy";
            
            if (GameReferenceHandler.instance.Raycaster.CheckRaycastPlane(out float hit, out Vector3 mousePos))
            {
                if(toy.toyMovement.IsFollowing)
                {
                    mousePos = GameReferenceHandler.instance.ToyPositionCheckerSo.ClampToyPosition(mousePos);
                    toy.toyMovement.FollowPlayer(mousePos);
                }
            } 
        }
    }

    private void ChangeToyTag()
    {
        ToyPiece lastPiece = toySelector.SelectedToy;

        lastPiece.gameObject.tag = "Untagged";
    }
}
