using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_RoomList", order = 1)]
public class SO_RoomList : ScriptableObject
{
    [SerializeField] List<SO_Room> m_list;
    public List<SO_Room> List { get => m_list; }
}
