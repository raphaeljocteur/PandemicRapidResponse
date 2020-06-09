using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode2 : MonoBehaviour
{

    private Action onTimeRunOut;

    [System.Serializable]
    private class Dice
    {
        public RESSOURCES ressource;
        public bool isLocked;
        public void Roll(System.Random rnd)
        {
            ressource = (RESSOURCES)rnd.Next(0, (int)RESSOURCES.COUNT);
        }
    }

    #region Enum

    private enum RESSOURCES
    {
        WATER,
        FOOD,
        ENERGY,
        DRUG,
        VACCINE,
        PLANE,
        COUNT
    }

    private enum ROOMS
    {
        CARGO_BAY,
        RECYCLING_CENTER,
        FOOD_ROOM,
        WATER_ROOM,
        ENERGY_ROOM,
        DRUG_ROOM,
        VACCINE_ROOM,
        COUNT
    }

    private enum CHARACTER
    {
        ANALYST,
        DIRECTOR,
        ENGINEER,
        RECYCLER,
        SPECIALIST,
        COUNT
    }

    private enum CITIES
    {
        SYDNEY,
        MANILA,
        TOKYO,
        SEOUL,
        HONG_KONG,
        BANGKOK,
        DELHI,
        KARACHI,
        RIYADH,
        CAIRO,
        ISTANBUL,
        MOSKOW,
        ESSEN,
        PARIS,
        LONDON,
        MADRID,
        MONTREAL,
        ATLANTA,
        LOS_ANGELES,
        MEXICO,
        BOGOTA,
        SAO_PAULO,
        LAGOS,
        JOHANNESBURG,
        COUNT
    }

    #endregion

    #region Inspector Variables
    [SerializeField] private int m_waterMax = 4;
    [SerializeField] private int m_foodMax = 4;
    [SerializeField] private int m_energyMax = 4;
    [SerializeField] private int m_drugMax = 4;
    [SerializeField] private int m_vaccineMax = 4;
    [SerializeField] private int m_trashMax = 10;
    [Tooltip("Time token value between 10 and 180 seconds.")]
    [Range(10.0f, 180.0f)]
    [SerializeField]  
    private float m_timeTokenValue = 120.0f;

    [Tooltip("Player who want to play (between 2 and 4)")]
    [Range(2, 4)]
    [SerializeField]
    private int m_nbPlayers = 2;

    #endregion

    #region internal Variables
    private bool m_gameOver;
    private bool m_win;
    private int m_planePosition;
    private int m_water;
    private int m_food;
    private int m_energy;
    private int m_drug;
    private int m_vaccine;
    private int m_trash;
    private int m_usableTimeToken;
    private int m_reservedTimeToken;
    private int m_currentPlayer;
    private bool m_hasRoll = false;
    private float m_time;
    private List<CHARACTER> m_characterDeck;
    private List<CHARACTER> m_players;
    private List<ROOMS> m_playersPosition;
    private List<Dice> m_dices;
    private List<int> m_citiesBox;
    private List<int> m_citiesDeck;
    private List<int> m_citiesGoal;
    private List<int> m_deposit;
    private static GameMode2 m_instance;
    private System.Random m_rnd;

    #endregion

    public void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        m_instance.Init();
    }

    private void CheckWinCondition()
    {
        //Check if we delivered every Cities
        if (m_citiesDeck.Count == 0 && m_citiesGoal.Count == 0) m_win = true;
    }

    private void CheckGameOverCondition()
    {
        //no more token and time
        if (m_usableTimeToken == 0 && m_time < 0) m_gameOver = true;
        //too much trash
        if (m_trash > m_trashMax) m_gameOver = true;
    }

    //Check starting position of my character
    private ROOMS GetStartingPosition(CHARACTER character)
    {
        switch(character)
        {
            case CHARACTER.ANALYST:
                return ROOMS.RECYCLING_CENTER;
            case CHARACTER.DIRECTOR:
                return ROOMS.FOOD_ROOM;
            case CHARACTER.ENGINEER:
                return ROOMS.ENERGY_ROOM;
            case CHARACTER.RECYCLER:
                return ROOMS.CARGO_BAY;
            case CHARACTER.SPECIALIST:
                return ROOMS.DRUG_ROOM;
            default:
                return ROOMS.CARGO_BAY;
        }
    }

    private void Update()
    {
        CheckGameOverCondition();
        CheckWinCondition();

        //Stop if the game has ended
        if (m_gameOver || m_win) return;

        //Time passes
        m_time -= Time.deltaTime;
        if(m_time < 0.0f) TimeRunOut();

        if (m_hasRoll == false)
        {
            //GetAllAvailables dices
            var dices = m_dices.Skip(6*m_currentPlayer).Take(6).Where(s => s.isLocked == false).ToList();
            RollDices(dices);
            m_hasRoll = true;
        }
    }

    private void RollDices(List<Dice> diceToRoll)
    {
        foreach(Dice dice in diceToRoll)
        {
            dice.Roll(m_rnd);
        }
    }

    private void Init()
    {
        //initialise a random variable for shuffling deck
        m_rnd = new System.Random();

        //Create all our dices needed
        m_dices = new List<Dice>();
        for (int i = 0; i < 24; i++) m_dices.Add(new Dice());

        //Reset attributes values
        m_currentPlayer = 0;
        m_gameOver = false;
        m_win = false;
        m_usableTimeToken = 3;
        m_reservedTimeToken = 6;
        m_time = m_timeTokenValue;
        m_trash = 0;
        m_water = m_waterMax;
        m_food = m_foodMax;
        m_energy = m_energyMax;
        m_drug = m_drugMax;
        m_vaccine = m_vaccineMax;

        //Create our city deck
        m_citiesBox = new List<int>();
        for (int i = 0; i < (int)CITIES.COUNT; i++) m_citiesBox.Add(i);
        //Shuffle it 
        m_citiesBox = m_citiesBox.OrderBy(x => m_rnd.Next()).ToList();
        //Draw 1 card to place plane
        m_planePosition = m_citiesBox[0];
        m_citiesBox.RemoveAt(0);
        //Draw 2 card for cities goal 
        m_citiesGoal = m_citiesBox.Take(2).ToList();
        m_citiesBox.RemoveRange(0, 2);
        //Draw 3 card for cities deck
        m_citiesDeck = m_citiesBox.Take(3).ToList();
        m_citiesBox.RemoveRange(0, 3);

        //Create our Character deck 
        m_characterDeck = new List<CHARACTER>();
        for (int i = 0; i < (int)CHARACTER.COUNT; i++) m_characterDeck.Add((CHARACTER)i);
        //Shuffle it
        m_characterDeck = m_characterDeck.OrderBy(x => m_rnd.Next()).ToList();
        //Draw as many card as character
        m_players = m_characterDeck.Take(m_nbPlayers).ToList();
        //Set their positions
        m_playersPosition = new List<ROOMS>();
        for (int i = 0; i < m_players.Count; i++) m_playersPosition.Add(GetStartingPosition(m_players[i]));
    }

    //TimeRunOut procedure
    private void TimeRunOut()
    {
        m_usableTimeToken--;
        m_time = m_timeTokenValue;
        m_citiesGoal.Add(m_citiesDeck[0]);
        m_citiesDeck.RemoveAt(0);

        onTimeRunOut?.Invoke();
    }

}
