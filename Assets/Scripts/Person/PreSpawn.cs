using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreSpawn : MonoBehaviour
{
    public event SystemTools.SimpleSystemCB OnAnimationEnded;
    public List<GameObject> activationGOs = new List<GameObject>();

    private bool activationAdded = false;

    public void AnimationEnded()
    {
        OnAnimationEnded?.Invoke();
        Destroy(gameObject);
    }

    public void AddGOActivation(GameObject go)
    {
        activationGOs.Add(go);
        if (!activationAdded)
        {
            activationAdded = true;
            OnAnimationEnded += ActivateGO;
        }
    }

    private void ActivateGO()
    {
        foreach(GameObject go in activationGOs)
        {
            go.SetActive(true);
        }
    }
}
