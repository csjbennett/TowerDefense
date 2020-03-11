using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
