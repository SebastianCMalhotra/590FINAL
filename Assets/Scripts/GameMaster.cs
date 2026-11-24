using UnityEngine;
using System.Collections;
using System.Threading;

public class GameMaster : MonoBehaviour
{

    public int PlayerInitialHealth= 5;
    private int PlayerCurrentHealth = 0;
    public int InitialTimeLeft = 360;
    private int CurrentTimeLeft = 0;
    public float PlayerInitialLanternFuel = 10.0f;
    private float PlayerCurrentLanternFuel = 0.0f;
    public float PlayerColdnessLevel = 0.0f; // will always start at 0 (not cold)
    
    [Range(0.0f, 1.0f)] // these are cool you can control them in the unity inspector so they can only be editted to these values.
    public float AggressionLevel = 0.0f;
    


    private bool GameBeaten = false;
    private Coroutine timerCoroutine; // acts as our timer that goes every second
    private IEnumerator OneSecondLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            OnOneSecond();
        }
    }

    private void OnOneSecond()
    {
        
        CurrentTimeLeft--;
        if (CurrentTimeLeft <= 0) 
        { 
            GameBeaten = true;
            WinScenario();
            StopTimer();
        }
    }
    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    /// <summary>
    /// This is for the game starting, aka everything here runs when we want the game to actually start
    /// </summary>
    void OnGameStart()
    {
        PlayerCurrentHealth = PlayerInitialHealth;
        CurrentTimeLeft = InitialTimeLeft;
        PlayerCurrentLanternFuel = PlayerInitialLanternFuel;
        PlayerColdnessLevel = 0.0f;
    }

    void WinScenario()
    {
        // code here runs once when you win the game
    }

    void LossScenario()
    {
        // code here runs once when you lose the game
    }

}
