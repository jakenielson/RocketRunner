using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    Collider myCollider;
    bool loadIsDown;
    bool noclipIsDown;
    bool isNoclipping;

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void Update()
    {
        ProcessInput();
        ProcessCheats();
    }

    private void ProcessInput()
    {
        loadIsDown = Input.GetKeyDown(KeyCode.L);
        noclipIsDown = Input.GetKeyDown(KeyCode.C);
    }

    private void ProcessCheats()
    {
        if (loadIsDown)
        {
            myCollider.enabled = true;
            myCollider.LoadNextLevel();
        }

        if (noclipIsDown)
        {
            myCollider.ToggleActive();
        }
    }
}
