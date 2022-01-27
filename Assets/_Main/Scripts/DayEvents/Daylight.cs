using System;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float speed;
    private float _currRotX;
    
    // Start is called before the first frame update
    void Awake()
    {
        _transform = transform;
        _currRotX = _transform.rotation.x;
    }

    // Update is called once per frame
    void Update()
    {

        _currRotX += speed * Time.deltaTime ;
        
        _transform.localRotation = Quaternion.Euler(_currRotX,0,0);
        
    }
}
