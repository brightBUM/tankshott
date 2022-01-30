using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(loadscenetrans(1));
        }
    }

    IEnumerator loadscenetrans(int time)
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("fade");
        yield return new WaitForSeconds(1);
        loadgame();
    }
    public void loadgame()
    {
        SceneManager.LoadScene("Game");
    }
    public void replay()
    {

        SceneManager.LoadScene("Start");
    }
    public void quitgame()
    {
        Application.Quit();
    }
}
