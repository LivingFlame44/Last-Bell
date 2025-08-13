using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public GameObject interactText;
    public GameObject winPanel;

    public bool interacted;
    public UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.SetActive(true);
            if (!interacted)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interacted = true;
                    WinGame();
                }
            }
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }
}
