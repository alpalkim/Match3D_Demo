using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlatformToyMatcher 
{

    public bool CheckToyEquality(ToyPiece piece1, ToyPiece piece2)
    {
        bool toyEquality;

        if (piece1.ToyTypeKey == piece2.ToyTypeKey)
        {
            toyEquality = true;
        }
        else
        {
            toyEquality = false;
        }

        return toyEquality;
    }
}
