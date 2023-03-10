using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedState
{
    New,
    Mid,
    Full
}

public class SeedProgression : MonoBehaviour
{
    [Header("Seeds Configurations")]
    [SerializeField]
    private SeedState _seedState = SeedState.New;
    [SerializeField]
    private GameObject _monster;
    [SerializeField]
    private GameObject _monsterPhaseFX;

    [SerializeField]
    private float _defaultProgressionTime = 20f;
    private float _timeRemaining;
    private bool _timerIsRunning = true;
    private Transform _currentSeed;

    private void Start()
    {
        _timeRemaining = _defaultProgressionTime;
        var seedNew = transform.GetChild(0).transform;
        seedNew.gameObject.SetActive(true);
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = _defaultProgressionTime;
                OnTimerFinished();
            }
            //float minutes = Mathf.FloorToInt(_timeRemaining / 60);
            float seconds = Mathf.FloorToInt(_timeRemaining % 60);
            //print($"Time remaining: {seconds}");
        }
    }


    private void OnTimerFinished()
    {
        switch (_seedState)
        {
            case SeedState.New:
                EvolveSeed(1);
                _seedState = SeedState.Mid;
                break;
            case SeedState.Mid:
                _seedState = SeedState.Full;
                EvolveSeed(2);
                break;
            case SeedState.Full:
                SummonMonster();
                _timerIsRunning = false;
                break;
        }
    }

    private void SummonMonster()
    {
        var positionToSummon = _currentSeed.position;
        Instantiate(_monsterPhaseFX, transform.position, Quaternion.identity);
        Instantiate(_monster, positionToSummon, Quaternion.identity);
        Destroy(_currentSeed.gameObject);
    }

    void EvolveSeed(int idx)
    {
        var olderSeed = transform.GetChild(idx - 1).transform;
        _currentSeed = transform.GetChild(idx).transform;

        olderSeed.gameObject.SetActive(false);
        _currentSeed.gameObject.SetActive(true);
    }
}
