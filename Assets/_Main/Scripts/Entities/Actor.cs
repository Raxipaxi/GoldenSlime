using System;
using UnityEngine;


public class Actor : MonoBehaviour, iDamageable, iMobile
{
    public float CurrentLife { get; }
    public float MaxLife { get; }

    #region iDamageable

    public virtual void TakeDamage(float x)
    {
        
    }

    public virtual void Die()
    {
        
    }



    #endregion

    #region iMobile

    public virtual void Idle()
    {

    }

    public virtual void Attack(float dmg)
    {
        
    }


    public virtual void Move(Vector3 dir, float speed)
    {
       
    }
    
    #endregion
    
}