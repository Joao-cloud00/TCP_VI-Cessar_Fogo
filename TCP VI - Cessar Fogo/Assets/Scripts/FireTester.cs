using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            BaseFireCell fire = GetComponent<BaseFireCell>();
            if (fire != null)
            {
                fire.Ignite();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BaseFireCell fire = GetComponent<BaseFireCell>();
            if (fire != null)
            {
                fire.Extinguish();
            }
        }

    }
}
