using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //loop through z axis
        for (int i = 0, z = 0; z <= zSize; z++)
        {   //loop through x axis
            for (int x = 0; x <= xSize; x++)
            {   //creates a grid with all verticies

                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 2f;
                // float yx = Mathf.PerlinNoise(x * 0.7f, z * 0.75f) * 1.25f;
                // float yz = Mathf.PerlinNoise(x * 6.5f, z * 6.5f) * 9.5f;
                //yx * yz make small hills since x keeps z from being too high
                vertices[i] = new Vector3(x, y, z);
                i++;

            }
        }
        //create an array of triangles
        triangles = new int[xSize * zSize * 6];

        //to generate throughout the designates sphere area, loop
        int vert = 0;
        int tris = 0;
        //loop through z
        for (int z = 0; z < zSize; z++)
        {
            //loop through x
            for (int x = 0; x < xSize; x++)
            {
            
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //changes lighting
        mesh.RecalculateNormals();
    }

    //visualize how the terrain will be generated
    // private void OnDrawGizmos()
    // {
    //     if (vertices == null) return;

    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(vertices[i], 0.1f);
    //     }
    // }
}
