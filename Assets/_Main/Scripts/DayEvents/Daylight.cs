using System;
using UnityEngine;

public class Daylight : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float speed;
    [SerializeField] private DayEvent day;
    [SerializeField] private NightEvent night;
    private float _currRotX;
    public event Action OnNight;
    public event Action OnDay;
    public static Daylight instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _transform = transform;
        _currRotX = _transform.rotation.x;
        SubscribeEvents();
    }

    void SubscribeEvents()
    {
        day.OnDay += DayCommand;
        night.OnNight += NightCommand;
    }

    void DayCommand()
    {
        OnDay?.Invoke();
    }    
    void NightCommand()
    {
        HudManager.instance.UpdateNights();
        OnNight?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        _currRotX += speed * Time.deltaTime ;
        _transform.localRotation = Quaternion.Euler(_currRotX,0,0);
    }
}
