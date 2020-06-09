using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_CharacterList", order = 1)]
public class SO_CharacterList : ScriptableObject
{
    [SerializeField] List<SO_Character> m_list;
    public List<SO_Character> List { get => m_list; }
}