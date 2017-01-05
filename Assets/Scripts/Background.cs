using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
    // Use this for initialization
    GameObject plane;
    void Start () {
        plane = GameObject.Find("background");
    }
    
    // Update is called once per frame
    void Update () {
        plane.transform.Rotate(0, 2 ,0);
    }
}
