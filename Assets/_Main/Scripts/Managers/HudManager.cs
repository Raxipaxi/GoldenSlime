using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    [SerializeField]private TextMeshProUGUI ammoText;

    private void Awake()
    {
        instance = this;
    }
    public  void UpdateAmmo(int currentAmmo)
    {
        ammoText.text = currentAmmo.ToString();
    }


}
