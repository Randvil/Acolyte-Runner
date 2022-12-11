using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button sound;
    public Sprite soundOn;
    public Sprite soundOff;

    public Button music;
    public Sprite musicOn;
    public Sprite musicOff;

    public void Play() => SceneManager.LoadScene(1);

    public void Quit() => Application.Quit();

    public void Music()
    {
        StaticSettings.musicOn = !StaticSettings.musicOn;
        if (StaticSettings.musicOn)
            music.GetComponent<Image>().sprite = musicOn;
        else
            music.GetComponent<Image>().sprite = musicOff;        
    }

    public void Sound()
    {
        StaticSettings.soundOn = !StaticSettings.soundOn;
        if (StaticSettings.soundOn)
            sound.GetComponent<Image>().sprite = soundOn;
        else
            sound.GetComponent<Image>().sprite = soundOff;
    }
}
