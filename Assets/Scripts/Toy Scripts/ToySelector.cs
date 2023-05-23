using UnityEngine;

public class ToySelector
{
    LayerMask toyLayer;

    public ToySelector(LayerMask toyLayer)
    {
        this.toyLayer = toyLayer;
    }
    public ToyPiece SelectedToy => _selectedToy;

    ToyPiece _selectedToy;

    /// <summary>
    /// Responsibility = keeping selected toy 
    /// When player clicks on the screen checks if the player hit a toy object and assigns it as the SelectedToy;
    /// </summary>
    public void OnScreenClick()
    {
        if (GameReferenceHandler.instance.Raycaster.CheckRaycastHit(out RaycastHit hit, toyLayer, 5000f))
        {
            bool isToy = hit.collider.TryGetComponent(out ToyPiece toy);
            if (isToy)
            {
                SelectToy(toy);
            }
            else
            {
                DeselectToy();
            }
        }
    }

    private void SelectToy(ToyPiece toy)
    {
        _selectedToy = toy;
        toy.StopCoroutine(toy.SetTagToUntagged());
    }

    public void DeselectToy()
    {
        _selectedToy?.StartCoroutine(_selectedToy.SetTagToUntagged());
        _selectedToy = null;
    }
}
