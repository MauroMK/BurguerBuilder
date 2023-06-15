using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraulicPress : MonoBehaviour
{
    [SerializeField] private float targetPosition;
    private float timeToMove = 0.25f;
    private float originalPosition = 0.12f;
    private float delayTime = 0.3f;

    public void MovePressWithDelay()
    {
        LeanTween.delayedCall(delayTime, MoveHydraulicPress);
    }

    private void MoveHydraulicPress()
    {
        LeanTween.moveLocalY(this.gameObject, targetPosition, timeToMove).setEaseInOutSine().setOnComplete(MoveBackToOriginalPosition);
    }

    private void MoveBackToOriginalPosition()
    {
        LeanTween.moveLocalY(gameObject, originalPosition, timeToMove).setEaseInOutSine();
    }
}
