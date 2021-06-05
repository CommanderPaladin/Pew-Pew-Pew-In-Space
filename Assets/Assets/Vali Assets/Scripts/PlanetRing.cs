using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRing : MonoBehaviour
{
    [Range(3, 360)]
    public int segments = 3;
    public float innerRadius = 0.7f;
    public float thickness = 0.5f;
    public Material ringMat;

    GameObject ring;
        Mesh ringMesh;
    MeshFilter ringMF;
    MeshRenderer ringMR;

    // Start is called before the first frame update
    void Start()
    {
        ring = new GameObject(name + " Ring");
        ring.transform.parent = transform;
        ring.transform.localScale = Vector3.one;
        ring.transform.localPosition = Vector2.zero;
        ring.transform.localRotation = Quaternion.identity;
        ringMF = ring.AddComponent<MeshFilter>();
        ringMesh = ringMF.mesh;
        ringMR = ring.AddComponent<MeshRenderer>();
        ringMR.material = ringMat;

        Vector3[] vertices = new Vector3[(segments + 1) * 4];
        int[] triangles = new int[segments*6 *2];
        Vector3[] uv = new Vector3[(segments + 1)*4];
        int halfway = (segments + 1)*2;

        for(int i = 0; i<segments +1;i++)
        {
            float progress = (float)i / (float)segments;
            float angle = Mathf.Deg2Rad * progress * 360;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
