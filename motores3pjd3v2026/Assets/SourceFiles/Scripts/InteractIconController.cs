using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractIconController : MonoBehaviour
{
    private bool isInteractable;
    private Vector3 worldPosition;
    private Image iconImage;
    private RectTransform rect;
    private Camera mainCamera;

    private void Start()
    {
        iconImage = GetComponent<Image>();
        mainCamera = Camera.main;
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        InteractOM.OnInteractable += SetInteractable;
        InteractOM.OnInteractPosition += SetWorldPosition;
    }

    private void Update()
    {
        if (isInteractable)
            rect.position = mainCamera.WorldToScreenPoint(worldPosition);
    }

    private void SetWorldPosition(Vector3 obj)
    {
        worldPosition = obj;
    }

    private void SetInteractable(bool obj)
    {
        isInteractable = obj;
        iconImage.color = obj ? Color.white : Color.clear;
    }
    
    
}
