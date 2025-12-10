using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RankData", menuName = "RankData/RankDataObject")]
public class RankData : ScriptableObject
{
    [Serializable]
    public class RankRule
    {
        public string rankName;
        public int minAccuracy;
        public int maxMisses;
    }

    public RankRule[] ranks;
}
