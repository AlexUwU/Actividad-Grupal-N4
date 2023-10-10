using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using System;

public class CameraFace : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture camTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect myFace;
    public float ejey;
    public float ejex;
    //private OpenCvSharp.Rect[] faces;
    bool value = true;
    public float primerY = 0.0f;
    public float primerX = 0.0f;
    public GameObject player;
    public float speed = 1.0f;
    Vector3 inicial;

    void Start()
    {
        inicial = player.transform.position;
        WebCamDevice[] devices = WebCamTexture.devices;
        camTexture = new WebCamTexture(devices[0].name);
        cascade = new CascadeClassifier(Application.dataPath+ @"/haarcascade_frontalface_default.xml");
        camTexture.Play();
        //faces = cascade.DetectMultiScale(OpenCvSharp.Unity.TextureToMat(camTexture), 1.1, 2, HaarDetectionType.ScaleImage);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture = camTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(camTexture);
        FindCara(frame);
        DisplayFrame(frame);
    }

    void FindCara(Mat frame) {
       var faces = cascade.DetectMultiScale(frame,1.1,2,HaarDetectionType.ScaleImage);
        if (faces.Length > 0) {
            if (value) {
                primerY = faces[0].Y;
                primerX = faces[0].X;
                value = false;
            }
            myFace = faces[0];
            ejey = faces[0].Y;
            ejex = faces[0].X;

            float step = speed * Time.deltaTime;
            float norm = Mathf.Clamp(primerY - ejey, -3, 3);
            float normx = Mathf.Clamp(primerX - ejex, -3, 3);
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(inicial.x - normx , inicial.y + norm, inicial.z), step * 10);

        }
    }

    private void DisplayFrame(Mat frame)
    {
        if (myFace != null) {
            frame.Rectangle(myFace, new Scalar(255, 0, 0), 2); 
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;  
    }
}
