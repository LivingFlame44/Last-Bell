using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public GameObject interactText;
    public DialogueTrigger dialogueTrigger;

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
            dialogueTrigger.TriggerDialogue();
            //Time.timeScale = 0f;
        }
    }

    public void WinGame()
    {
        
    }
}
