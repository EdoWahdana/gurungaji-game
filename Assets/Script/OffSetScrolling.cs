using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class OffSetScrolling : MonoBehaviour
{
    public RawImage img;
    public float speed;
    void Update() {
        Rect rect = img.uvRect;
        rect.x += speed;
        img.uvRect = rect; 
    }
}