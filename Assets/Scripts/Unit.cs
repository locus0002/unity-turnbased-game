using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotationSpeed = 9f;
    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] private Animator animatorCtrl;

    private bool arrived = true;
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }
    public void Move(Vector3 targetPosition) 
    {
        arrived = false;
        this.targetPosition = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived) 
        {
            float distance = Mathf.Abs(Vector3.Distance(targetPosition, transform.position));
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
            animatorCtrl.SetBool("IsWalking", true);
            if (distance < minDistance)
            {
                arrived = true;
                animatorCtrl.SetBool("IsWalking", false);
            }
        }
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public override string ToString()
    {
        return GetInstanceID().ToString();
    }
}
