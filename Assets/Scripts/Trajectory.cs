using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trajectory : MonoBehaviour
{
    //[SerializeField] int id;
    [SerializeField] float power;
    [SerializeField] float angle;
    [SerializeField] public Transform canon;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootpos;
    [SerializeField] Text angletext;
    [SerializeField] Text powertext;
    [SerializeField] public Transform controlpt2;
    [SerializeField] public Transform controlpt3;
    [SerializeField] LineRenderer lr;
    bool corountineallowed;
    bool firepressed;
    [SerializeField]Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        firepressed = false;
        corountineallowed = true;
        lr.positionCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        pos.position = shootpos.transform.right;
        if (corountineallowed && firepressed)
        {
            StartCoroutine(followcurve());
        }
        updatecontrolpoints();

    }
    private void updatecontrolpoints()
    {
        if(GameManger.instance.currentTurn == GameManger.turns.player2sturn)
        {
            controlpt2.position = (shootpos.position-canon.position).normalized * power;
            //controlpt2.position = (shootpos.transform.right * power);
            var diff = controlpt2.position - shootpos.position;
            controlpt3.position = new Vector3(controlpt2.position.x + diff.x, shootpos.position.y, 0);
        }
        else
        {
            controlpt2.position = (shootpos.position-canon.position) * power;
            //controlpt2.position = (shootpos.transform.right * power);
            var diff = controlpt2.position - shootpos.position;
            controlpt3.position = new Vector3(controlpt2.position.x + diff.x, shootpos.position.y, 0);
        }
        

        Drawcurve();
    }
    public void Drawcurve()
    {
        int i = 0;
        for (float t = 0; t <= 0.5f; t += 0.05f)
        {
            // 3 point bezier curve
            var curvepoints = Mathf.Pow(1 - t, 2) *shootpos.position + 2 * (1 - t) * t *controlpt2.position + Mathf.Pow(t, 2) *controlpt3.position;
            lr.SetPosition(i, curvepoints);
            i++;
        }
    }
    IEnumerator followcurve()
    {
        corountineallowed = false;
        var temp = Instantiate(projectile, shootpos.position, Quaternion.identity);
        float k = 0;
        while (k < 1)
        {
            k += Time.deltaTime;
            var curvepoints = Mathf.Pow(1 - k, 2) * shootpos.position + 2 * (1 - k) * k * controlpt2.position + Mathf.Pow(k, 2) * controlpt3.position;
            if(temp!=null)
            {
                temp.transform.position = curvepoints;
            }
            yield return new WaitForEndOfFrame();
        }
        corountineallowed = true;
        firepressed = false;
    }

    #region UIbuttons
    public void incpower(int amount)
    {
        if(power<=15)
        {
            power += amount;
        }
        powertext.text = power.ToString();
    }
    public void decpower(int amount)
    {
        if(power>0)
        {
            power -= amount;
        }
        powertext.text = power.ToString();
    }
    public void incangle()
    {
        angle += 10;
        canon.Rotate(new Vector3(0, 0, 10));
        angletext.text = angle.ToString();
    }
    public void decangle()
    {
        angle -= 10;
        canon.Rotate(new Vector3(0, 0, -10));
        angletext.text = angle.ToString();
    }
    public void fire()
    {
        firepressed = true;
    }
    #endregion
}
