using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face
{
    //public GameObject t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18;
       // List<GameObject> golist = new List<GameObject>();
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;
    
// Constructor
    public Face(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        // Calculating other 2 directions on face based on localUp direction
        axisA = new Vector3(localUp.y, localUp.z, localUp.x); // swapping xyz coordinates of localUp to calculate a vector perpendicular to it
        axisB = Vector3.Cross(localUp, axisA); // using cross product of localUp and axisA to calculate a vector which is perpendicular to both
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(15.0f);
    }

    public void ConstructMesh()
    {
        
        // resolution = number of vertices along a single edge of the face so resolution multiplied by itself will be total number of vertices on a face
        Vector3[] vertices = new Vector3[resolution * resolution];
        // the following int array holds the indices of all of the triangles on a face mesh
        // the formula used in this calculation is;
        // resolution (number of vertices along a single edge of the face)
        // minus 1 (which gives the amount of cells made between the vertices along one edge of the face, x or y)
        // squared (which gives the amount of cells made between the vertices of the entire face, x & y, = the entire grid of vertices in the face)
        // multiplied by 2 (which accounts for each of the cells being made up by 2 triangles)
        // multiplied by 3 (which accounts for each triangle being made up of 3 vertices) - the formula just multiplies by 6 for these last 2 steps (2 * 3)
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x,y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                //Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                //vertices[i] = pointOnUnitSphere;
                vertices[i] = pointOnUnitCube;

                if (x!= resolution - 1 && y!= resolution - 1)
                
                {
                        triangles[triIndex] = i;
                        triangles[triIndex + 1] = i + resolution + 1;
                        triangles[triIndex + 2] = i + resolution;

                        triangles[triIndex + 3] = i;
                        triangles[triIndex + 4] = i + 1;
                        triangles[triIndex + 5] = i + resolution + 1;
                        triIndex += 6;
                }
                
            }
            
        }
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
    }
    
}
