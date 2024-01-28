using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Titlescreen : MonoBehaviour
{
    /*
     * Player Manager Class
     * Title menu where options and credits menus can be accessed
     */

    [Header("Menu Buttons")]
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;

    [Space(10)]
    public Transform MenuCursor;
    public float MenuCursorSpace;

    public enum title_states
    {
        INTRO,
        TITLE,
        OPTIONS,
        CREDITS
    }
    title_states current_title_state = title_states.TITLE;
    int current_menu_option = 0;
    int toggle_input;
    int mouse_toggle_input;

    RectTransform current_menu_button;

    Text musicText;
    Text soundText;

    private void Awake()
    {
        current_menu_button = MainMenu.transform.GetChild(0).GetComponent<RectTransform>();

        musicText = OptionsMenu.transform.GetChild(0).GetComponent<Text>();
        soundText = OptionsMenu.transform.GetChild(1).GetComponent<Text>();
    }

    public void ChangeState(title_states state)
    {
        current_title_state = state;
    }

    public void ChangeMenuPosition(int pos)
    {
        current_menu_option = pos;
    }

    public void Select()
    {
        AudioManager.instance.PlayOneShot(FMODLib.instance.select);
        switch (current_title_state)
        {
            case title_states.INTRO:

                break;

            case title_states.TITLE:
                switch(current_menu_option)
                {
                    case 0:
                        // Start game
                        LevelManager.instance.ChangeScene(2);
                        break;
                    case 1:
                        current_title_state = title_states.OPTIONS;
                        MainMenu.SetActive(false);
                        OptionsMenu.SetActive(true);
                        current_menu_option = 0;
                        break;
                    case 2:
                        current_title_state = title_states.CREDITS;
                        EventHandler.OnSceneChange(Enumerations.GameState.CREDITS);
                        MainMenu.SetActive(false);
                        CreditsMenu.SetActive(true);
                        current_menu_option = 0;
                        break;
                }

                    break;

            case title_states.OPTIONS:
                if (current_menu_option == 2)
                {
                    current_title_state = title_states.TITLE;
                    OptionsMenu.SetActive(false);
                    MainMenu.SetActive(true);
                    current_menu_option = 1;
                }

                break;

            case title_states.CREDITS:
                current_title_state = title_states.TITLE;
                EventHandler.OnSceneChange(Enumerations.GameState.INTRO);
                CreditsMenu.SetActive(false);
                MainMenu.SetActive(true);
                current_menu_option = 2;

                break;

        }
    }

    void Update()
    {
        // Keyboard button selection
        if (Input.GetKeyDown(KeyCode.UpArrow)) current_menu_option--;
        if (Input.GetKeyDown(KeyCode.DownArrow)) current_menu_option++;

        if (Input.GetKeyDown(KeyCode.Return)) Select();

        // Menus navigation
        MenuCursor.localPosition = current_menu_button.localPosition + new Vector3((-current_menu_button.rect.width / 2) - MenuCursorSpace, 0, 0);
        switch (current_title_state)
        {
            case title_states.INTRO:

                break;

            case title_states.TITLE:
                current_menu_option = Mathf.Clamp(current_menu_option, 0, 2);
                current_menu_button = MainMenu.transform.GetChild(current_menu_option).GetComponent<RectTransform>();

                break;

            case title_states.OPTIONS:
                current_menu_option = Mathf.Clamp(current_menu_option, 0, 2);
                current_menu_button = OptionsMenu.transform.GetChild(current_menu_option).GetComponent<RectTransform>();

                toggle_input = Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0;
                int volumeText = 0;

                switch (current_menu_option)
                    {
                        case 0:
                            // Music toggle
                            if (toggle_input != 0)
                            {
                                AudioManager.instance.SetMusicVolume((float)toggle_input / 10);
                            }
                            else if (mouse_toggle_input != 0)
                            {
                                AudioManager.instance.SetMusicVolume((float)mouse_toggle_input / 10);
                            }

                            volumeText = Mathf.RoundToInt(AudioManager.MusicVolume * 100);
                            musicText.text = "Music Volume - " + volumeText.ToString() + "%";

                            break;
                        case 1:
                            // Sound toggle
                            if (toggle_input != 0)
                            {
                                AudioManager.instance.SetSoundVolume((float)toggle_input / 10);
                            }
                            else if (mouse_toggle_input != 0)
                            {
                                AudioManager.instance.SetSoundVolume((float)mouse_toggle_input / 10);
                            }

                            volumeText = Mathf.RoundToInt(AudioManager.SoundVolume * 100);
                            soundText.text = "Sound Volume - " + volumeText.ToString() + "%";

                            break;
                    }

                    break;

            case title_states.CREDITS:
                current_menu_option = Mathf.Clamp(current_menu_option, 1, 1);
                current_menu_button = CreditsMenu.transform.GetChild(current_menu_option).GetComponent<RectTransform>();

                break;

        }
    }

    private void OnGUI()
    {
        mouse_toggle_input = (int)Input.mouseScrollDelta.y;
    }
}
