using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProGenPlane : MonoBehaviour
{
    [Range(2, 256)] 
    public int resolution = 4;

    [SerializeField, HideInInspector] MeshFilter meshFilter;
    Face terrainFace;

    //private void OnValidate()
    private void Awake()
    {
        Initialise();
    }

    void Initialise()
    {
        if (meshFilter == null)
        {
            meshFilter = new MeshFilter();
        }

        //Face[] terrainFaces = new Face[6];
        //terrainFace = new Face();
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        //for (int i = 0; i < 6; i++)
        // {
        if (meshFilter == null)
        {
            GameObject meshObject = new GameObject("mesh");

            meshObject.transform.parent = transform;

            meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilter = meshObject.AddComponent<MeshFilter>();
            //meshFilters[i].sharedMesh = new Mesh();
            meshFilter.mesh = new Mesh();
            
            terrainFace = new Face(meshFilter.mesh, resolution, directions[0]);
            terrainFace.ConstructMesh();


            // terrainFaces[i] = new Face(meshFilters[i].mesh, resolution, directions[i]);
            // StartCoroutine(Wait());
            // terrainFaces[i].ConstructMesh();
        }

        //terrainFaces[i] = new Face(meshFilters[i].sharedMesh, resolution, directions[i]);
       // terrainFace = new Face(meshFilter.mesh, resolution, directions[1]);
       // terrainFace.ConstructMesh();
        //StartCoroutine(Wait());
        // }
    }
}
/* private void Awake()
{
 StartCoroutine(Initialise());
 public IEnumerator Initialise()
 WaitForSeconds wait = new WaitForSeconds(.05f);
 yield return wait;
 
 IEnumerator Wait()
 {
     yield return new WaitForSeconds(15.0f);
     StartCoroutine(Wait());
 }
 

}*/




