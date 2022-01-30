using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] TankControl p1_tank;
    [SerializeField] TankControl p2_tank;
    [SerializeField] GameObject p1_UIpanel;
    [SerializeField] GameObject p2_UIpanel;
    [SerializeField] Text winnertxt;
    public static GameManger instance;
    string winner;
    int buidindex;
    public enum turns
    {
        player1sturn,
        player2sturn,
        gameover
    }
    public turns currentTurn;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Bullet.turnchange.AddListener(changeTurn);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name =="Start")
        {
            Destroy(gameObject);
        }
        else if (currentTurn == turns.gameover)
        {
            var temp = GameObject.FindWithTag("Finish");
            if (temp != null)
            {
                winnertxt = temp.GetComponent<Text>();
            }
        }
    }
    public void changeTurn()
    {
        if(currentTurn == turns.player1sturn)
        {
            currentTurn = turns.player2sturn;
            P2turn();
        }
        else if(currentTurn == turns.player2sturn)
        {
            currentTurn = turns.player1sturn;
            P1turn();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
        switch(currentTurn)
        {
            case turns.player1sturn:
                checkhealth();
                break;
            case turns.player2sturn:
                checkhealth();
                break;
            case turns.gameover:
                winnertxt.text = winner;
                break;
            default:
                break;
        }
        
    }
    public void P1turn()
    {
        p2_tank.movable = false;
        p1_tank.movable = true;
        p2_tank.GetComponent<Trajectory>().enabled = false;
        p1_tank.GetComponent<Trajectory>().enabled = true;
        p2_UIpanel.SetActive(false);
        p1_UIpanel.SetActive(true);
    }
    public void P2turn()
    {
        p1_tank.movable = false;
        p2_tank.movable = true;
        p1_tank.GetComponent<Trajectory>().enabled = false;
        p2_tank.GetComponent<Trajectory>().enabled = true;
        p1_UIpanel.SetActive(false);
        p2_UIpanel.SetActive(true);
    }
    public void checkhealth()
    {
        if(p1_tank.Health<=0 )
        {
            winner = p2_tank.name;
            loadgameover();
        }
        else if(p2_tank.Health<=0)
        {
            winner = p1_tank.name;
            loadgameover();
        }
    }
    public void loadgameover()
    {
        SceneManager.LoadScene("Gameover");
        currentTurn = turns.gameover;
    }
    
}
