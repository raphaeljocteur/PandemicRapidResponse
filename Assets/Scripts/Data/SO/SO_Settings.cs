using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_Settings", order = 1)]
public class SO_Settings : ScriptableObject
{
    [SerializeField] int m_nbPlayers;
    [SerializeField] int m_maxPlayer = 4;
    [SerializeField] int m_minPlayer = 2;

    public int NbPlayers 
    { 
        get => m_nbPlayers;
        set => m_nbPlayers = Mathf.Clamp(value, m_minPlayer, m_maxPlayer); 
    }
}