using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_DiceList", order = 1)]
public class SO_DiceList : ScriptableObject
{
    [SerializeField] List<SO_Dice> m_list;
    public List<SO_Dice> List { get => m_list; }
}