using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public int Atk { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Distance { get; set; }
    [field: SerializeField] public int Reward { get; set; }
}
