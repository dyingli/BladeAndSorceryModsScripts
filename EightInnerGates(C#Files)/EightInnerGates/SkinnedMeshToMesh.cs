using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using ThunderRoad;

public class SkinnedMeshToMesh : MonoBehaviour
{

    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect VFXGraph;
    public float refreshRate;

    public IEnumerator UpdateVFXGraph()
    {

        while (gameObject.activeSelf)
        {

            Debug.Log("Updating Mesh");
            Mesh m = new Mesh();

            skinnedMesh.BakeMesh(m);
            Vector3[] vertices = m.vertices;

            Mesh m2 = new Mesh();
            m2.vertices = vertices;
            VFXGraph.SetMesh("mesh", m2);

            Debug.Log(VFXGraph.aliveParticleCount);
            
            yield return new WaitForSeconds(refreshRate);

        }


    }


    public void StartUpdate() {


        StartCoroutine(UpdateVFXGraph());
    
    
    }


    public void Setup(SkinnedMeshRenderer skinnedMeshIn, float refreshRateIn, VisualEffect VFXGraphIn)
    {

        this.skinnedMesh = skinnedMeshIn;
        this.refreshRate = refreshRateIn;
        this.VFXGraph = VFXGraphIn;
        Debug.Log("Setting up mesh");



    }
}
