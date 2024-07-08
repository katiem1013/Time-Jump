using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tilesParent;

    public static PuzzleBox PuzzleBox;

    private List<Tiles> tileList;
    private Vector2Int puzzleSize = new Vector2Int(3,3);
    public float neighbourTileDistance = 127; // this is can be checked through checking the poistion of two tiles and taking one away

    public Vector3 EmptyTilePosition { set; get; }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        tileList = new List<Tiles>();
        SpawnTiles();

        LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());
        yield return new WaitForEndOfFrame();

        tileList.ForEach(x => x.SetCorrectPosition());

        StartCoroutine("OnShuffle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawns the tiles for the sliding puzzles
    private void SpawnTiles()
    {
        for (int y = 0; y < puzzleSize.y; y++) 
        { 
            for (int x = 0; x < puzzleSize.x; x++)
            {
                // creates the tiles
                GameObject clone = Instantiate(tilePrefab, tilesParent);
                Tiles tile = clone.GetComponent<Tiles>();

                tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);
                tileList.Add(tile); // puts all the tiles in a list 
            }
        }
    }

    // puts the tiles in the a random order
    public IEnumerator OnShuffle()
    {
        float current = 0;
        float percent = 0;
        float time = 0.5f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            int index = Random.Range(0, puzzleSize.x * puzzleSize.y); // gets a random range based on the amount of tiles
            tileList[index].transform.SetAsLastSibling(); // adds to the last position on a list

            yield return null;
        }

        // checks if a tile position is empty
        EmptyTilePosition = tileList[tileList.Count - 1].GetComponent<RectTransform>().localPosition;
    }

    // checks if the tile is movable 
    public void IsMoveTile(Tiles tiles)
    {
        if (Vector3.Distance(EmptyTilePosition, tiles.GetComponent<RectTransform>().localPosition) == neighbourTileDistance)
        {
            Vector3 goalPosition = EmptyTilePosition;
            EmptyTilePosition = tiles.GetComponent<RectTransform>().localPosition; // finds the empty position on the board

            tiles.OnMoveTo(goalPosition);
        }
    }

    // checks if the pieces are all in the right position 
    public void IsGameOver()
    {
        List<Tiles> tiles = tileList.FindAll(x => x.IsCorrected == true);
        Debug.Log("Correct Count: " + tiles.Count);
        
        // takes away 1 because there is a blank piece on the board
        if(tiles.Count == puzzleSize.x * puzzleSize.y - 1)
        {
            PuzzleBox.completePuzzle = true;
        }
    }

    // shuffles the board
    public void Shuffle()
    {
        StartCoroutine("OnShuffle");
    }
}
