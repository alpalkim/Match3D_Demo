using UnityEngine;

public struct PlayerDataObject
{
    public int level;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}