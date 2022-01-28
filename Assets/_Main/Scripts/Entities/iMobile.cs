// All actors that have movement use this interface

using UnityEngine;

public interface iMobile // Can move and attack
{
    void Idle();
    void Attack(float dmg);
    void Move(Vector3 dir, float speed);

}