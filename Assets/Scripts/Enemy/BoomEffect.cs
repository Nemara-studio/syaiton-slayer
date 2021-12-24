using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
