using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BaseFireCell fire = GetComponent<BaseFireCell>();
            if (fire != null)
            {
                fire.Extinguish();
            }
        }

    }
    public void Ignite()
    {
        BaseFireCell fire = GetComponent<BaseFireCell>();
        if (fire != null)
        {
            fire.Ignite();
        }
    }
}
