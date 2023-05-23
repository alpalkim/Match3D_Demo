using UnityEngine;
using DG.Tweening;

public class MovingImage : MonoBehaviour
{
    Vector2 targetPos;
    Vector3 startScale;
    RectTransform thisRect;

    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector3 ScaleVector = new Vector3(0.88f, 0.88f, 0.88f);
    [SerializeField] private float t = 0.3f;


    public AnimationCurve PositionCurve;

    private void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        targetPos = thisRect.position;
        thisRect.anchoredPosition = startPos;
        startScale = thisRect.localScale;
    }
    
    private void OnEnable()
    {
        MoveToPosition();
    }

    private void MoveToPosition()
    {
        
        thisRect.DOMove(targetPos, 2*t).SetEase(PositionCurve);

        thisRect.DOScale(ScaleVector,2*t).SetEase(Ease.OutExpo).OnComplete(()=> 
        
        {
            thisRect.DOScale(Vector3.one, t).SetEase(Ease.OutBounce);
        });
    }

    private void ResetPosition()
    {
        thisRect.localScale = startScale;
        thisRect.anchoredPosition = startPos;
    }

    private void OnDisable()
    {
        ResetPosition();
    }
}
