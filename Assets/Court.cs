using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Court : MonoBehaviour
{
    private const int PLANE_SIZE = 1000;
    private const int PLANE_THICKNESS = 1;
    private int size;

    private float x, z;
    
    private GameObject[] walls = new GameObject[16];
    private GameObject[] corners = new GameObject[4];
    private GameObject floor;
    public Material wall01;
    public Material floor01;

    public float getWidth()
    {
        return z;
    }

    public float getLength()
    {
        return x;
    }

    // Start is called before the first frame update
    void Start()
    {
        size = GameControl.gameControl.matchSettings.courtSize;

        if (size == 0)
        {
            x = 100;
            z = 65;
        }
        else if (size == 1)
        {
            x = 200;
            z = 100;
        }
        else if (size == 2)
        {
            x = 300;
            z = 150;
        }
        else if (size == 3)
        {
            x = 400;
            z = 200;
        }

        createFloor();
        createMarkings();
        createWalls();
        createTower1();
        createTower2();
        createTower3();
        createTower4();
    }

    private void createTower1()
    {
        GameObject walls = new GameObject("Walls");
        MeshFilter meshFilter = (MeshFilter)walls.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        Vector3[] vertices = new Vector3[] {
            //1
            glVertex3f(-x/3f*2f, z*2, 0),
            glVertex3f(-x/3f*2f, z*2, 200),
            glVertex3f(-x/3f*2f, z+40, 0),
            glVertex3f(-x/3f*2f, z*1.5f+20f, 200),
            //2
            glVertex3f(-x/3f*2f, z+40, 0),
            glVertex3f(-x/3f*2f, z*1.5f+20f, 200),
            glVertex3f(-x/3f*1f, z+40, 0),
            glVertex3f(-x/3f*1f, z*1.5f+20f, 200),
            //3
            glVertex3f(-x/3f*1f, z+40, 0),
            glVertex3f(-x/3f*1f, z*1.5f+20f, 200),
            glVertex3f(-x/3f*1f, z*2, 0),
            glVertex3f(-x/3f*1f, z*2, 200)
        };
        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            3,0,2,  1,0,3,      //1
            5,6,4,  7,6,5,      //2
            9,10,8, 11,10,9     //3
        };
        mesh.uv = new Vector2[]
        {
            glTexCoord2f(0, 0),
            glTexCoord2f(0, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(3, 0),
            glTexCoord2f(3, 2)
        };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = walls.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
    }

    private void createTower2()
    {
        GameObject walls = new GameObject("Walls");
        MeshFilter meshFilter = (MeshFilter)walls.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        Vector3[] vertices = new Vector3[] {
            //1
            glVertex3f(x/3f*2f, z*2, 0),
            glVertex3f(x/3f*2f, z*2, 200),
            glVertex3f(x/3f*2f, z+40, 0),
            glVertex3f(x/3f*2f, z*1.5f+20f, 200),
            //2
            glVertex3f(x/3f*2f, z+40, 0),
            glVertex3f(x/3f*2f, z*1.5f+20f, 200),
            glVertex3f(x/3f*1f, z+40, 0),
            glVertex3f(x/3f*1f, z*1.5f+20f, 200),
            //3
            glVertex3f(x/3f*1f, z+40, 0),
            glVertex3f(x/3f*1f, z*1.5f+20f, 200),
            glVertex3f(x/3f*1f, z*2, 0),
            glVertex3f(x/3f*1f, z*2, 200)
        };
        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            2,0,3,  3,0,1,      //1
            4,6,5,  5,6,7,      //2
            8,10,9, 9,10,11     //3
        };

        mesh.uv = new Vector2[]
        {
            glTexCoord2f(0, 0),
            glTexCoord2f(0, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(3, 0),
            glTexCoord2f(3, 2)
        };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = walls.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
    }

    private void createTower3()
    {
        GameObject walls = new GameObject("Walls");
        MeshFilter meshFilter = (MeshFilter)walls.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        Vector3[] vertices = new Vector3[] {
            //1
            glVertex3f(-x/3f*2f, -z*2, 0),
            glVertex3f(-x/3f*2f, -z*2, 200),
            glVertex3f(-x/3f*2f, -z-40, 0),
            glVertex3f(-x/3f*2f, -z*1.5f-20f, 200),
            //2
            glVertex3f(-x/3f*2f, -z-40, 0),
            glVertex3f(-x/3f*2f, -z*1.5f-20f, 200),
            glVertex3f(-x/3f*1f, -z-40, 0),
            glVertex3f(-x/3f*1f, -z*1.5f-20f, 200),
            //3
            glVertex3f(-x/3f*1f, -z-40, 0),
            glVertex3f(-x/3f*1f, -z*1.5f-20f, 200),
            glVertex3f(-x/3f*1f, -z*2, 0),
            glVertex3f(-x/3f*1f, -z*2, 200)
        };
        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            2,0,3,  3,0,1,      //1
            4,6,5,  5,6,7,      //2
            8,10,9, 9,10,11     //3
        };
        mesh.uv = new Vector2[]
        {
            glTexCoord2f(0, 0),
            glTexCoord2f(0, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(3, 0),
            glTexCoord2f(3, 2)
        };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = walls.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
    }

    private void createTower4()
    {
        GameObject walls = new GameObject("Walls");
        MeshFilter meshFilter = (MeshFilter)walls.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        Vector3[] vertices = new Vector3[] {
            //1
            glVertex3f(x/3f*2f, -z*2, 0),
            glVertex3f(x/3f*2f, -z*2, 200),
            glVertex3f(x/3f*2f, -z-40, 0),
            glVertex3f(x/3f*2f, -z*1.5f-20f, 200),
            //2
            glVertex3f(x/3f*2f, -z-40, 0),
            glVertex3f(x/3f*2f, -z*1.5f-20f, 200),
            glVertex3f(x/3f*1f, -z-40, 0),
            glVertex3f(x/3f*1f, -z*1.5f-20f, 200),
            //3
            glVertex3f(x/3f*1f, -z-40, 0),
            glVertex3f(x/3f*1f, -z*1.5f-20f, 200),
            glVertex3f(x/3f*1f, -z*2, 0),
            glVertex3f(x/3f*1f, -z*2, 200)
        };
        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            3,0,2,  1,0,3,      //1
            5,6,4,  7,6,5,      //2
            9,10,8, 11,10,9     //3
        };
        mesh.uv = new Vector2[]
        {
            glTexCoord2f(0, 0),
            glTexCoord2f(0, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(1, 0),
            glTexCoord2f(1, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(2, 0),
            glTexCoord2f(2, 2),
            glTexCoord2f(3, 0),
            glTexCoord2f(3, 2)
        };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = walls.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
    }


    private void createWalls()
    {
        GameObject walls = new GameObject("Walls");
        MeshFilter meshFilter = (MeshFilter)walls.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        Vector3[] vertices = new Vector3[] {
            //1
            glVertex3f(-x+28.48581f, z, 0),
            glVertex3f(-x+28.48581f, z, 30),
            glVertex3f(x-28.48581f, z, 0),
            glVertex3f(x-28.48581f, z, 30),
            //2
            glVertex3f(x-28.48581f, z, 0),
            glVertex3f(x-28.48581f, z, 30),
            glVertex3f(x-14, z-6, 0),
            glVertex3f(x-14, z-6, 30),
            //3
            glVertex3f(x-14, z-6, 0),
            glVertex3f(x-14, z-6, 30),
            glVertex3f(x-6, z-14, 0),
            glVertex3f(x-6, z-14, 30),
            //4
            glVertex3f(x-6, z-14, 0),
            glVertex3f(x-6, z-14, 30),
            glVertex3f(x, z-28.48581f, 0),
            glVertex3f(x, z-28.48581f, 30),
            //5
            glVertex3f(x, z-28.48581f, 0),
            glVertex3f(x, z-28.48581f, 30),
            glVertex3f(x, 35, 0),
            glVertex3f(x, 35, 30),
            //6
            glVertex3f(x, 35, 0),
            glVertex3f(x, 35, 30),
            glVertex3f(x+30, 35, 0),
            glVertex3f(x+30, 35, 30),
            //7
            glVertex3f(x+30, 35, 0),
            glVertex3f(x+30, 35, 30),
            glVertex3f(x+30, -35, 0),
            glVertex3f(x+30, -35, 30),
            //8
            glVertex3f(x+30, -35, 0),
            glVertex3f(x+30, -35, 30),
            glVertex3f(x, -35, 0),
            glVertex3f(x, -35, 30),
            //9
            glVertex3f(x, -35, 0),
            glVertex3f(x, -35, 30),
            glVertex3f(x, -z+28.48581f, 0),
            glVertex3f(x, -z+28.48581f, 30),
            //10
            glVertex3f(x, -z+28.48581f, 0),
            glVertex3f(x, -z+28.48581f, 30),
            glVertex3f(x-6, -z+14, 0),
            glVertex3f(x-6, -z+14, 30),
            //11
            glVertex3f(x-6, -z+14, 0),
            glVertex3f(x-6, -z+14, 30),
            glVertex3f(x-14, -z+6, 0),
            glVertex3f(x-14, -z+6, 30),
            //12
            glVertex3f(x-14, -z+6, 0),
            glVertex3f(x-14, -z+6, 30),
            glVertex3f(x-28.48581f, -z, 0),
            glVertex3f(x-28.48581f, -z, 30),
            //13
            glVertex3f(x-28.48581f, -z, 0),
            glVertex3f(x-28.48581f, -z, 30),
            glVertex3f(-x+28.48581f, -z, 0),
            glVertex3f(-x+28.48581f, -z, 30),
            //14
            glVertex3f(-x+28.48581f, -z, 0),
            glVertex3f(-x+28.48581f, -z, 30),
            glVertex3f(-x+14, -z+6, 0),
            glVertex3f(-x+14, -z+6, 30),
            //15
            glVertex3f(-x+14, -z+6, 0),
            glVertex3f(-x+14, -z+6, 30),
            glVertex3f(-x+6, -z+14, 0),
            glVertex3f(-x+6, -z+14, 30),
            //16
            glVertex3f(-x+6, -z+14, 0),
            glVertex3f(-x+6, -z+14, 30),
            glVertex3f(-x, -z+28.48581f, 0),
            glVertex3f(-x, -z+28.48581f, 30),
            //17
            glVertex3f(-x, -z+28.48581f, 0),
            glVertex3f(-x, -z+28.48581f, 30),
            glVertex3f(-x, -35, 0),
            glVertex3f(-x, -35, 30),
            //18
            glVertex3f(-x,      -35, 0),
            glVertex3f(-x,      -35, 30),
            glVertex3f(-x-30,   -35, 0),
            glVertex3f(-x-30,   -35, 30),
            //19
            glVertex3f(-x-30,  -35, 0),     //-40, 0
            glVertex3f(-x-30,  -35, 30),    //-40, 30
            glVertex3f(-x-30,   35, 0),     //-40, 0
            glVertex3f(-x-30,   35, 30),    //-40, 30
            //20
            glVertex3f(-x-30, 35, 0),
            glVertex3f(-x-30, 35, 30),
            glVertex3f(-x, 35, 0),
            glVertex3f(-x, 35, 30),
            //21
            glVertex3f(-x, 35, 0),
            glVertex3f(-x, 35, 30),
            glVertex3f(-x, z-28.48581f, 0),
            glVertex3f(-x, z-28.48581f, 30),
            //22
            glVertex3f(-x, z-28.48581f, 0),
            glVertex3f(-x, z-28.48581f, 30),
            glVertex3f(-x+6, z-14, 0),
            glVertex3f(-x+6, z-14, 30),
            //23
            glVertex3f(-x+6, z-14, 0),
            glVertex3f(-x+6, z-14, 30),
            glVertex3f(-x+14, z-6, 0),
            glVertex3f(-x+14, z-6, 30),
            //24
            glVertex3f(-x+14, z-6, 0),
            glVertex3f(-x+14, z-6, 30),
            glVertex3f(-x+28.48581f, z, 0),
            glVertex3f(-x+28.48581f, z, 30)
        };
        mesh.vertices = vertices;
        mesh.triangles = new int[] {
            1,2,0,      3,2,1,      //1
            5,6,4,      7,6,5,      //2
            9,10,8,     11,10,9,    //3
            13,14,12,   15,14,13,   //4
            18,16,19,   19,16,17,   //5
            21,22,20,   23,22,21,   //6
            26,24,27,   27,24,25,   //7
            30,28,31,   31,28,29,   //8
            34,32,35,   35,32,33,   //9
            38,36,39,   39,36,37,   //10
            42,40,43,   43,40,41,   //11
            46,44,47,   47,44,45,   //12
            50,48,51,   51,48,49,   //13
            54,52,55,   55,52,53,   //14
            58,56,59,   59,56,57,   //15
            62,60,63,   63,60,61,   //16
            65,66,64,   67,66,65,   //17
            68,71,70,   69,71,68,   //18
            73,74,72,   75,74,73,   //19
            77,78,76,   79,78,77,   //20
            81,82,80,   83,82,81,   //21
            85,86,84,   87,86,85,   //22
            89,90,88,   91,90,89,   //23
            93,94,92,   95,94,93    //24
        };
        mesh.uv = new Vector2[]
        {
            glTexCoord2f(0, 0),
            glTexCoord2f(0, 1),
            glTexCoord2f(15, 0),
            glTexCoord2f(15, 1),
            glTexCoord2f(15, 0),
            glTexCoord2f(15, 1),
            glTexCoord2f(16, 0),
            glTexCoord2f(16, 1),
            glTexCoord2f(16, 0),
            glTexCoord2f(16, 1),
            glTexCoord2f(17, 0),
            glTexCoord2f(17, 1),
            glTexCoord2f(17, 0),
            glTexCoord2f(17, 1),
            glTexCoord2f(18, 0),
            glTexCoord2f(18, 1),
            glTexCoord2f(18, 0),
            glTexCoord2f(18, 1),
            glTexCoord2f(21, 0),
            glTexCoord2f(21, 1),
            glTexCoord2f(21, 0),
            glTexCoord2f(21, 1),
            glTexCoord2f(23, 0),
            glTexCoord2f(23, 1),
            glTexCoord2f(23, 0),
            glTexCoord2f(23, 1),
            glTexCoord2f(25, 0),
            glTexCoord2f(25, 1),
            glTexCoord2f(25, 0),
            glTexCoord2f(25, 1),
            glTexCoord2f(27, 0),
            glTexCoord2f(27, 1),
            glTexCoord2f(27, 0),
            glTexCoord2f(27, 1),
            glTexCoord2f(29, 0),
            glTexCoord2f(29, 1),
            glTexCoord2f(29, 0),
            glTexCoord2f(29, 1),
            glTexCoord2f(32, 0),
            glTexCoord2f(32, 1),
            glTexCoord2f(32, 0),
            glTexCoord2f(32, 1),
            glTexCoord2f(33, 0),
            glTexCoord2f(33, 1),
            glTexCoord2f(33, 0),
            glTexCoord2f(33, 1),
            glTexCoord2f(34, 0),
            glTexCoord2f(34, 1),
            glTexCoord2f(34, 0),
            glTexCoord2f(34, 1),
            glTexCoord2f(49, 0),
            glTexCoord2f(49, 1),
            glTexCoord2f(49, 0),
            glTexCoord2f(49, 1),
            glTexCoord2f(50, 0),
            glTexCoord2f(50, 1),
            glTexCoord2f(50, 0),
            glTexCoord2f(50, 1),
            glTexCoord2f(51, 0),
            glTexCoord2f(51, 1),
            glTexCoord2f(51, 0),
            glTexCoord2f(51, 1),
            glTexCoord2f(52, 0),
            glTexCoord2f(52, 1),
            glTexCoord2f(52, 0),
            glTexCoord2f(52, 1),
            glTexCoord2f(54, 0),
            glTexCoord2f(54, 1),
            glTexCoord2f(54, 0),
            glTexCoord2f(54, 1),
            glTexCoord2f(56, 0),
            glTexCoord2f(56, 1),
            glTexCoord2f(56, 0),
            glTexCoord2f(56, 1),
            glTexCoord2f(59, 0),
            glTexCoord2f(59, 1),
            glTexCoord2f(59, 0),
            glTexCoord2f(59, 1),
            glTexCoord2f(61, 0),
            glTexCoord2f(61, 1),
            glTexCoord2f(61, 0),
            glTexCoord2f(61, 1),
            glTexCoord2f(63, 0),
            glTexCoord2f(63, 1),
            glTexCoord2f(63, 0),
            glTexCoord2f(63, 1),
            glTexCoord2f(64, 0),
            glTexCoord2f(64, 1),
            glTexCoord2f(64, 0),
            glTexCoord2f(64, 1),
            glTexCoord2f(65, 0),
            glTexCoord2f(65, 1),
            glTexCoord2f(65, 0),
            glTexCoord2f(65, 1),
            glTexCoord2f(66, 0),
            glTexCoord2f(66, 1)
    };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = walls.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
        walls.AddComponent<MeshCollider>();
    }

    private Vector2 glTexCoord2f(float v1, float v2)
    {
        return new Vector2(v1, v2);
    }

    private void logTriangles(int[] triangles)
    {
        string triangleString = "";
        foreach(int triangle in triangles)
        {
            triangleString = triangleString + "" + triangle+", ";
        }


        Debug.Log(triangleString);
    }

    private int[] dummyTriangles()
    {
        int[] triangles = new int[144];
        int vertexStart = 0;
        for(int i=0;i<144;i=i+6)
        {
            triangles[i+0] = 0+ vertexStart;
            triangles[i+1] = 3 + vertexStart;
            triangles[i+2] = 1 + vertexStart;

            triangles[i+3] = 1 + vertexStart;
            triangles[i+4] = 3 + vertexStart;
            triangles[i+5] = 2 + vertexStart;
            vertexStart = vertexStart + 4;
        }
        return triangles;
    }

    private int[] calculateTrianglesFromQuads(Vector3[] vertices)
    {
        int[] triangles = new int[vertices.Length / 2 * 3];
        int triangleIndex = 0;
        for (int i = 0; i < vertices.Length; i = i + 4)
        {
            Vector3[] currentVertices = new Vector3[4];
            currentVertices[0] = vertices[i+0];
            currentVertices[1] = vertices[i+1];
            currentVertices[2] = vertices[i+2];
            currentVertices[3] = vertices[i+3];

            Vector3[] sortedVertices = new Vector3[4];
            Array.Copy(currentVertices, sortedVertices, 4);
            sortVertices(sortedVertices);

            int[] vertexPositions = new int[4];
            int vertexPositionIndex = 0;
            foreach (Vector3 sortedVertex in sortedVertices)
            {
                int vertexPos = Array.IndexOf(currentVertices, sortedVertex);
                vertexPositions[vertexPositionIndex] = vertexPos;
                vertexPositionIndex = vertexPositionIndex + 1;
            }

            if(allZsBiggerThanZero(currentVertices) || allXSmallerThanZero(currentVertices))
            {
                triangles[triangleIndex] = vertexPositions[1] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[3] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[0] + i;
                triangleIndex = triangleIndex + 1;

                triangles[triangleIndex] = vertexPositions[2] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[3] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[1] + i;
                triangleIndex = triangleIndex + 1;
            }
            else
            {
                triangles[triangleIndex] = vertexPositions[0] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[3] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[1] + i;
                triangleIndex = triangleIndex + 1;

                triangles[triangleIndex] = vertexPositions[1] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[3] + i;
                triangleIndex = triangleIndex + 1;
                triangles[triangleIndex] = vertexPositions[2] + i;
                triangleIndex = triangleIndex + 1;
            }


        }
        return triangles;
    }

    private bool allZsBiggerThanZero(Vector3[] currentVertices)
    {
        foreach(Vector3 currentVertex in currentVertices)
        {
            if (currentVertex.z < 0) return false;
        }
        return true;
    }

    private bool allXSmallerThanZero(Vector3[] currentVertices)
    {
        foreach (Vector3 currentVertex in currentVertices)
        {
            if (currentVertex.x > 0) return false;
        }
        return true;
    }

    private void sortVertices(Vector3[] sortedVertices)
    {
        Array.Sort(sortedVertices, delegate (Vector3 a, Vector3 b) {
            int firstComparison = a.x.CompareTo(b.x);
            if (firstComparison != 0) return firstComparison;
            else return a.z.CompareTo(b.z);
        });
        Vector3 downLeft = sortedVertices[0];
        Vector3 downRight = sortedVertices[1];
        Vector3 upRight = sortedVertices[3];
        Vector3 upLeft = sortedVertices[2];
        sortedVertices[0] = downLeft;
        sortedVertices[1] = downRight;
        sortedVertices[2] = upRight;
        sortedVertices[3] = upLeft;
    }

    private Vector3 glVertex3f(float x, float z, float y)
    {
        return new Vector3(x,y,z);
    }

    private void createFloor()
    {
        GameObject floor = new GameObject("Floor");
        MeshFilter meshFilter = (MeshFilter)floor.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        mesh.vertices = new Vector3[] {
            new Vector3(-x-300, 0, -z-300),
            new Vector3(x+300, 0, -z-300),
            new Vector3(x+300, 0, z+300),
            new Vector3(-x-300, 0, z+300)
        };

        mesh.uv = new Vector2[] {
         new Vector2 (0, 0),
         new Vector2 (10, 0),
         new Vector2(10, 10),
         new Vector2 (0, 10)
        };
        mesh.triangles = new int[] { 0, 3, 1, 1, 3, 2 };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = floor.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        floor.AddComponent(typeof(MeshCollider));
        renderer.material = floor01;
    }

    private void createMarkings()
    {
        GameObject markings = new GameObject("Markings");
        MeshFilter meshFilter = (MeshFilter)markings.AddComponent(typeof(MeshFilter));
        Mesh mesh = new Mesh();
        mesh.name = "ScriptedMesh";
        mesh.vertices = new Vector3[] {
            new Vector3(-1, 0.01f, -z),
            new Vector3(1, 0.01f, -z),
            new Vector3(1, 0.01f, z),
            new Vector3(-1, 0.01f, z),

            new Vector3(-x,         0.01f,  z / 2.0f),          // -10, 5
            new Vector3(-x / 1.5f,  0.01f,  z / 2.0f),          // -6,  5
            new Vector3(-x / 1.5f,  0.01f,  z / 2.0f - 1.0f),   // -6,  4
            new Vector3(-x,         0.01f,  z / 2.0f - 1.0f),   // -10, 4
            
            new Vector3(-x, 0.01f,-z / 2.0f),                   // -10, -5
            new Vector3(-x / 1.5f, 0.01f,-z / 2.0f),            // -6,  -5
            new Vector3(-x / 1.5f, 0.01f,-z / 2.0f + 1.0f),     // -6,  -4
            new Vector3(-x, 0.01f,-z / 2 + 1.0f),               // -10, -4

            new Vector3(-x / 1.5f, 0.01f,z / 2.0f),             // -6, 5
            new Vector3(-x / 1.5f + 1.0f, 0.01f,z / 2.0f),      // -5, 5
            new Vector3(-x / 1.5f + 1.0f, 0.01f,-z / 2.0f),     // -5, -5
            new Vector3(-x / 1.5f, 0.01f,-z / 2.0f),            // -6, -5
            
            new Vector3(x, 0.01f,z / 2.0f),                     // 10,  5
            new Vector3(x / 1.5f, 0.01f,z / 2.0f),              // 6,   5
            new Vector3(x / 1.5f, 0.01f,z / 2.0f - 1.0f),       // 6,   4
            new Vector3(x, 0.01f,z / 2.0f - 1.0f),              // 10,  4
            
            new Vector3(x, 0.01f,-z / 2.0f),                    // 10, -5
            new Vector3(x / 1.5f, 0.01f,-z / 2.0f),             // 6, -5
            new Vector3(x / 1.5f, 0.01f,-z / 2.0f + 1.0f),      // 6, -4
            new Vector3(x, 0.01f,-z / 2.0f + 1.0f),             // 10, -4
            
            new Vector3(x / 1.5f, 0.01f,z / 2.0f),
            new Vector3(x / 1.5f + 1.0f, 0.01f,z / 2.0f),
            new Vector3(x / 1.5f + 1.0f, 0.01f,-z / 2.0f),
            new Vector3(x / 1.5f, 0.01f,-z / 2.0f),

            new Vector3(-x - 1.0f, 0.01f,-35),
            new Vector3(-x, 0.01f,-35),
            new Vector3(-x, 0.01f,35),
            new Vector3(-x - 1.0f, 0.01f,35),

            new Vector3(x, 0.01f,-35),
            new Vector3(x + 1.0f, 0.01f,-35),
            new Vector3(x + 1.0f, 0.01f,35),
            new Vector3(x, 0.01f,35)
        };


        mesh.triangles = new int[] {   0, 3, 1, 1, 3, 2,
                                        7, 4, 6, 6, 4, 5,
                                        8, 11, 9, 9, 11, 10,
                                        15, 12, 14, 14, 12, 13,
                                        18, 17, 19, 19, 17, 16,
                                        20, 21, 22, 20, 22, 23,
                                        26, 27, 24, 26, 24, 25,
                                        28, 31, 29, 29, 31, 30,
                                        32, 35, 33, 33, 35, 34};
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        MeshRenderer renderer = markings.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = new Material(Shader.Find("Specular"));
    }

    void CreateStuff()
    {
        walls[0] = CreatePlane(0, -1, 0, -z);
        walls[1] = CreatePlane(0, 1, 0, -z);

        walls[2] = CreatePlane(Mathf.Cos(Mathf.PI / 4.0f), -Mathf.Sin(Mathf.PI / 4.0f), 0, Mathf.Cos(Mathf.PI / 4.0f) * -x - Mathf.Sin(Mathf.PI / 4.0f) * (z - 20.0f));
        walls[3] = CreatePlane(Mathf.Cos(Mathf.PI / 8.0f), -Mathf.Sin(Mathf.PI / 8.0f), 0, Mathf.Cos(Mathf.PI / 8.0f) * (-x + 6.0f) - Mathf.Sin(Mathf.PI / 8.0f) * (z - 14.0f));
        walls[4] = CreatePlane(Mathf.Sin(Mathf.PI / 8.0f), -Mathf.Cos(Mathf.PI / 8.0f), 0, Mathf.Sin(Mathf.PI / 8.0f) * (-x + 14.0f) - Mathf.Cos(Mathf.PI / 8.0f) * (z - 6.0f));

        walls[5] = CreatePlane(-Mathf.Cos(Mathf.PI / 4.0f), -Mathf.Sin(Mathf.PI / 4.0f), 0, Mathf.Cos(Mathf.PI / 4.0f) * -x - Mathf.Sin(Mathf.PI / 4.0f) * (z - 20));
        walls[6] = CreatePlane(-Mathf.Cos(Mathf.PI / 8.0f), -Mathf.Sin(Mathf.PI / 8.0f), 0, Mathf.Cos(Mathf.PI / 8.0f) * (-x + 6.0f) - Mathf.Sin(Mathf.PI / 8.0f) * (z - 14.0f));
        walls[7] = CreatePlane(-Mathf.Sin(Mathf.PI / 8.0f), -Mathf.Cos(Mathf.PI / 8.0f), 0, Mathf.Sin(Mathf.PI / 8.0f) * (-x + 14.0f) - Mathf.Cos(Mathf.PI / 8.0f) * (z - 6.0f));

        walls[8] = CreatePlane(Mathf.Cos(Mathf.PI / 4.0f), Mathf.Sin(Mathf.PI / 4.0f), 0, Mathf.Cos(Mathf.PI / 4.0f) * -x - Mathf.Sin(Mathf.PI / 4.0f) * (z - 20));
        walls[9] = CreatePlane(Mathf.Cos(Mathf.PI / 8.0f), Mathf.Sin(Mathf.PI / 8.0f), 0, Mathf.Cos(Mathf.PI / 8.0f) * (-x + 6.0f) - Mathf.Sin(Mathf.PI / 8.0f) * (z - 14.0f));
        walls[10] = CreatePlane(Mathf.Sin(Mathf.PI / 8.0f), Mathf.Cos(Mathf.PI / 8.0f), 0, Mathf.Sin(Mathf.PI / 8.0f) * (-x + 14.0f) - Mathf.Cos(Mathf.PI / 8.0f) * (z - 6.0f));

        walls[11] = CreatePlane(-Mathf.Cos(Mathf.PI / 4.0f), Mathf.Sin(Mathf.PI / 4.0f), 0, Mathf.Cos(Mathf.PI / 4.0f) * -x - Mathf.Sin(Mathf.PI / 4.0f) * (z - 20.0f));
        walls[12] = CreatePlane(-Mathf.Cos(Mathf.PI / 8.0f), Mathf.Sin(Mathf.PI / 8.0f), 0, Mathf.Cos(Mathf.PI / 8.0f) * (-x + 6.0f) - Mathf.Sin(Mathf.PI / 8.0f) * (z - 14.0f));
        walls[13] = CreatePlane(-Mathf.Sin(Mathf.PI / 8.0f), Mathf.Cos(Mathf.PI / 8.0f), 0, Mathf.Sin(Mathf.PI / 8.0f) * (-x + 14.0f) - Mathf.Cos(Mathf.PI / 8.0f) * (z - 6.0f));

        walls[14] = CreatePlane(1, 0, 0, -x - 30);
        walls[15] = CreatePlane(-1, 0, 0, -x - 30);

        int wallIndex = 0;
        foreach (GameObject wall in walls)
        {
            wall.name = "" + wallIndex;
            wallIndex++;
        }

        int i;
        for (i = 0; i < 4; i++) corners[i] = CreateBox(1000, 1000, 1000);
        corners[0].transform.position = new Vector3(-x - 500, 0, 500 + 35);
        corners[1].transform.position = new Vector3(-x - 500, 0, -500 - 35);
        corners[2].transform.position = new Vector3(x + 500, 0, 500 + 35);
        corners[3].transform.position = new Vector3(x + 500, 0, -500 - 35);

        floor = CreatePlane(0, 0, 1, 0);
        floor.name = "Floor";
    }

    GameObject CreateBox(float x, float z, float y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Corner";
        cube.transform.localScale = new Vector3(x, y, z);  
        return cube;
    }

    GameObject CreatePlane(float a, float c, float b, float d)
    {
        float divisor = a * a + b * b + c * c;
        Vector3 center = new Vector3(a*d / divisor, b*d / divisor, c*d / divisor);

        GameObject plane = new GameObject("Plane");
        MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = CreatePlaneMesh(new Vector3(a, b, c), center);
        MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = wall01;
        plane.transform.position = center;
        return plane;
    }

    Mesh CreatePlaneMesh(Vector3 normal, Vector3 center)
    {
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";


        Vector3 v1;
        if (Vector3.Dot(normal, Vector3.up) > 0.8f)
            v1 = Vector3.Cross(normal, Vector3.right);
        else
            v1 = Vector3.Cross(normal, Vector3.up);
        Vector3 v2 = Vector3.Cross(normal, v1);

        v1 *= PLANE_SIZE * 0.5f;
        v2 *= PLANE_SIZE * 0.5f;

        // your vertex points:

        Vector3 p1 = center + v1 + v2;
        Vector3 p2 = center - v1 + v2;
        Vector3 p3 = center - v1 - v2;
        Vector3 p4 = center + v1 - v2;


        m.vertices = new Vector3[] {
         p1, p2, p3, p4
        };

        m.uv = new Vector2[] {
         new Vector2 (0, 0),
         new Vector2 (0, 1),
         new Vector2(1, 1),
         new Vector2 (1, 0)
     };
        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        m.RecalculateNormals();

        return m;
    }


}
