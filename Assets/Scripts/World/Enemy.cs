using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public bool existsInReality;
    public bool existsInImagination;
    
    enum EnemyType
    {
        melee,
        ranged
    }
}
