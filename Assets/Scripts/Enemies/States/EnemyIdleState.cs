using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    private EnemyController controller;
    private LayerMask Layerfind;
    private Coroutine coroutine;
    private RaycastHit2D hit;
    private Vector2 target;
    public EnemyIdleState(StateMachine machine) : base(machine)
    {
        controller = machine as EnemyController;
    }

    public override void OnFinish()
    {
        coroutine = null;
    }

    public override void OnStart()
    {
        Layerfind = LayerMask.GetMask("Ground");
    }

    public override void OnUpdate()
    {        
        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(controller.unit.transform.position, Vector2.down, 1f, Layerfind);
            Move(Vector2.down);
            return;
        }
        if (coroutine == null)
        {
            coroutine = controller.unit.StartCoroutine(Rotate());
            target = new Vector2(controller.unit.transform.position.x, controller.unit.ModelEnemy.HeightFindLine);
        }

        if (Vector2.Distance(controller.unit.transform.position, target) > 0.1f)
        {
            Move(Vector2.up);
        }
        else
        {
            controller.Switch(new EnemyFindState(controller));
        }
    }

    public void Move(Vector2 direction)
    {
        controller.unit.rb.velocity = direction * controller.unit.ModelEnemy.Speed;
    }

    public IEnumerator Rotate()
    {
        controller.unit.transform.rotation = Quaternion.Euler(0f,0f,0f);
        Quaternion rotateEnd = Quaternion.Euler(0f, 0f, 180f);
        controller.unit.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        while (rotateEnd != controller.unit.transform.rotation)
        {
            controller.unit.transform.Rotate(new Vector3(0, 0, 1));
            yield return null;
        }
        controller.unit.rb.constraints = RigidbodyConstraints2D.None;
        controller.unit.transform.rotation = rotateEnd;
    }
}
