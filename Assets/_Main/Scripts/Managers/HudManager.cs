using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    [SerializeField]private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI nights;
    [SerializeField] private int currentnight;
    private void Awake()
    {
        instance = this;
    }
    public  void UpdateAmmo(int currentAmmo)
    {
        ammoText.text = currentAmmo.ToString();
    }
    public void UpdateNights()
    {
        currentnight++;
        if(currentnight == 4)
        {
            GameManager.instance.LoseScreen();
        }
        nights.text = currentnight.ToString();
    }


}
