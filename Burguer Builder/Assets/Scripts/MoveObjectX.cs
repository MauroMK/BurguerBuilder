using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectX : MonoBehaviour
{
    [SerializeField] private float targetPosition;
    private float originalPosition = 0.4f;
    private float timeToMove = 0.25f;
    private float delayTime = 0.3f;

    public void MoveWrongSandwich()
    {
        LeanTween.delayedCall(delayTime, MoveHydraulicPressX);   // Delay to let the sandwich complete before goind to the trash
        AudioManager.instance.PlaySound("HydraulicPress");
    }

    private void MoveHydraulicPressX()
    {
        LeanTween.moveLocalX(this.gameObject, targetPosition, timeToMove).setEaseInOutSine().setOnComplete(MoveBackToOriginalPosition);
    }

    private void MoveBackToOriginalPosition()
    {
        LeanTween.moveLocalX(gameObject, originalPosition, timeToMove).setEaseInOutSine();
    }
}