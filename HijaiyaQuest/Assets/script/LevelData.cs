using UnityEngine;

[CreateAssetMenu(fileName = "Level Baru", menuName = "Data Soal/Level")]
public class LevelData : ScriptableObject
{
    public int levelIndex;               // contoh: 0 = Level 1
    public SoalData[] daftarSoal;
}