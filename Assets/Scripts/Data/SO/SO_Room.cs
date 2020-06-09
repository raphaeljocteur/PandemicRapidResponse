using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/So_Room", order = 1)]
public class SO_Room : ScriptableObject
{
    [SerializeField] string m_name;
    [SerializeField] List<SO_Room> m_doors;
    [Header("Resource : ")]
    [SerializeField] List<SO_ResourceStock> m_startingStock;
    [SerializeField] List<SO_ResourceStock> m_stock;

    public List<SO_ResourceStock> Stock { get => m_stock; set => m_stock = value; }
    public List<SO_ResourceStock> StartingStock { get => m_startingStock; }
}
