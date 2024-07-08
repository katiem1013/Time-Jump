using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TvRemote : MonoBehaviour
{
    public Animator animator;

    [Space(5)]
    [Header("Rows")]
    public float row1;
    public float row2;
    public float row3;
    public float row4;

    [Space(5)]
    [Header("Toggles")]
    public float amountOfToggles = 12; // helps track how many toggle should be within the scene easily
    public Toggle toggle0, toggle1, toggle2;
    public Toggle toggle3, toggle4, toggle5;
    public Toggle toggle6, toggle7, toggle8;
    public Toggle toggle9, toggle10, toggle11;

    [Space(5)]
    [Header("Canvas")]
    public GameObject tvRemote;
    static public bool tvOn = false;

    // Start is called before the first frame update
    void Start()
    {
        row1 = 0; row2 = 0; row3 = 0; row4 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (row1 == 1 && row2 == 2 && row3 == 1 && row4 == 3)
        {
            tvOn = true;
            Invoke("TvOn", 1);
        }

        if (tvOn == true)
            StopInteract();

        // row 1 toggles
        if(row1 == 1)
            toggle0.Select();
        if (row1 == 2)
            toggle1.Select();
        if (row1 == 3)
            toggle2.Select();

        // row 2 toggles
        if (row2 == 1)
            toggle3.Select();
        if (row2 == 2)
            toggle4.Select();
        if (row2 == 3)
            toggle5.Select();

        // row 3 toggles
        if (row3 == 1)
            toggle6.Select();
        if (row3 == 2)
            toggle7.Select();
        if (row3 == 3)
            toggle8.Select();

        // row 4 toggles
        if (row4 == 1)
            toggle9.Select();
        if (row4 == 2)
            toggle10.Select();
        if (row4 == 3)
            toggle11.Select();
    }

    // changes which button is selected
    public void Row1()
    {
        if (toggle0.isOn)
            row1 = 1;

        if (toggle1.isOn)
            row1 = 2;

        if (toggle2.isOn)
            row1 = 3;
    }

    public void Row2()
    {
        if (toggle3.isOn)
            row2 = 1;

        if (toggle4.isOn)
            row2 = 2;

        if (toggle5.isOn)
            row2 = 3;
    }

    public void Row3()
    {
        if (toggle6.isOn)
            row3 = 1;

        if (toggle7.isOn)
            row3 = 2;

        if (toggle8.isOn)
            row3 = 3;
    }

    public void Row4()
    {
        if (toggle9.isOn)
            row4 = 1;

        if (toggle10.isOn)
            row4 = 2;

        if (toggle11.isOn)
            row4 = 3;
    }

    // activate canvas
    public void Interact()
    {
        if (!tvOn)
            tvRemote.SetActive(true);
    }

    // deactivates canvas
    public void StopInteract()
    {
        tvRemote.SetActive(false);
    }

    public void OnMouseDown()
    {
        // sets each row to 0
        Interact();
        row1 = 0; row2 = 0; row3 = 0; row4 = 0;
        Invoke("TvStatic", 1);
    }

    // turns the tv on
    void TvOn()
    {
        animator.SetTrigger("TvOn");
    }
    
    // plays static if the tv isn't on
    void TvStatic()
    {
        animator.SetTrigger("StaticOn");
    }
}
