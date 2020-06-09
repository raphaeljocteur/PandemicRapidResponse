using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_Cities", order = 1)]
public class SO_Cities : ScriptableObject
{
    [SerializeField] List<SO_City> m_list;
    public List<SO_City> List { get => m_list; }
}
