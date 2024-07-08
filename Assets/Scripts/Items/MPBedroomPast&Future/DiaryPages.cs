using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryPages : MonoBehaviour
{
    [Header("Pages")]
    public GameObject[] pages;
    public int buttonsClicked = 0;
    public int[] pageOrder;
    public GameObject fileNames;

    // Start is called before the first frame update
    void Start()
    {
        pages[0].SetActive(true);
        buttonsClicked = 0;
        fileNames = FindInActiveObjectByTag("Seeing");
        fileNames.SetActive(true); // adds seeing to the file
    }

    // Update is called once per frame
    void Update()
    {

    }

    // deactivates the current page and activates the next 
    public void NextPage(int button)
    {
        if (buttonsClicked < pages.Length - 1) 
        {
            buttonsClicked++;
            if (button == pageOrder[buttonsClicked])
            {
                pages[buttonsClicked].SetActive(true);
                pages[buttonsClicked - 1].SetActive(false);
            }    
        }

        else
            print("No more Pages");
    }

    // deactivates the current page and activates the last one
    public void LastPage(int button)
    {
        if (buttonsClicked > pages.Length - 4)
        {
            buttonsClicked--;

            if (button == pageOrder[buttonsClicked])
            {
                pages[buttonsClicked].SetActive(true);
                pages[buttonsClicked + 1].SetActive(false);
            }
        }

        else
            print("No more Pages");
    }

    // finds inactive objects
    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
