using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    public static UI instance;
    public Canvas canvasLose;
    public Canvas canvasVictory;

    public TextMeshProUGUI scoreUI;

    public TextMeshProUGUI scoreresult;
    
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        canvasLose.gameObject.SetActive(false);
        canvasVictory.gameObject.SetActive(false);        
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = Player.instance.score.ToString();
    }
    public IEnumerator Victory(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvasVictory.gameObject.SetActive(true);
        scoreresult.text = Player.instance.score.ToString();
    }
    public void Lose()
    {
        canvasLose.gameObject.SetActive(true);
    }

}