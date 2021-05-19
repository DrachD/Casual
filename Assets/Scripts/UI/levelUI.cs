using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// automatically add titles to our buttons
public class levelUI : MonoBehaviour
{
    [SerializeField] private List<Button> levelButtons;
    
    private void OnValidate()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].GetComponentInChildren<Text>().text = "LEVEL " + (i + 1);
        }
    }
}
