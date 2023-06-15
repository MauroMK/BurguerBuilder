using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectZ : MonoBehaviour
{
    [SerializeField] private float targetPosition;
    private float originalPosition = -0.239f;
    private float timeToMove = 0.25f;
    private float delayTime = 0.3f;

    public void MoveCorrectSandwich()
    {
        LeanTween.delayedCall(delayTime, MoveHydraulicPressZ);   // Delay to let the sandwich get completed before goind to the trash
        AudioManager.instance.PlaySound("HydraulicPress");
    }

    private void MoveHydraulicPressZ()
    {
        LeanTween.moveLocalZ(this.gameObject, targetPosition, timeToMove).setEaseInOutSine().setOnComplete(MoveBackToOriginalPosition);
    }

    private void MoveBackToOriginalPosition()
    {
        LeanTween.moveLocalZ(gameObject, originalPosition, timeToMove).setEaseInOutSine();
    }
}