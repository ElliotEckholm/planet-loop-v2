using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelLevelMenu;
    public GameObject panelPauseMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;
    public GameObject[] levels;
    public static GameObject[] currentLevelObjects;

    public static bool isGameOver = false;


    private PredictionManager predictionManager;


    public static GameManager Instance { get; private set; }

    public enum State
    {
        MENU,
        LEVELMENU,
        PAUSEMENU,
        INIT,
        PLAY,
        LEVELCOMPLETED,
        LOADLEVEL,
        GAMEOVER
    }

    State _state;
    GameObject _currentLevel;
    bool _isSwitchingState;


    public static bool IsGamePaused = false;
    public static bool LevelComplete = false;
    public static bool restartClicked = false;

    // Use getter and setter when you want to update a value only when it is change
    public static int _level;

    public static int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public void PlayClicked()
    {
        SwitchState(State.LEVELMENU);
    }

    public void PauseClicked()
    {
        SwitchState(State.PAUSEMENU);
        IsGamePaused = true;
    }

    public void ResumeClicked()
    {
        panelPauseMenu.SetActive(false);
        panelPlay.SetActive(true);
        IsGamePaused = false;
    }

    public void NextLevelClicked()
    {
        Destroy(_currentLevel);
        LoadLevel();
        Level++;
        SwitchState(State.INIT);
    }

    public void MenuClicked()
    {
        // TODO: create destroyLevel func
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }
        LoadLevel();
        SwitchState(State.LEVELMENU);
    }

    public void LoadLevel()
    {
        predictionManager = GameObject.FindGameObjectWithTag("PredictionManager").GetComponent<PredictionManager>();
        SceneManager.UnloadSceneAsync(predictionManager.predicitionSceneName);
       
    }

    public void RestartClicked()
    {
        restartClicked = true;
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }

        LoadLevel();
        Reset();
        SwitchState(State.LOADLEVEL);
    }

    public void RestartFromLose()
    {
        restartClicked = true;
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }

        LoadLevel();
        Reset();
        SwitchState(State.INIT);
    }

    public void Level1Clicked()
    {
        Level = 0;
        SwitchState(State.INIT);
    }

    public void Level2Clicked()
    {
        Level = 1;
        SwitchState(State.INIT);
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }

    // Reset all variables, collisions, isPaused, isGameOver, etc
    public void Reset()
    {
        LandZoneCollider.Reset();
        WinZoneCollider.Reset();
        ShipManager.shipLanded = false;
        ShipManager.shipCollision = false;
        ShipHelper.ResetAngle();
        IsGamePaused = false;
        isGameOver = false;
        PanelPlayUI.buttonEntered = false;
        LaunchButton.launchButtonClicked = false;
        LaunchButton.launchButtonClickedFirstTime = false;
        LevelComplete = false;
        ShipManager.applyPlanetForces = true;
        MagnitudeSlider.Reset();
        Destroy(GameObject.Find("Ship"));
        LandButton.landButtonClicked = false;

    }

   

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                panelMenu.SetActive(true);
                break;
            case State.LEVELMENU:
                Cursor.visible = true;
                panelPlay.SetActive(false);
                panelLevelMenu.SetActive(true);
                break;
            case State.PAUSEMENU:
                Cursor.visible = true;
                panelPlay.SetActive(false);
                panelPauseMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = true;
                if (_currentLevel != null)
                {
                    Destroy(_currentLevel);
                }
                panelPlay.SetActive(true);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                panelPlay.SetActive(true);
                break;
            case State.LEVELCOMPLETED:
                panelPlay.SetActive(false);
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADLEVEL:
                Reset();
                currentLevelObjects = null;
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentLevel = Instantiate(levels[Level]);
                    currentLevelObjects = GameObject.FindGameObjectsWithTag("planetObject");
                }

                break;
            case State.GAMEOVER:
                panelGameOver.SetActive(true);
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.LEVELMENU:
                break;
            case State.PAUSEMENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                if (_currentLevel != null && !_isSwitchingState && LevelComplete)
                {
                    SwitchState(State.LEVELCOMPLETED);
                    Reset();
                }

                if (_currentLevel != null && !_isSwitchingState && isGameOver)
                {
                    SwitchState(State.GAMEOVER, 0.5F);
                }

                break;
            case State.GAMEOVER:
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.LEVELMENU:
                panelLevelMenu.SetActive(false);
                break;
            case State.PAUSEMENU:
                panelPauseMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                //panelPlay.SetActive(false);
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }
}