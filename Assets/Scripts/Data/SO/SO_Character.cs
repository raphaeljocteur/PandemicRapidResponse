using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_Character", order = 1)]
public class SO_Character : ScriptableObject
{
    [SerializeField] string m_name;
    [SerializeField] string m_profession;
    [SerializeField] string m_description;
    [SerializeField] SO_Room m_startingRoom;
    [HideInInspector] public SO_Room CurrentRoom;

    public string Name { get => m_name; }
    public SO_Room StartingRoom { get => m_startingRoom; }
    public string Profession { get => m_profession; }
    public string Description { get => m_description; }
}