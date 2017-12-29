using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadious = 0.1f;
    [SerializeField] float attackMoveStopRadious = 5f;
    ThirdPersonCharacter m_Character;
    CameraRaycaster cameraRaycaster;
    Vector3 CurrentDestination, clickPoint;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        CurrentDestination = transform.position;
    }

    private void FixedUpdate()
    {
        ProcessMouseMovment();
    }
    private void ProcessMouseMovment()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;

            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    CurrentDestination = ShortDestination(clickPoint, walkMoveStopRadious);
                    break;
                case Layer.Enemy:
                    // TODO make sure not to move away from target

                    CurrentDestination = ShortDestination(clickPoint, attackMoveStopRadious);

                    break;
                default:
                    print("shuld not be there");
                    return;

            }
        }
        walkToDestination();

    }



    private void walkToDestination()
    {
        var playerToClickPoint = CurrentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0.01)
        {
            m_Character.Move(CurrentDestination - transform.position, false, false);
        } else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)

    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
        {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, CurrentDestination);
        Gizmos.DrawLine(CurrentDestination, clickPoint);
        Gizmos.DrawSphere(CurrentDestination, 0.05f);
        Gizmos.DrawSphere(clickPoint, 0.1f);
        print("gizmo draw");

        }
    
}

