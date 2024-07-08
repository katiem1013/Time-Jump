using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBag : MonoBehaviour
{
    [Header("Slider")]
    public Scrollbar bagOpening;
    public bool bagOpened;
    static public float watchFixed;

    [Header("Images")]
    public GameObject evidenceBag;
    public GameObject watch, watchZoom, watchZoomBack, watchBackOpen, watchText;
    public GameObject fileNames;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(watchFixed);
        
        // checks if the bag is opened
        if (bagOpening.value == 1 && !bagOpened)
        {
            evidenceBag.SetActive(false);
            watch.SetActive(true);
            watchZoom.SetActive(false);
            watchZoomBack.SetActive(false);
            watchBackOpen.SetActive(false);
            bagOpened = true;
        }

        // checks if the watch is fixed
        if (watchFixed == 4)
        {
            evidenceBag.SetActive(false);
            watch.SetActive(false);
            watchZoom.SetActive(true);
            watchZoomBack.SetActive(false);
            watchBackOpen.SetActive(false);
            watchText.SetActive(true);

            fileNames = FindInActiveObjectByTag("Time"); // adds time to the file
            fileNames.SetActive(true);
        }
    }

    // opens the zoom in on watch
    public void ZoomInOnWatch()
    {
        evidenceBag.SetActive(false);
        watch.SetActive(false);
        watchZoom.SetActive(true);
        watchZoomBack.SetActive(false);
        watchBackOpen.SetActive(false);
    }

    // shows the back of the watch
    public void ZoomFlipWatch() 
    {
        evidenceBag.SetActive(false);
        watch.SetActive(false);
        watchZoom.SetActive(false);
        watchZoomBack.SetActive(true);
        watchBackOpen.SetActive(false);
    }

    // shows the inside of the watch
    public void OpenWatch()
    {
        evidenceBag.SetActive(false);
        watch.SetActive(false);
        watchZoom.SetActive(false);
        watchZoomBack.SetActive(false);
        watchBackOpen.SetActive(true);
    }

    // finds inactive game objects
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
