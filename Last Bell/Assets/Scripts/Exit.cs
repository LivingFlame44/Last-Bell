using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    public GameObject interactText;
    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeScore();
                SceneManager.LoadScene(GenerateRandomLevel());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(false);
    
        }
    }

    public int GenerateRandomLevel()
    {
        if(GameManager.currentLevel <= 0)
        {
            return 2;
        }
        else
        {
            int randomNum = Random.Range(0, 2);

            if (randomNum == 0)
            {
                return 1;
            }

            else
            {
                return Random.Range(3, SceneManager.sceneCountInBuildSettings);
            }
        }
            
    }

    public void ChangeScore()
    {
        if (GameManager.instance.currentLevelType == GameManager.LevelType.Fake)
        {
            
            GameManager.currentLevel = GameManager.highestLevel;

        }
        else
        {
            GameManager.currentLevel = GameManager.currentLevel -1;
        }
    }
}
