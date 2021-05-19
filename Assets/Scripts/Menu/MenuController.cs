using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Game, Main, Option, GameOver, LevelPassed
}

public class MenuController : MonoBehaviour
{
    // Current state
    private State _activeState;

    // All the menu states
    [SerializeField] MenuState[] _menuStates;

    // State stack for switching between menus
    private Stack<State> _states = new Stack<State>();

    private void Awake()
    {
        _activeState = State.Main;
        _states.Push(_activeState);

        SwitchToAnotherState();
    }

    // Main state switch
    private void SwitchToAnotherState()
    {
        foreach (MenuState menuState in _menuStates)
        {
            if (menuState.state != _activeState)
                menuState.gameObject.SetActive(false);
            else
                menuState.gameObject.SetActive(true);
        }
    }

    // Return to the previous menu popped from the stack
    public void BackButton()
    {
        _states.Pop();
        _activeState = _states.Pop();
        if (_states.Count == 0)
        {
            _states.Push(_activeState);
        }
        SwitchToAnotherState();
    }

    public void MainButton()
    {
        _activeState = State.Main;
        _states.Push(_activeState);
        SwitchToAnotherState();
    }

    public void GameButton()
    {
        _activeState = State.Game;
        _states.Push(_activeState);
        SwitchToAnotherState();
    }

    public void OptionButton()
    {
        _activeState = State.Option;
        _states.Push(_activeState);
        SwitchToAnotherState();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
