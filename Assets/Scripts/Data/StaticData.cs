/* 
 * StaticData.cs
 * Marlow Greenan
 * 3/30/2025
 * 
 * Contains static data (player settings, current ending, etc.)
 */
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    [Header("Save")]
    private static Enums.End end = Enums.End.None;
    private static int currentLevel = 0;
    private static List<int> characterPointSaves = new List<int>();
    private static bool manualReset = true;

    [Header("Settings")]
    [Range(0, 1)] private static float volumeMaster = 1f;
    [Range(0, 1)] private static float volumeAmbient = 1f;
    [Range(0, 1)] private static float volumeSFX = 1f;


    public static float VolumeMaster { get => volumeMaster; set => volumeMaster = value; }
    public static float VolumeAmbient { get => volumeAmbient; set => volumeAmbient = value; }
    public static float VolumeSFX { get => volumeSFX; set => volumeSFX = value; }
    public static Enums.End End { get => end; set => end = value; }
    public static int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public static bool ManualReset { get => manualReset; set => manualReset = value; }
    public static List<int> CharacterPointSaves { get => characterPointSaves; set => characterPointSaves = value; }
}
