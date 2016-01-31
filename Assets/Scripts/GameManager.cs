using UnityEngine;
using System.Collections;

public enum GameState { Init, Playing, Dead, Paused }

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    private static GameState _state, _pervState;

    public GameState CurrentState
    {
        get { return _state; }
        set
        {
            _pervState = _state;
            _state = value;
        }
    }

    void Awake()
    {
        _state = GameState.Playing;
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }

            return instance;
        }
    }

    private GameManager() { }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _state == GameState.Paused)
        {
            CardManager.Instance.ReadCards();
        }
    }
}
