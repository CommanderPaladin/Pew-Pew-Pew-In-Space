using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryFlow : MonoBehaviour
{
    public int StoryLevelSelector = 1;
    public float secondsBetweenTexts = 2f;
    public bool nextSceeneOnEnd = false;
    public bool isThisStory = false;

    // Start is called before the first frame update
    Color32 startColor= new Color(255, 255, 255, 0);
    Color32 endColor  = new Color(255, 255, 255, 255);
    TextMeshProUGUI textMesh;
    List<string> storyText = new List<string>();

    int lineCounter = 0;
    int lineCounterTips = 0;
    byte alpha = 0;



    IEnumerator FadeOn()
    {
        //textMesh.alpha = 0;



        float t = 0;
        while (t < 1)
        {
            textMesh.color = Color32.Lerp(startColor, endColor, t);
            t += Time.deltaTime;
            yield return null;
        }
        /*if (alpha < 255)
        {
            alpha += 1;
            //yield return new WaitForSeconds(0.00001f);
            yield return FadeOn();
        }
        yield return null;*/

    }

    IEnumerator FadeOff()
    {

        float t = 0;
        while (t < 1)
        {
            textMesh.color = Color32.Lerp(endColor, startColor, t);
            t += Time.deltaTime; ;
            yield return null;
        }
    }


    void Start()
    {
        textMesh = this.GetComponent<TextMeshProUGUI>();

        textMesh.text = "Hello commander!";

        //Switch maybe?
        if (StoryLevelSelector == 1)
            GenerateStoryText1();
        else if (StoryLevelSelector == -1)
            TipMessages();
        else if (StoryLevelSelector == -2)
            BattleTipsMission1();

        if (storyText.Count > 0)
            StartCoroutine(StoryTelling());
        else
        {
            if (nextSceeneOnEnd==true)
                End();
        }

    }

    // Update is called once per frame
    private void TipMessages()
    {
        string text= string.Empty ;
        text = "{w}Press Left Click to [SKIP]";
        storyText.Add(text);
        text = "Press Left Click to [SKIP]";
        storyText.Add(text);
        text = "Press Left Click to [SKIP]";
        storyText.Add(text);
        text = "Press Left Click to [SKIP]";
        storyText.Add(text);
        text = "";
        storyText.Add(text);
    }
    private void BattleTipsMission1()
    {
        string text = string.Empty;
        text = "All you have to do now is to destroy the enemy Battleship and it's fighters.";
        storyText.Add(text);
        text = "In order to do that you first need to dezactivate it's shields.";
        storyText.Add(text);
        text = "To dezactivate the enemy's Battleship Shield, you have to destroy all the enemy fighters or to survive 3 waves.";
        storyText.Add(text);
        text = "Good luck Commander!";
        storyText.Add(text);
        text = "";
        storyText.Add(text);
    }
    private void GenerateStoryText1()
    {
        string text = string.Empty;

        text = "{w}Not all stories start with a happy beginning...";
        storyText.Add(text);

        text = "I don't have a body anymore.";
        storyText.Add(text);


        text = "It's just me, and some text on a screen.";
        storyText.Add(text);


        text = "Until...";
        storyText.Add(text);

        text = "Until someone needs my help again...";
        storyText.Add(text);


        text = "I was a soldier, on my home planet.";
        storyText.Add(text);


        text = "An unknown alien species came and...";
        storyText.Add(text);

        text = "...blew up my lovely planet...";
        storyText.Add(text);


        text = "I was on it. Capable of nothing...";
        storyText.Add(text);


        text = "...only to see my death...";
        storyText.Add(text);

        text = "...and the death of the things I sworn to protect";
        storyText.Add(text);


        text = "I thought I was dead.";
        storyText.Add(text);


        text = "Some ships from the Federation came and...";
        storyText.Add(text);

        text = "...searched for survivors.";
        storyText.Add(text);

        text = "All they found was my planet being torn apart into pieces.";
        storyText.Add(text);

        text = "Oh yea, they also found me.";
        storyText.Add(text);

        text = "Lucky me...";
        storyText.Add(text);

        text = "Those bastards didn't let me die in peace.";
        storyText.Add(text);


        text = "They want me to fight for them.";
        storyText.Add(text);

        text = "To fight from a computer.";
        storyText.Add(text);

        text = "At what cost?";
        storyText.Add(text);

        text = "For how long?";
        storyText.Add(text);

        text = "What do I have to gain?";
        storyText.Add(text);

        //Change Color To red
        text = "{g}Hello commander.";
        storyText.Add(text);

        text = "You will be deployed to fight some rebels in Castadian System.";
        storyText.Add(text);

        //Change Color to white
        text = "{w}Here we go again...";
        storyText.Add(text);

        /*for (int i = 0; i < storyText.Count;i++)
        {
            Debug.Log(storyText[i]);
        }*/








    }

    private void ChangeTextColor(string color)
    {
        if (color == "white")
        {
            startColor = new Color(255, 255, 255, 0);
            endColor = new Color(255, 255, 255, 255);
        }


        if (color == "red")
        {
            startColor = new Color(255, 0, 0, 0);
            endColor = new Color(255, 0, 0, 255);
        }

        if (color == "green")
        {
            startColor = new Color(0, 255, 0, 0);
            endColor = new Color(0, 255, 0, 255);
        }

    }
    private string CheckText(string text)
    {
        if (text.Contains("{w}"))
        {
            this.ChangeTextColor("white");
            text=text.Replace("{w}","");
        }

        if (text.Contains("{r}"))
        {
            this.ChangeTextColor("red");
            text=text.Replace("{r}", "");
        }

        if (text.Contains("{g}"))
        {
            this.ChangeTextColor("green");
            text = text.Replace("{g}", "");
        }
        return text;
    }
    IEnumerator StoryTelling()
    {
        string currentText = string.Empty;
        currentText = storyText[lineCounter];
        currentText = CheckText(currentText);

        #region Old
        /*if (currentText.Contains("{w}"))
            this.ChangeTextColor("white");
        if (currentText.Contains("{r}"))
            this.ChangeTextColor("red");*/
        #endregion

        textMesh.text = currentText.ToLower();
        yield return FadeOn();
        //StartCoroutine(FadeOn());

        lineCounter++;

        yield return new WaitForSeconds(secondsBetweenTexts);
        if (lineCounter < storyText.Count)
        {
            //StartCoroutine(FadeOff());
            yield return FadeOff();
            StartCoroutine(StoryTelling());
            
        }
        else
        {
            if (nextSceeneOnEnd==true)
                End();
        }


    }

    private void End()
    {
        GameControl.NextLevel();
    }

    void Update()
    {
        if (isThisStory==true && Input.GetMouseButton(0))
        {
            End();
        }
    }
}
