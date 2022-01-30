using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShotGun : MonoBehaviour
{
    public int maxAmmo;
    public float spreadAngle;
    public float pelletVel;
    public GameObject pellet;
    public Transform barrelExit;
    private List<Quaternion> pellets;
    public Vector3 offset;
    [SerializeField] private int currentAmmo;
    private void Awake()
    {
        pellets = new List<Quaternion>(maxAmmo);
        for (int i = 0; i < maxAmmo; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
        currentAmmo = maxAmmo;
    }
    private void Start()
    {
        Daylight.instance.OnDay += Reload;
    }

    public void Fire(float damage)
    {
 
        HudManager.instance.UpdateAmmo(currentAmmo);
        int index = 0 ;
        if(currentAmmo <= 0) { AudioManager.instance.PlaySound(SoundClips.EmptyGun); return; }
        if(currentAmmo > 0)
        {
            AudioManager.instance.PlaySound(SoundClips.Shoot);
            currentAmmo--;
            HudManager.instance.UpdateAmmo(currentAmmo);

            for (int i = 0; i < pellets.Count - 1; i++)
            {
                var randomPos = Random.insideUnitSphere;
                pellets[index] = Random.rotation;
                GameObject pelletInst = GenericPool.Instance.SpawnFromPool("Bullet", barrelExit.position, barrelExit.rotation);
                //GameObject pelletInst = Instantiate(this.pellet, barrelExit.position, barrelExit.rotation);
                pelletInst.transform.rotation = Quaternion.RotateTowards(pelletInst.transform.rotation, pellets[index], spreadAngle);
                pelletInst.GetComponent<Rigidbody>().AddForce(transform.up * pelletVel, ForceMode.Acceleration);
                index++;
            }
        }

       
    }
    public void Reload()
    {
        currentAmmo = maxAmmo;
        HudManager.instance.UpdateAmmo(currentAmmo);
        AudioManager.instance.PlaySound(SoundClips.Reload);
    }
}
