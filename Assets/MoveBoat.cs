using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
   private Vector3 localStartPoint = new Vector3(1.17f, 0.06f, 0f);  // Starting point of the boat in local space
    private Vector3 localEndPoint = new Vector3(3.48f, 0.01f, 0f);    // End point of the boat in local space
    public float speed = 1.0f;       // Speed of the boat movement

    private Vector3 globalStartPoint;
    private Vector3 globalEndPoint;
    private float startTime;
    private float journeyLength;
    public int halfCycles = 4;           // Number of back-and-forth cycles
    private int currentHalfCycle = 0;
    public GameObject firstScene;
    public AudioControllerScript audioController;
    public DialogueScript dialogueScript;
    bool isFinished = false;
     void Start()
    {
        // Convert local positions to global positions.
        globalStartPoint = transform.parent.TransformPoint(localStartPoint);
        globalEndPoint = transform.parent.TransformPoint(localEndPoint);

        // Set the boat's initial position to the local start point.
        transform.localPosition = localStartPoint;

        // Record the time when the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(globalStartPoint, globalEndPoint);
    }

    void Update()
    {
        if (currentHalfCycle < halfCycles)
        {
            // Calculate the distance covered based on the speed and time.
            float distCovered = (Time.time - startTime) * speed;

            // Calculate the fraction of the journey completed.
            float fractionOfJourney = distCovered / journeyLength;

            // Set the position of the object using local positions.
            transform.localPosition = Vector3.Lerp(localStartPoint, localEndPoint, fractionOfJourney);

            // Check if the boat has reached the end point.
            if (fractionOfJourney >= 1.0f)
            {
                // Swap localStartPoint and localEndPoint
                Vector3 temp = localStartPoint;
                localStartPoint = localEndPoint;
                localEndPoint = temp;

                // Reset startTime
                startTime = Time.time;

                // Increment the half-cycle counter
                currentHalfCycle++;

                // Recalculate journey length if needed
                journeyLength = Vector3.Distance(transform.parent.TransformPoint(localStartPoint), transform.parent.TransformPoint(localEndPoint));
            }
        }
        else
        {
            // Ensure the boat stops at the final end position after the half-cycles are complete
            transform.localPosition = localStartPoint;
            isFinished = true;
        }
        if(isFinished && Input.GetMouseButtonDown(0))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Activate the first scene and deactivate the parent object
        audioController.ChangeSubfolder(1);
        dialogueScript.ChangeScene(1, false);
        firstScene.SetActive(true);
        currentHalfCycle = 0;
        ChangeBoat.IsChangedMode = false;

        transform.parent.gameObject.SetActive(false);
    }
    void OnEnable(){
                // Record the time when the movement started.
        startTime = Time.time;
        isFinished = false;
    }

}
