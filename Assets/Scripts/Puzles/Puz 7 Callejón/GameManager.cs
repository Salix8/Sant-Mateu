using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
    public class GameManager : MonoBehaviour
    {
        public delegate void TileCreated(GameObject tile);
        public static event TileCreated OnTileCreated;

        [SerializeField] GameObject _tilePrefab;
        public List<Sprite> imageParts;
        public List<Tile> tiles = new List<Tile>(); // Lista de tiles

        Vector3 emptySpace = Vector3.zero;

        float tileSize = 1.3f;

        bool allInPlace = false;

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            StartCoroutine(SetupGame()); // Inicia la secuencia de configuración del juego
           
        }

        IEnumerator SetupGame()
        {
            int rowSize = 3;
            int gridSize = rowSize * rowSize;

            float x = 0f;
            float y = 0f;

            // Instanciación de los tiles
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
                tileScript.SetImage(imageParts[i]);
                tiles.Add(tileScript); // Añadir el tile a la lista de tiles

                OnTileCreated?.Invoke(tileObj);

                x += tileSize;
            }

            // Espera 1 frame para asegurarse de que todo está instanciado antes de continuar
            yield return null;

            // Ahora que los tiles están creados, configura la cámara
            Camera.main.transform.position = new Vector3(rowSize / 2f, -rowSize / 2f, -10f);
            Camera.main.orthographicSize = (rowSize * tileSize) + 1f;

            // Activar todos los tiles
            

            Invoke("Shuffle", 0.01f); // Mezclar los tiles después de haberlos activado
        }

        void Shuffle()
        {
            for (int i = 0; i < 100; i++)
            {
                Swap(tiles[Random.Range(0, tiles.Count)], false);
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
            foreach (var tile in tiles)
            {
                if (!tile.IsInPlace())
                {
                    inPlace = false;
                    break;
                }
            }

            allInPlace = inPlace;
            if (allInPlace)
            {
                Debug.Log("¡Felicidades!");
            }
        }
    }
}
