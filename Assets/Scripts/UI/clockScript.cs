using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockScript : MonoBehaviour
{

    // IMAGE
    private Transform clockWheel;

    // LIMITS
    [SerializeField] private float maxTime = 1000f;
    [SerializeField] private float currentTime; // for testing

    // LERPING
    [SerializeField] private float lerpTime = 0.2f;
    private Quaternion currentAngle;



    private void Start()
    {
        clockWheel = transform.GetChild(0);
        currentTime = 0;
    }

    private void Update()
    {
        if(currentTime > maxTime) currentTime = maxTime;
        if(currentTime < 0) currentTime = 0;

        UpdateClock();
    }

    private void UpdateClock()
    {
        currentAngle = Quaternion.Euler(0, 0, 270 * (currentTime / maxTime));
        clockWheel.rotation = Quaternion.Lerp(clockWheel.rotation, currentAngle, Time.deltaTime / lerpTime);
    }

    public void SetTime(float newTime)
    {
        currentTime = newTime;
    }

}
