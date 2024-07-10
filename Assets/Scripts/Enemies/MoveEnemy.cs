using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float movementSpeed = 2f;
    public LayerMask groundLayer; // Слой для поиска коллайдеров снизу

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingDown = false;

    void Start()
    {
        startPosition = startPoint.position;
        targetPosition = endPoint.position;
    }

    void Update()
    {
        MoveObject();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        if (hit.collider != null)
        {
            movingDown = !movingDown; // Переключаем направление движения
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(transform.position, startPosition) < 0.1f || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Если объект достиг начальной или конечной точки, переключаем цели
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp;
        }

        if (movingDown)
        {
            // Двигаемся вниз к нижней точке
            transform.position = Vector3.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
        }
        else
        {
            // Двигаемся вверх к верхней точке
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
    }
}
