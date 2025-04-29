/*
 * BoundaryDetector.cs
 * Marlow Greenan
 * 4/27/2025
 * 
 * Collision boxes lost the war. We're using raycasting now. ;---; F in the chat for OnTrigger.
 */
using UnityEngine;

public class BoundaryDetector : MonoBehaviour
{
    [SerializeField] private string _boundaryTag = "Boundary";
    private static bool boundaryAhead = false;

    [SerializeField] private LayerMask _boundaryLayer;
    [SerializeField] private float _distance = 15f;

    public static bool BoundaryAhead { get => boundaryAhead; set => boundaryAhead = value; }

    private void Update()
    {
        CheckBoundaries();
    }
    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("Enter");
    //    if (collision.gameObject.CompareTag(_boundaryTag))
    //        boundaryAhead = true;
    //}
    //private void OnTriggerExit(Collider collision)
    //{
    //    Debug.Log("Exit");
    //    if (collision.gameObject.CompareTag(_boundaryTag))
    //        boundaryAhead = false;
    //}
    public void CheckBoundaries()
    {
        boundaryAhead = false;

        switch (GameManager.Instance.HUD.FaceDirection)
        {
            case (Enums.Direction.Forward):
                Debug.DrawRay(transform.position, Vector3.forward * _distance, Color.yellow);
                if (Physics.Raycast(transform.position, Vector3.forward, _distance, _boundaryLayer, QueryTriggerInteraction.Collide))
                    boundaryAhead = true;
                break;
            case (Enums.Direction.Right):
                Debug.DrawRay(transform.position, Vector3.right * _distance, Color.yellow);
                if (Physics.Raycast(transform.position, Vector3.right, _distance, _boundaryLayer, QueryTriggerInteraction.Collide))
                    boundaryAhead = true;
                break;
            case (Enums.Direction.Left):
                Debug.DrawRay(transform.position, Vector3.left * _distance, Color.yellow);
                if (Physics.Raycast(transform.position, Vector3.left, _distance, _boundaryLayer, QueryTriggerInteraction.Collide))
                    boundaryAhead = true;
                break;
            case (Enums.Direction.Backwards):
                Debug.DrawRay(transform.position, Vector3.back * _distance, Color.yellow);
                if (Physics.Raycast(transform.position, Vector3.back, _distance, _boundaryLayer, QueryTriggerInteraction.Collide))
                    boundaryAhead = true;
                break;
        }
    }
}
