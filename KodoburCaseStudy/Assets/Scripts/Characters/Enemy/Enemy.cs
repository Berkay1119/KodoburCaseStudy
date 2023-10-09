using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override void Die()
    {
        EventManager.OnEnemyDied(this);
    }

    public int GetExperiencePoint()
    {
        return experiencePoint;
    }
}
