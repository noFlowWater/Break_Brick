using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale += new Vector3(GameManager.instance.playerPlayPointX * 2 + 1,
                                            GameManager.instance.playerPlayPointY * 2 + 1,
                                            0.0f);
    }

}
