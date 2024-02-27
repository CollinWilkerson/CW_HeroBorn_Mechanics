using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Does not inherit because it is not in the game scene
public static class Utilities
{
    // everything inside must be static
    public static int playerDeaths = 0;

    //passes a reference to the varable instead of a copy. if the variable is modified then the value outside of the function will change
    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next time you'll be at number " + countReference;
    }
    // restart method
    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;

        Debug.Log("Player deaths: " + playerDeaths);
        //uses the ref keyword again
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log("Player deaths: " + playerDeaths);
    }
    public static bool RestartLevel(int SceneIndex)
    {
        if(SceneIndex < 0)
        {
            throw new System.ArgumentException("Scene index cannot be negative");
        }
        SceneManager.LoadScene(SceneIndex);
        Time.timeScale = 1.0f;

        return true;
    }
}
