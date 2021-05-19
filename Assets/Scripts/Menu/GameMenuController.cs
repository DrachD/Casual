using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////////
// Class game menu controller
////////////////////////////////////////////////////////
public class GameMenuController : MonoBehaviour
{
    // We store all the states of our menus
    [SerializeField] MenuState[] _menuStates;

    // If the menu exists in the array, then we manipulate this menu
    public MenuState GetMenuState(State state)
    {
        foreach (MenuState menuState in _menuStates)
        {
            if (state == menuState.state)
            {
                return menuState;
            }
        }

        return null;
    }
}
