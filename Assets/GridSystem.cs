using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float x_space;
    public float z_space;

    [SerializeField]
    private GameObject _visualGridBox;

    [SerializeField]
    private GameObject _gridSystemPlaceholder;

    void Start()
    {
        Mesh planeMesh = GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        Vector3 boundsInPixel = Vector3.Scale(transform.localScale, bounds.size);
        GenerateGrids(boundsInPixel.x, boundsInPixel.z);
    }

    private void GenerateGrids(float columnLength, float rowLength)
    {
        var gridList = new List<Transform>();
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            var posX = x_space + (x_space * (i % columnLength));
            var posY = 0;
            var posZ = z_space + (z_space * (i / columnLength));
            var item = Instantiate(_visualGridBox, new Vector3(posX, posY, posZ), Quaternion.identity);
            gridList.Add(item.transform);
        }
        gridList.ForEach((gridItem) =>
        {
            gridItem.parent = _gridSystemPlaceholder.transform;
        });
        //TODO: create a way to position the container correctly bellow the plane
        _gridSystemPlaceholder.transform.position = new Vector3(-25.6f, 0.01f, -26);
    }
}
