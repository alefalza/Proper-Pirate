using UnityEngine;

namespace CustomGeometry
{
    public class Sphere : Shape
    {
        [SerializeField] private int radius = 1;
        [SerializeField] [Range(1, 89)] private int divsPerQuad = 1;

        private int TotalDivs => divsPerQuad * 4 + 4;

        protected override void GenerateVertices()
        {
            int arrayLength = TotalDivs * (divsPerQuad * 2 + 1) + 2;
            _vertices = new Vector3[arrayLength];
            
            float degPerDiv = 360f / TotalDivs;
            float radPerDiv = Mathf.PI / 180 * degPerDiv;
            
            int vertexCount = 0;
            
            for (int q = -divsPerQuad; q <= divsPerQuad; q++)
            {
                float radiusFactor = radius * Mathf.Cos(radPerDiv * q);
                float yPos = radius * Mathf.Sin(radPerDiv * q);

                for (int i = 0; i < TotalDivs; i++)
                {
                    float xPos = radiusFactor * Mathf.Cos(radPerDiv * i);
                    float zPos = radiusFactor * Mathf.Sin(radPerDiv * i) * -1;

                    Vector3 vertex = new Vector3(xPos, yPos, zPos);
                    _vertices[vertexCount] = vertex;
                    vertexCount++;
                }
            }
            
            Vector3 topVertex = Vector3.up * radius;
            _vertices[vertexCount] = topVertex;
            vertexCount++;
            
            Vector3 bottomVertex = Vector3.down * radius;
            _vertices[vertexCount] = bottomVertex;
        }

        protected override void GenerateTriangles()
        {
            int arrayLength = TotalDivs * 6 * divsPerQuad * 2;
            arrayLength += TotalDivs * 3;
            arrayLength += TotalDivs * 3;
            _triangles = new int[arrayLength];
            
            int k;
            int tris = 0;
            
            // Middle triangles
            for (k = 0; k < divsPerQuad * 2; k++)
            {
                for (int i = 0; i < TotalDivs; i++)
                {
                    _triangles[tris + 0] = k * TotalDivs + i;
                    _triangles[tris + 1] = k * TotalDivs + TotalDivs + (i + 1) % TotalDivs;
                    _triangles[tris + 2] = k * TotalDivs + TotalDivs + i;
                    
                    _triangles[tris + 3] = k * TotalDivs + TotalDivs + (i + 1) % TotalDivs;
                    _triangles[tris + 4] = k * TotalDivs + i;
                    _triangles[tris + 5] = k * TotalDivs + (i + 1) % TotalDivs;

                    tris += 6;
                }
            }
            
            // Top triangles
            for (int i = 0; i < TotalDivs; i++)
            {
                _triangles[tris + 0] = k * TotalDivs + i;
                _triangles[tris + 1] = k * TotalDivs + (i + 1) % TotalDivs;
                _triangles[tris + 2] = TotalDivs * (divsPerQuad * 2 + 1);

                tris += 3;
            }
            
            // Bottom triangles
            for (int i = 0; i < TotalDivs; i++)
            {
                _triangles[tris + 0] = i;
                _triangles[tris + 1] = TotalDivs * (divsPerQuad * 2 + 1) + 1;
                _triangles[tris + 2] = (i + 1) % TotalDivs;

                tris += 3;
            }
        }
    }
}
