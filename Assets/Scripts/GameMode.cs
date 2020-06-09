using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    public Action onRoundStart;

    #region data
    [Header("Data")]
    [SerializeField] List<SO_City> m_cities;
    [SerializeField] List<SO_Room> m_rooms;
    [SerializeField] List<SO_Character> m_characters;
    [SerializeField] List<TimeToken> m_timeTokens;
    [SerializeField] SO_Plane plane;
    #endregion

    [Header("Prefab")]
    [SerializeField] GameObject prefabTimeToken;
    [SerializeField] GameObject prefabDices;

    [SerializeField] int m_currentPlayer;

    private List<SO_City> cityRemnant;
    private List<SO_City> cityGoal;
    private List<SO_City> cityDeck;

    private List<SO_Character> players;

    float m_time;
    float m_timeTokenValue = 120.0f;
    System.Random m_rnd;

    [SerializeField] SO_GameState m_gameState;

    private WaitForSeconds m_StartWait;

    #region Inputs
    public void OnPass()
    {
        m_gameState.isActionPass = true;
    }
    public void OnTravelForward()
    {
        m_gameState.isActionMovePlaneForward = true;
    }
    public void OnTravelBackward()
    {
        m_gameState.isActionMovePlaneBackward = true;
    }
    #endregion

    #region Conditions

    private void CheckGameOver(SO_GameState gamestate)
    {
        ////no more token and time
        //if (gamestate.usableTimeToken == 0 && gamestate.time < 0) gamestate.isGameOver = true;
        ////too much trash
        //if (gamestate.TrashRoom.Stock.Count > gamestate.trashMax) gamestate.isGameOver = true;
    }

    private void CheckWin(SO_GameState gamestate)
    {
        //if (gamestate.cityDeck.Count == 0 && gamestate.cityGoal.Count == 0) gamestate.isWin = true;
    }

    #endregion

    #region Actions
    
    private void TimeRunOut(SO_GameState gamestate)
    {
        //gamestate.usableTimeToken--;
        //gamestate.time = gamestate.TimeTokenValue;
        //gamestate.cityGoal.Add(gamestate.cityDeck[0]);
        //gamestate.cityDeck.RemoveAt(0);
    }

    private void RollDice(System.Random rnd, List<SO_Dice> dicesToRoll)
    {
        //foreach (SO_Dice dice in dicesToRoll)
        //{
        //    dice.Roll(rnd);
        //}
    }

    private void NextPlayer(SO_GameState gamestate)
    {
        //gamestate.m_currentPlayer++;
        //if (gamestate.m_currentPlayer >= gamestate.NbPlayers)
        //{
        //    gamestate.m_currentPlayer = 0;
        //}
    }

    private IEnumerator UseRoom(SO_GameState gamestate)
    {
        yield return null;
    }

    private IEnumerator WantToRoll(System.Random rnd, SO_GameState gamestate)
    {
        yield return null;
    }

    private IEnumerator WantToMove(SO_GameState gamestate)
    {
        yield return null;
    }

    private IEnumerator WantToMoveForward(SO_GameState gamestate)
    {

        //int index = gamestate.CityList.List.IndexOf(gamestate.plane.destination);
        //if (index == gamestate.CityList.List.Count - 1) index = 0;
        //else index++;

        //gamestate.plane.destination = gamestate.CityList.List[index];

        yield return null;
    }

    private IEnumerator WantToMoveBackWard(SO_GameState gamestate)
    {

        //int index = gamestate.CityList.List.IndexOf(gamestate.plane.destination);
        //if (index == 0) index = gamestate.CityList.List.Count - 1;
        //else index--;

        //gamestate.plane.destination = gamestate.CityList.List[index];

        yield return null;
    }

    private IEnumerator DepositRoom(SO_GameState gamestate)
    {
        yield return null;
    }

    #endregion

    void Start()
    {
        m_StartWait = new WaitForSeconds(1.0f);

        m_rnd = new System.Random();

        PrepareBoard();

        StartCoroutine(GameLoop(m_rnd, m_gameState));
    }

    private void PrepareBoard()
    {

        //Set variable to initialValue
        m_currentPlayer = 0;
        m_time = m_timeTokenValue;

        //Set rooms stock
        foreach (SO_Room room in gamestate.RoomList.List)
        {
            room.Stock = room.StartingStock;
        }

        //copy our Shuffle deck of cities
        var m_cities = new List<SO_City>(gamestate.CityList.List.OrderBy(x => m_rnd.Next()).ToList());

        //set plane on a city
        gamestate.plane.destination = m_cities[0];
        m_cities.RemoveAt(0);

        //draw next two card two card
        gamestate.cityGoal = m_cities.Take(2).ToList();
        m_cities.RemoveRange(0,2);

        //Draw next 3 card for cities deck
        gamestate.cityDeck = m_cities.Take(3).ToList();
        m_cities.RemoveRange(0, 3);

        //Draw next 3 card for cities deck
        gamestate.cityRemnant = m_cities;

        //Shuffle it
        var m_characters = gamestate.CharacterList.List.OrderBy(x => m_rnd.Next()).ToList();

        //Draw as many card as character
        gamestate.players = m_characters.Take(gamestate.NbPlayers).ToList();

        //Set players starting room
        for(int i =0; i <gamestate.NbPlayers; i++)
        {
            gamestate.playersPosition[i] = gamestate.players[i].StartingRoom;
        }

    }

    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop(System.Random rnd, SO_GameState gamestate) 
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting(rnd, gamestate));

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying(rnd, gamestate));

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding(gamestate));

        StartCoroutine(GameLoop(rnd, gamestate));

    }

    private IEnumerator RoundStarting(System.Random rnd, SO_GameState gamestate)
    {

        var availableDice = gamestate.dices.Skip(6 * gamestate.m_currentPlayer).Take(6).Where(s => s.m_isLock == false).ToList();

        RollDice(rnd, availableDice);

        //Invoke event the round has start
        onRoundStart?.Invoke();

        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying(System.Random rnd, SO_GameState gamestate)
    {

        while (!gamestate.isActionPass && !gamestate.isGameOver && !gamestate.isWin)
        {
            //Check GameEnd Conditions
            CheckGameOver(gamestate);
            CheckWin(gamestate);

            //If want to Roll start coroutine 
            if (gamestate.isActionRoll)
            {
                m_gameState.isActionRoll = false;
                yield return StartCoroutine(WantToRoll(rnd, gamestate));
            }

            //If want to Move start coroutine 
            if (gamestate.isActionMove)
            {
                m_gameState.isActionMove = false;
                yield return StartCoroutine(WantToMove(gamestate));
            }

            //If want to Move Plane Forward start
            if (gamestate.isActionMovePlaneForward)
            {
                m_gameState.isActionMovePlaneForward = false;
                yield return StartCoroutine(WantToMoveForward(gamestate));
            }

            //If want to Move Plane Backward start coroutine 
            if (gamestate.isActionMovePlaneBackward)
            {
                m_gameState.isActionMovePlaneBackward = false;
                yield return StartCoroutine(WantToMoveBackWard(gamestate));
            }

            //If want to use room start coroutine 
            if (gamestate.isActionUseRoom)
            {
                m_gameState.isActionUseRoom = false;
                yield return StartCoroutine(UseRoom(gamestate));
            }

            //If want to use room start coroutine 
            if (gamestate.isActionDepositRoom)
            {
                m_gameState.isActionDepositRoom = false;
                yield return StartCoroutine(DepositRoom(gamestate));
            }

            gamestate.time -= Time.deltaTime;
            if (gamestate.time < 0.0f) TimeRunOut(gamestate);

            yield return null;
        }

    }

    private IEnumerator RoundEnding(SO_GameState gamestate) 
    {

        //Select Next Player
        NextPlayer(gamestate);
        
        m_gameState.isActionPass = false;
        
        yield return null;
    }

}