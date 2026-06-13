using UnityEngine;

[CreateAssetMenu(fileName = "BrickStats", menuName = "Scriptable Objects/BrickStats")]
public class BrickStats : ScriptableObject
{
    public float MaxHealth;
    public float SlowAmount;
    public int CoinValue;
}
