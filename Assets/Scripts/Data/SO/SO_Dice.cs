using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Dice", order = 1)]
public class SO_Dice : ScriptableObject
{
    public RESOURCE value;
    [SerializeField] int m_owner;
    public bool m_isLock;
    public bool m_isSelected;
    [SerializeField] SO_Room m_room;

    public int Owner { get => m_owner; }

    public void Roll(System.Random rnd)
    {
        value = (RESOURCE)rnd.Next(0, 6);
    }
}