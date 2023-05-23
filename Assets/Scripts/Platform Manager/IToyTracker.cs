public interface IToyTracker
{
    ToyPair MatchedPair { get; set; }
    void ResetTracking();
    void SetPlacedToy(ToyPiece toy);

}