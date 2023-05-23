using UnityEngine;

public class GameReferenceHandler : MonoBehaviour
{
    public static GameReferenceHandler instance { get; private set; }

    #region Object References
    
    [Header("RUNTIME COLLIDER REFERENCES")] 
    [SerializeField] Collider _toyPoolCollider;
    [SerializeField] Collider _toyGenerationCollider;
    [SerializeField] Collider _storageCollider;
    
    [SerializeField] Transform toyParent;
    [SerializeField] private LayerMask ToyLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ToyPooler pooler;
    #endregion

    #region Runtime References

    public Raycaster Raycaster;
    public ToyManager ToyManager;
    public ToySpawner ToySpawner;
    public ToyPositionChecker ToyPositionCheckerSo;
    public ToySelector ToySelector;

    #endregion

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        CreateReferences();
        
    }

    private void CreateReferences()
    {
        ToySpawner = new ToySpawner(toyParent);
        //
        ToyManager = new ToyManager(ToySpawner,pooler);
        //
        Raycaster = new Raycaster(mainCamera,ToyManager.ToyPickupHeight);
        //
        ToyPositionCheckerSo = ScriptableObject.CreateInstance<ToyPositionChecker>();
        ToyPositionCheckerSo.Init(_toyPoolCollider,_toyGenerationCollider,_storageCollider);
        //
        ToySelector = new ToySelector(ToyLayer);
    }
    
    
}
