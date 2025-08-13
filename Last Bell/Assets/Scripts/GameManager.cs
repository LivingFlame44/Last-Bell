using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int highestLevel = 5;
    public static int currentLevel = 5;

    public static bool tutorialDone;

    public LevelType currentLevelType;

    public GameObject entranceNum, exitNum;

    public GameObject leftDoor, rightDoor;

    public GameObject tutorialPanel;
    public enum LevelType
    {
        Real,
        Fake,
        Final
    }

    void Awake()
    {
        // Enforce Singleton pattern
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(currentLevel == 0)
        {
            entranceNum.GetComponent<TextMeshPro>().text = "G";
        }
        else
        {
            entranceNum.GetComponent<TextMeshPro>().text = (currentLevel + 1).ToString() ;
        }
        

        if(exitNum != null)
        {
            exitNum.GetComponent<TextMeshPro>().text = (currentLevel + 1).ToString();
        }

        OpenDoor();

        if (!tutorialDone)
        {
            StartCoroutine(ShowTutorial());
        }

        AudioManager.Instance.PlayMusic("Ambient_TenseAtmosphere");
        AudioManager.Instance.PlaySFX("Ambient_MidnightSFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseDoor()
    {
        leftDoor.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-494, 0), 0.7f);
        rightDoor.GetComponent<RectTransform>().DOAnchorPos(new Vector2(492, 0), 1f);
    }

    public void OpenDoor()
    {
        leftDoor.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1550, 0), 0.7f);
        rightDoor.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1630, 0), 1f);      
    }

    public IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        if (tutorialPanel != null) 
        {
            tutorialPanel.SetActive(true);
        }
        
        tutorialDone = true;
    }
}
