using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFadeInOut : MonoBehaviour
{
    private static TransitionFadeInOut instance = null;
    public static TransitionFadeInOut Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TransitionFadeInOut();
            }
            return instance;
        }
    }


    GameObject ActiveObject;
    GameObject HideObject;
    List<GameObject> HideObjects;
    Animator _Animator;
    bool isSingle = false;

    private void Awake()
    {
        instance = this;
        _Animator = GetComponent<Animator>();
        HideObjects = new List<GameObject>();
        //Debug.Log(HideObjects);
    }

    public void StartFadeInOut(GameObject actObj, GameObject hideObj)
    {
        ActiveObject = actObj;
        HideObject = hideObj;
        isSingle = true;
        _Animator.Play("Transition");
    }

    public void StartFadeInOut(GameObject actObj, params GameObject[] hideObj)
    {
        ActiveObject = actObj;
        //Debug.Log(hideObj[0]);
        //Debug.Log(hideObj[1]);
        foreach (var obj in hideObj)
        {
            //Debug.Log(HideObjects);
            //Debug.Log(obj);
            HideObjects.Add(obj);
        }
        isSingle = false;
        _Animator.Play("Transition");
    }

    public void FadeInOutAnimation()
    {
        if(ActiveObject != null) ActiveObject.SetActive(true);
        if(isSingle)
        {
            if (HideObject != null) HideObject.SetActive(false);
        }
        else
        {
            if (HideObjects != null)
            {
                foreach(var obj in HideObjects)
                {
                    obj.SetActive(false);
                }
            }
            HideObjects.Clear();
        }
    }
}
