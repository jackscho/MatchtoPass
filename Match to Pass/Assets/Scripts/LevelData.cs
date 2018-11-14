using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public string headSpriteLocation;
    public float timeLimitInSeconds;
    public ColorSlotRowData[] colorSlotRows;
    public Color[] userColorOptions;
    public Color bombColor;
}