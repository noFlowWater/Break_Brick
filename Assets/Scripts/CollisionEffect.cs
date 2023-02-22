using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Play();// 재생시작
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
