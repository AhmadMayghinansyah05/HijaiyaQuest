using UnityEngine;

[CreateAssetMenu(fileName = "Soal Baru", menuName = "Data Soal/Soal")]
public class SoalData : ScriptableObject
{
    [TextArea]
    public string soal;
    public string[] jawaban;
    public int indexJawabanBenar; // Misalnya jawaban benar ada di index ke-2
}
