using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateMachine
{
    public EnemyUnit unit;

    public EnemyController(EnemyUnit enemyUnit)
    {
        unit = enemyUnit;
    }
    public override void OnUpdate()
    {
        Current?.OnUpdate();
    }

    public override void Switch(State state)
    {
        Current?.OnFinish();
        Current = state;
        Current?.OnStart();
    }
}
