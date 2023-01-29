using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Playing,
    Shopping,
    Waiting,
}

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private GameStates _currentGameState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameplayEvents.Instance.OnAddingGroundItem += SetShoppingState;
        GameplayEvents.Instance.OnCancelAction += SetPlayingState;
    }

    private void SetShoppingState(Transform item)
    {
        _currentGameState = GameStates.Shopping;
    }

    private void SetPlayingState()
    {
        _currentGameState = GameStates.Playing;
    }

    public GameStates Current()
    {
        return _currentGameState;
    }

    public bool IsPlaying()
    {
        return _currentGameState == GameStates.Playing;
    }
}
