using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyFindState : State
{
    private EnemyController controller;
    private Coroutine coroutine;
    private Coroutine coroutineRotate;
    private LayerMask layerfind;
    private LayerMask layerfPlayer;
    private RaycastHit2D hitPlayer;
    private RaycastHit2D hit;
    private Vector2 direction;
    private bool PlayerFinded = false;
    private int[] array = new int[2] { 1, -1 };
    public delegate void CoroutineCallback();
    public EnemyFindState(StateMachine machine) : base(machine)
    {
        controller = machine as EnemyController;
    }

    public override void OnFinish()
    {
        controller.unit.rb.constraints = RigidbodyConstraints2D.None;
    }

    public override void OnStart()
    {
        layerfind = 1 << LayerMask.NameToLayer("Ground");
        layerfPlayer = 1 << LayerMask.NameToLayer("Player");
        controller.unit.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        direction.y = controller.unit.transform.position.y;
        direction.x = array[Random.Range(0, array.Length)];
        coroutine = controller.unit.StartCoroutine(Rotate(180, 90));
    }

    public override void OnUpdate()
    {
        if (coroutine != null || PlayerFinded)
        {
            return;
        }
        Move(direction);
        FindPlayer();
        Find();
    }
    public void FindPlayer()
    {
        hitPlayer = Physics2D.Raycast(controller.unit.transform.position, Vector2.down, 20f, layerfPlayer);
        if (hitPlayer.collider == null)
        {
            return;
        }
        if (hitPlayer.collider != null && hitPlayer.collider.TryGetComponent<TankBase>(out TankBase tankBase))
        {
            PlayerFinded = true;
            controller.unit.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            coroutineRotate = controller.unit.StartCoroutine(Rotate(90, 0, FinishRotate));
            return;
        }

    }
    public void Find()
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.unit.transform.position, direction, 1f, layerfind);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider != null)
        {
            direction.x *= -1;
        }
    }

    public void Move(Vector2 direction)
    {
        controller.unit.rb.velocity = direction * controller.unit.ModelEnemy.Speed;
    }
    public IEnumerator Rotate(float nowpos, float newpos, CoroutineCallback callback = null)
    {
        controller.unit.transform.rotation = Quaternion.Euler(0f, 0f, nowpos);
        Quaternion rotateEnd = Quaternion.Euler(0f, 0f, newpos);
        while (rotateEnd != controller.unit.transform.rotation)
        {
            controller.unit.transform.Rotate(new Vector3(0, 0, -1));
            yield return null;
        }
        coroutine = null;
        callback?.Invoke();

        controller.unit.transform.rotation = rotateEnd;
    }
    public void FinishRotate()
    {
        controller.Switch(new EnemyIdleState(controller));
    }
}
