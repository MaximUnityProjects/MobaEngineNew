using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class negj : MonoBehaviour {
    new Camera camera = null;
    public float distance = 0;
    // Use this for initialization
    void Start() {
        camera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {

        //GetCameraSize(Camera.main);
        print(camera.rect);
    }


    static void GetCameraSize(Camera camera) {
        var zeroPos = camera.ScreenToWorldPoint(Vector3.zero);
        var width = (zeroPos - camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).magnitude;
        var height = (zeroPos - camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).magnitude;
        print(width + " " + height);
    }
}
