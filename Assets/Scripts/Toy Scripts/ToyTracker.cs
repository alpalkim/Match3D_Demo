using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyTracker : MonoBehaviour, IToyTracker
{
    ToyManager toyManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] PlatformManager platformManager;
    ToyPiece _expectedToy;
    ToyPair _expectedPair;

    public ToyPair MatchedPair { get; set; }

    private void Awake()
    {
        platformManager.OnToysMatched += OnToysMatched;
    }

    private void Start()
    {
        toyManager = GameReferenceHandler.instance.ToyManager;
    }


    private void OnToysMatched()
    {
        RemoveToyPairFromPool(MatchedPair);
        ResetTracking();
        scoreManager.ChangeScoreValue();
    }


    private void RemoveToyPairFromPool(ToyPair pair)
    {
        int index = pair.leftToyPiece.InstantiationIndex;
        toyManager.ToyPool.Remove(pair);
        if (index == toyManager.ToyPool.Count)
        {
            return;
        }

        for (int i = index; i < toyManager.ToyPool.Count; i++)
        {
            toyManager.ToyPool[i].leftToyPiece.DecrementIndex();
            toyManager.ToyPool[i].rightToyPiece.DecrementIndex();
            toyManager.ToyPool[i].Index--;
        }
    }

    public void SetPlacedToy(ToyPiece toy)
    {
        _expectedPair = toyManager.ToyPool[toy.InstantiationIndex];
        FindExpectedToy(toy, _expectedPair);
        if (!_expectedToy) return;
        _expectedToy.toyMovement.ScaleHitBoxSize();
    }

    public void ResetTracking()
    {
        if (_expectedToy)
        {
            _expectedToy.toyMovement.ResetHitBoxSize();
            _expectedToy = null;
        }
    }

    public void SetMatchedPair(ToyPiece piece)
    {
        ToyPair pair = toyManager.ToyPool[piece.InstantiationIndex];

        MatchedPair = pair;
    }


    void FindExpectedToy(ToyPiece toy, ToyPair toyPair)
    {
        if (toy.ToyPosition == PlatformPositions.Left)
        {
            _expectedToy = toyPair.rightToyPiece;
        }
        else
        {
            _expectedToy = toyPair.leftToyPiece;
        }
    }
}