using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

//gambehavior inherits from monobehavior and IManager. GameBehavior must have the IManager elements to compile
public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;

    public string State
    {
        get {return _state;}
        set {_state = value;}
    }
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;

    private int _itemsCollected = 0;
    public int Items
    {
        //calls when accessed
        get { return _itemsCollected; }
        //calls when updated
        set {
            _itemsCollected = value;
            Debug.LogFormat("Items: {0}", _itemsCollected);
            if(_itemsCollected >= maxItems)
            {
                endGame(true);
            }
            else
            {
                labelText = "item found, only " + (maxItems - _itemsCollected) + " more to go!";
            }
        }
    }
    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
            if (_playerHP <= 0)
            {
                endGame(false);
            }
            else
            {
                labelText = "AHHH!!! MY BONES!!!";
            }
        }
    }
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Manager initializedd...";
        Debug.Log(_state);

        _state.FancyDebug();
    }

    private void endGame(bool win)
    {
        if (win)
        {
            labelText = "You've found all the items!";
            showWinScreen = true;

            //stops the game
            Time.timeScale = 0f;
        }
        else
        {
            labelText = "You wnat another life with that?";
            showLossScreen = true;
            Time.timeScale = 0;
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"))
            {
                //calling our static class
                Utilities.RestartLevel(0);
            }
        }
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose"))
            {
                Utilities.RestartLevel();
            }
        }
    }
}
