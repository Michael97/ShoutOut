/// Peter Phillips
/// 07.03.2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipNormals : MonoBehaviour
{
	void Start ()
    {
        Mesh _mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] _normals = _mesh.normals;
        
        for (int i = 0; i < _normals.Length; ++i)
        {
            _normals[i] = -1 * _normals[i];
        }

        _mesh.normals = _normals;

        for (int i = 0; i < _mesh.subMeshCount; ++i)
        {
            int[] _tris = _mesh.GetTriangles(i);
            for (int j = 0; j < _tris.Length; j += 3)
            {
                int _temp = _tris[j];
                _tris[j] = _tris[j + 1];
                _tris[j + 1] = _temp;
            }

            _mesh.SetTriangles(_tris, i);
        }
    }
}
