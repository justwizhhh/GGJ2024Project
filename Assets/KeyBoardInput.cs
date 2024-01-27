using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Windows;

public class KeyBoardInput : MonoBehaviour
{

    Event input = Event.KeyboardEvent("Enter");

    private void OnGUI()
    {
        input = Event.current;
    }


    // Update is called once per frame
    void Update()
    {
        if (input != null)
        {
            if (input.isKey)
            {
                // Check for correct letter
                string key = input.keyCode.ToString();
                if (key != "None")
                {
                    if (key.Length <= 1)
                    {
                        //Debug.Log(key);
                        EventHandler.OnLetterTyped(key[0]);
                    }
                }
            }
        }
    }
}
