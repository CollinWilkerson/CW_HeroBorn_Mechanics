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
    //it is best practice to initalize stacks with the type they are holding
    public Stack<string> lootStack = new Stack<string>();

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
    public delegate void DebugDelegate(string newText);

    public DebugDelegate debug = Print;
    void Start()
    {
        Initialize();
        //initializing the generic we made
        InventoryList<string> inventoryList = new InventoryList<string>();
        //calling the generic function
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "Manager initialized...";

        _state.FancyDebug();

        debug(_state);

        LogWithDelegate(debug);
        //adds to the top of the stack
        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");
        //gets the player
        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        //subscribes? player jump to handle player jump
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");

    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
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
                //runs first
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                //runs on error
                catch (System.ArgumentException e)
                {
                    // 3
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " +
                    e.ToString());
                }
                //runs
                finally
                {
                    debug("Restart handled...");
                }

            }
        }
    }

    public void PrintLootReport()
    {
        //removes item from the top of the stack and returns it
        var currentItem = lootStack.Pop();
        //returns item from the stack without removing it.
        var nextItem = lootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);

        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }
}
