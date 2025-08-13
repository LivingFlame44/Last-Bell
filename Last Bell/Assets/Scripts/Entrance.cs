using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Entrance : MonoBehaviour
{
    public GameObject interactText;
    public bool interacted;
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
            if (!interacted)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interacted = true;
                    ChangeScore();
                    StartCoroutine(CloseElevator());
                }
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
        if (GameManager.currentLevel <= 0)
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
            GameManager.currentLevel = GameManager.currentLevel - 1;

        }
        else
        {
            GameManager.currentLevel = GameManager.highestLevel;
        }
    }

    public IEnumerator CloseElevator()
    {

        GameManager.instance.CloseDoor();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(GenerateRandomLevel());
    }
}
