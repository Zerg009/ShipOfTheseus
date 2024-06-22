using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHalf : MonoBehaviour
{
    private int currentIndex = 0; // Start index
    bool IsFinished = false;
    public static bool IsChangedMode = false;
    public GameObject thirdScene;

    public Vector3 localStartPoint = new Vector3(1.17F, 0.06f, 0f);  // Starting point of the object in local space
    public Vector3 localEndPoint = new Vector3(12.35f, 1.02f, 0f);    // End point of the object in local space
    public Vector3 controlPoint = new Vector3(2.5f, 1.0f, 0f);       // Control point for the Bezier curve
    public float speed = 1.0f;       // Speed of the object movement
    private Vector3 globalStartPoint;
    private Vector3 globalEndPoint;
    private Vector3 globalControlPoint;
    private float startTime;
    private float journeyLength;
    private bool isMoving = false;
    public AudioControllerScript audioController;
    public DialogueScript dialogueScript;
    private void Start()
    {


        // Convert local positions to global positions.
        globalStartPoint = transform.parent.TransformPoint(localStartPoint);
        globalEndPoint = transform.parent.TransformPoint(localEndPoint);
        globalControlPoint = transform.parent.TransformPoint(controlPoint);

        // Calculate the journey length.
        journeyLength = Vector3.Distance(globalStartPoint, globalEndPoint);

        // Optional: Set the object's initial position to the local start point.
        transform.localPosition = localStartPoint;

        // Start the movement
        //StartMovement();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!isMoving)
            {
                if (currentIndex == 0 )
                {    
                    StartMovement();
                    return;
                }
                if (currentIndex == 1)
                {
                    DialogueScript.stopDialogue = false;
                    currentIndex = 0;
                    ChangeScene(thirdScene);
                    return;
                }
            }
            
        }
        if (isMoving)
        {
            MoveAlongCurve();
        }
    }
    void ChangeScene(GameObject scene)
    {
        scene.SetActive(true);
        ChangeBoat.IsChangedMode = true;
        dialogueScript.ChangeScene(3, false);
        audioController.ChangeSubfolder(3);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void StartMovement()
    {
        DialogueScript.stopDialogue = true;
        currentIndex++;
        startTime = Time.time;
        isMoving = true;
    }

    void MoveAlongCurve()
    {
        // Calculate the distance covered based on the speed and time.
        float distCovered = (Time.time - startTime) * speed;

        // Calculate the fraction of the journey completed.
        float fractionOfJourney = distCovered / journeyLength;

        // Calculate the position along the Bezier curve using the fraction of the journey
        Vector3 newPosition = CalculateBezierPoint(fractionOfJourney, globalStartPoint, globalControlPoint, globalEndPoint);

        // Set the position of the object
        transform.position = newPosition;

        // Check if the object has reached the end point.
        if (fractionOfJourney >= 1.0f)
        {
            isMoving = false;
            transform.position = globalEndPoint; // Ensure the object is exactly at the end point
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // u^2 * p0
        p += 2 * u * t * p1; // 2 * u * t * p1
        p += tt * p2;        // t^2 * p2

        return p;
    }
    void OnEnable()
    {
        transform.position = globalStartPoint;
    }
}
