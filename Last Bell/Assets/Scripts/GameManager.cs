using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int highestLevel = 3;
    public static int currentLevel = 3;

    public LevelType currentLevelType;

    public GameObject entranceNum, exitNum;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
