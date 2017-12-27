using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Stats : NetworkBehaviour {

    public Slider healthSlider;

    [SyncVar (hook = "UpdateHealth")]
    public int health;

    void UpdateHealth(int _Vida)
    {
        healthSlider.value = (float)_Vida / 100f;
    }

    public void ChangeHealth(int delta)
    {
        if (!isServer)
            return;
        health -= delta;

        if (health <= 0)
        {
            if(!gameObject.CompareTag("Player"))
                Destroy(gameObject);
        }

    } 
}
