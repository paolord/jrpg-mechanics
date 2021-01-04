using UnityEngine;

[CreateAssetMenu (fileName = "New CharacterBase", menuName = "Scriptable Objects/Character Base")]
public class CharacterBaseSO : ScriptableObject
{
    [SerializeField] public string GameClass;

    private int attack;
    private int hp;
    private int speed;

    [SerializeField] private int baseAttack;
    [SerializeField] private int baseHp;
    [SerializeField] private int baseSpeed;

    [SerializeField] private int attackGrowth;
    [SerializeField] private int hpGrowth;
    [SerializeField] private int speedGrowth;

    public void ApplyLevel(int level)
    {
        attack = baseAttack + attackGrowth * level;
        hp = baseHp + hpGrowth * level;
        speed = baseSpeed + speedGrowth * level;
    }
}
