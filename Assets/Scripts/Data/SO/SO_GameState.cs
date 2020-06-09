using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_GameState", order = 1)]
public class SO_GameState : ScriptableObject
{
    [SerializeField] SO_DiceList m_diceList;
    [SerializeField] SO_Cities m_cityList;
    [SerializeField] SO_RoomList m_roomList;
    [SerializeField] SO_CharacterList m_characterList;
    [SerializeField] SO_Room m_trashRoom;

    [SerializeField] int m_nbPlayers;
    [SerializeField] int m_startingPlayer;
    public int m_currentPlayer;
    [SerializeField] int m_maxPlayer = 4;
    [SerializeField] int m_minPlayer = 2;

    public SO_Plane plane;
    public bool isGameOver;
    public bool isWin;
    
    public bool isActionPass;
    public bool isActionRoll;
    public bool isActionMove;
    public bool isActionMovePlaneForward;
    public bool isActionMovePlaneBackward;
    public bool isActionUseRoom;
    public bool isActionDepositRoom;

    public float time;
    public int usableTimeToken;
    public int trashMax = 10;
    public int reservedTimeToken;

    [SerializeField] float m_timeTokenValue = 120.0f;

    public List<SO_Dice> dices;

    public List<SO_City> cityRemnant;
    public List<SO_City> cityGoal;
    public List<SO_City> cityDeck;

    public List<SO_Character> players;
    public List<SO_Room> playersPosition;

    public int NbPlayers
    {
        get => m_nbPlayers;
        set => m_nbPlayers = Mathf.Clamp(value, m_minPlayer, m_maxPlayer);
    }

    public SO_Cities CityList { get => m_cityList; }
    public SO_CharacterList CharacterList { get => m_characterList;}
    public int StartingPlayer { get => m_startingPlayer; }
    public SO_RoomList RoomList { get => m_roomList; set => m_roomList = value; }
    public float TimeTokenValue { get => m_timeTokenValue; }
    public SO_Room TrashRoom { get => m_trashRoom; }
    public SO_DiceList DiceList { get => m_diceList; }
}
