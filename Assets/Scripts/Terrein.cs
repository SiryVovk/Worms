using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.LightTransport;
using static UnityEngine.Rendering.HableCurve;
using static UnityEngine.UI.Image;

public class Terrein : MonoBehaviour
{
    [SerializeField] private SpriteRenderer terrainSprite;

    private Texture2D terrainTexture;
    private PolygonCollider2D pollygonCollider;

    private bool[] solid;

    private void Awake()
    {
        pollygonCollider = GetComponent<PolygonCollider2D>();

        GetTextureCopy();
        SetRuntimeSprite();

        SetAlphasArray();
    }

    #region TerreinPreparation
    private void GetTextureCopy()
    {
        Sprite originalSprite = terrainSprite.sprite;
        Texture2D originalTexture = originalSprite.texture;

        Rect r = originalSprite.rect;

        Color32[] srcPixels = originalTexture.GetPixels32();

        int w = (int)r.width;
        int h = (int)r.height;

        Color32[] spritePixels = new Color32[w * h];

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                int srcX = (int)r.x + x;
                int srcY = (int)r.y + y;

                spritePixels[y * w + x] = srcPixels[srcY * originalTexture.width + srcX];
            }
        }

        terrainTexture = new Texture2D(w, h, TextureFormat.RGBA32, false);
        terrainTexture.SetPixels32(spritePixels);
        terrainTexture.Apply();
    }

    private void SetRuntimeSprite()
    {
        Sprite originalSprite = terrainSprite.sprite;

        Rect spriteRect = new Rect(0, 0, terrainTexture.width, terrainTexture.height);
        Sprite runtimeSprite = Sprite.Create(
            terrainTexture,
            spriteRect,
            originalSprite.pivot / originalSprite.rect.size,
            originalSprite.pixelsPerUnit
            );

        terrainSprite.sprite = runtimeSprite;
    }

    private void SetAlphasArray()
    {
        solid = new bool[terrainTexture.width * terrainTexture.height];
        Color32[] pixels = terrainTexture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            solid[i] = pixels[i].a > 0;
        }

        RegeneratePolygonCollider();
    }

    #endregion TerreinPreparation

    public void DestroyTerrain(Vector2 worldPosition, float radius)
    {
        VisualDestruction(worldPosition, radius);
    }

    private void VisualDestruction(Vector2 worldPosition, float radius)
    {
        Vector2 local = terrainSprite.transform.InverseTransformPoint(worldPosition);
        Sprite sprite = terrainSprite.sprite;

        Vector2 localPx = local * sprite.pixelsPerUnit;

        Vector2 pivotPx = sprite.pivot;
        Vector2 texPos = localPx + pivotPx;

        int pixelX = Mathf.RoundToInt(texPos.x);
        int pixelY = Mathf.RoundToInt(texPos.y);

        float pixelPerUnit = sprite.pixelsPerUnit;
        int radiusInPixels = Mathf.CeilToInt(radius * pixelPerUnit);

        Color32[] pixels = terrainTexture.GetPixels32();

        for (int y = -radiusInPixels; y <= radiusInPixels; y++)
        {
            for (int x = -radiusInPixels; x <= radiusInPixels; x++)
            {
                int currentX = pixelX + x;
                int currentY = pixelY + y;
                if (currentX < 0 || currentX >= terrainTexture.width || currentY < 0 || currentY >= terrainTexture.height)
                {
                    continue;
                }

                float distance = Mathf.Sqrt(x * x + y * y);

                if (distance <= radiusInPixels)
                {
                    int index = currentY * terrainTexture.width + currentX;
                    solid[index] = false;
                    pixels[index].a = 0;
                }
            }
        }

        terrainTexture.SetPixels32(pixels);
        terrainTexture.Apply();

        RegeneratePolygonCollider();
    }

    private void RegeneratePolygonCollider()
    {
        if (solid == null || terrainTexture == null || pollygonCollider == null)
        {
            return;
        }

        int height = terrainTexture.height;
        int width = terrainTexture.width;
        var islands = FindIslands(solid, width, height);

        foreach (var island in islands)
        {
            bool[] islandMask = BuildIslandMask(island, width, height);

            Vector2 firstBoundaryCell = FirstBoundaryCell(islandMask,island, width, height);

            Debug.Log($"Start boundary cell: {firstBoundaryCell.x},{firstBoundaryCell.y}");
        }
    }

    private List<List<int>> FindIslands(bool[] solid, int width, int height)
    {
        bool[] visited = new bool[solid.Length];
        List<List<int>> islands = new List<List<int>>();

        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                if (!solid[index] || visited[index])
                {
                    continue;
                }

                List<int> island = new List<int>();
                Queue<int> toVisit = new Queue<int>();

                toVisit.Enqueue(index);
                visited[index] = true;

                while (toVisit.Count > 0)
                {
                    int qurent = toVisit.Dequeue();
                    island.Add(qurent);

                    int cx = qurent % width;
                    int cy = qurent / width;

                    for (int i = 0; i < 4; i++)
                    {
                        int indexToCheckX = cx + dx[i];
                        int indexToCheckY = cy + dy[i];

                        if (indexToCheckX < 0 || indexToCheckY < 0 || indexToCheckX >= width || indexToCheckY >= height)
                        {
                            continue;
                        }

                        int indexToCheck = indexToCheckY * width + indexToCheckX;

                        if (solid[indexToCheck] && !visited[indexToCheck])
                        {
                            visited[indexToCheck] = true;
                            toVisit.Enqueue(indexToCheck);
                        }
                    }
                }

                islands.Add(island);
            }
        }

        return islands;
    }

    private bool[] BuildIslandMask(List<int> island, int width, int height)
    {
        bool[] islandMask = new bool[width * height];

        foreach (int i in island)
        {
            islandMask[i] = true;
        }

        return islandMask;
    }

    private Vector2 FirstBoundaryCell(bool[] islandMask, List<int> island, int width, int height)
    {
        Vector2 firstBoundaryCell = new Vector2(-1,-1);

        foreach (int i in island)
        {
            int x = i % width;
            int y = i / width;

            if(isBoundryCell(islandMask,x,y, width,height))
            {
                firstBoundaryCell = new Vector2(x, y);
                break;
            }
        }
        return firstBoundaryCell;
    }

    private bool isBoundryCell(bool[] islandMask, int x, int y, int width, int height)
    {
        if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
        {
            return true;
        }

        int upIndex = (y + 1) * width + x;
        int downIndex = (y -1) * width + x;
        int leftIndex = y * width + (x - 1);
        int rightIndex = y * width + (x + 1);

        return !islandMask[upIndex] || !islandMask[downIndex] || !islandMask[leftIndex] || !islandMask[rightIndex];
    }
}
    
