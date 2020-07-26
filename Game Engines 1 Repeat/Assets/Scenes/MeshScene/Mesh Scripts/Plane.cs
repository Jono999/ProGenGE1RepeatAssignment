using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [Range(2, 256)] 
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    Face[] terrainFaces;

    //private void OnValidate()
    private void Awake()
    {
        Initialise();
        GenerateMesh();
    }

    void Initialise()
    {
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6]; 
        }
       
        terrainFaces = new Face[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObject = new GameObject("mesh");

                meshObject.transform.parent = transform;

                meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                //meshFilters[i].sharedMesh = new Mesh();
                meshFilters[i].mesh = new Mesh();
  
               // terrainFaces[i] = new Face(meshFilters[i].mesh, resolution, directions[i]);
               // StartCoroutine(Wait());
               // terrainFaces[i].ConstructMesh();
            }
            
            //terrainFaces[i] = new Face(meshFilters[i].sharedMesh, resolution, directions[i]);
            terrainFaces[i] = new Face(meshFilters[i].mesh, resolution, directions[i]);
            //StartCoroutine(Wait());
        }
    }
    void GenerateMesh()
    {
        foreach (Face face in terrainFaces)
        {
            face.ConstructMesh();
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
   
}
