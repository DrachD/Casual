using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

////////////////////////////////////////////////////////
// level button. Activate the level button when passing the current level of the game
////////////////////////////////////////////////////////

[RequireComponent(typeof(Button))]
public class LevelButtonLocker : MonoBehaviour
{
    private Animator _anim;
    private Button _button;
    public int level = 1;
    public bool alwayUnlocked = false;
    public GameObject lockedElement;
    [SerializeField] LevelData levelData;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _anim = GetComponent<Animator>();

        if (!alwayUnlocked)
        {
            if (level <= levelData.currentLevel)
            {
                SetActiveButton(false, Selectable.Transition.ColorTint);
            }
            else
            {
                SetActiveButton(true, Selectable.Transition.None);
            }
        }
        else
        {
            SetActiveButton(false, Selectable.Transition.ColorTint);
        }
    }

    private void Start()
    {
        _anim.enabled = (level == levelData.selectedLevel) ? true : false;
    }

    // animate the button of the selected game level
    private void SetActiveButton(bool value, Selectable.Transition selectable)
    {
        lockedElement.SetActive(value);
        _button.transition = selectable;
        _button.enabled = !value;
    }
}
