using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour {

    [SerializeField] Tile tilePrefab;
    [SerializeField] int column;
    [SerializeField] int row;

    public void Generate(int column, int row)
    {
        float width = tilePrefab.width;
        float height = tilePrefab.height;

        Transform tileHolder = transform;

        Vector3 src = tileHolder.transform.position;

        src += Vector3.down * column * height / 2;
        src += Vector3.right * width / 2;
        src += Vector3.up * height / 2;


        Tile.Initialize(row, column);

        for (int y = 0; y < column; y++)
        {
            for (int x = 0; x < row; x++)
            {
                Tile tile = Instantiate(tilePrefab);
                    tile.transform.SetParent(tileHolder);
                    tile.SetPosition(x, y);

                Vector3 pos = src + new Vector3(x * width, y * height, -1);
                tile.transform.position = pos;

            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        float width = row * tilePrefab.width;
        float height = column * tilePrefab.height;

        Vector2 center = new Vector2(
            transform.position.x + width / 2, transform.position.y
        );

        Gizmos.color = Color.grey;
        Gizmos.DrawCube(center, new Vector2(width, height));
    }


}
