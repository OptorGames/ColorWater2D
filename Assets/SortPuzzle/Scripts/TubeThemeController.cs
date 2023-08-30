using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeThemeController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter mesh;

    public void SetTubeModel(MeshFilter mesh, Material material)
    {
        meshRenderer.material = material;
        this.mesh.mesh = mesh.mesh;
    }
}
