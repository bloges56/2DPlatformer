using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{   
    void ApplyDamage(int damage);

    void Kill();

}
