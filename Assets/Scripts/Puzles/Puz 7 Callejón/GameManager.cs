﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
    public class GameManager : MonoBehaviour
    {
        public delegate void TileCreated(GameObject tile);
        public static event TileCreated OnTileCreated;
        public ProgressionManagerProxy progressionmanagerproxy;

        [SerializeField] GameObject _tilePrefab;
        public List<Sprite> imageParts;
        List<Tile> _tiles = new List<Tile>();

        Vector3 emptySpace = Vector3.zero;

        float tileSize = 1.3f;

        bool allInPlace = false;
        bool areTilesVisible = true; // Estado de visibilidad de las piezas

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            int rowSize = 3;
            int gridSize = rowSize * rowSize;

            float x = 0f;
            float y = 0f;

            for (int i = 0; i < gridSize; i++)
            {
                if (i % rowSize == 0 && i != 0)
                {
                    x = 0f;
                    y -= tileSize;
                }

                if (i == gridSize - 1)
                {
                    emptySpace = new Vector3(x, y, 0f);
                    break;
                }

                var tileObj = Instantiate(_tilePrefab);
                var tileScript = tileObj.GetComponent<Tile>();

                tileObj.transform.position = new Vector3(x, y, 0f);
                Debug.Log($"Tile {i} instanciado y asignado");

                tileScript.SetImage(imageParts[i]);
                _tiles.Add(tileScript);

                OnTileCreated?.Invoke(tileObj);

                x += tileSize;
            }

            Camera.main.transform.position = new Vector3(rowSize / 2f, -rowSize / 2f, -10f);
            Camera.main.orthographicSize = (rowSize * tileSize) + 1f;
            Invoke("Shuffle", 0.01f);
            Invoke("ToggleTilesVisibility", 0.01f);
            

        }

        public void Shuffle()
        {
            for (int i = 0; i < 100; i++)
            {
                Swap(_tiles[Random.Range(0, _tiles.Count)], false);
            }

           
        }

        public void ClickedTile(Tile tile)
        {
            if (allInPlace)
                return;

            if (Vector3.Distance(tile.transform.position, emptySpace) < tileSize + 0.1f)
            {
                Swap(tile);
                CompletionCheck();
            }
        }

        void Swap(Tile tile, bool animation = true)
        {
            Vector3 tilePos = tile.TargetPosition;
            if (animation)
                tile.MoveToPos(emptySpace);
            else
                tile.MoveToPosNoAnim(emptySpace);
            emptySpace = tilePos;
        }

        void CompletionCheck()
        {
            bool inPlace = true;
            foreach (var tile in _tiles)
            {
                if (!tile.IsInPlace())
                {
                    inPlace = false;
                    break;
                }
            }

            allInPlace = inPlace;
            if (allInPlace) {
                progressionmanagerproxy.SetComplete(6);
                Debug.Log("¡Felicidades!");
            }
        }

        // Método para alternar la visibilidad de las piezas
        public void ToggleTilesVisibility()
        {
            areTilesVisible = !areTilesVisible;

            foreach (var tile in _tiles)
            {
                tile.gameObject.SetActive(areTilesVisible);
            }
        }
    }
}
