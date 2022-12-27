using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=s_v9JnTDCCY

public class CubeBreaker : MonoBehaviour
{
    public float destroyTime = 2f;
    public float cubeSize = 0.2f; // 小正方體的大小

    public int cubesInRow = 5; // 生成cube數量

    float cubesPivotDistance;
    Vector3 cubesPivot;
    public float explosionForce = 50f;  //爆炸力大小
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }


    public void explode()
    {


        gameObject.SetActive(false);
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }
    void createPiece(int x, int y, int z)
    {
        GameObject piece;

        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);


        var destorySelf = piece.AddComponent<DestorySelf>() as DestorySelf;
        destorySelf.DestroyConstructor(destroyTime);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        piece.GetComponent<Renderer>().material = mat;

    }

}
public class DestorySelf : MonoBehaviour
{
    private float destroyTime;
    public void DestroyConstructor(float destroyTime) {
        this.destroyTime = destroyTime;
    }
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
