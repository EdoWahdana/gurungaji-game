using UnityEngine;

[System.Serializable]
public class StatusLevelUser
{
    public string level1,level2,level3,level4,level5,level6,level7;

    public static StatusLevelUser CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<StatusLevelUser>(jsonString);
    }
}