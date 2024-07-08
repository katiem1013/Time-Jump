using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Tiles : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI textNumber;
    private Board board;
    private Vector3 correctPosition;

    public bool IsCorrected { get; private set; } = false;

    private int numeric;

    // adds the number to each tile
    public int Numeric
    {
        set
        {
            numeric = value;
            textNumber.text = numeric.ToString();
        }
        get => numeric;
    }

    // gives all the tiles numbers and hides the last tile
    public void Setup(Board board, int hideNumber, int numeric)
    {
        this.board = board;
        textNumber = GetComponentInChildren<TextMeshProUGUI>();
        Numeric = numeric;

        if (Numeric == hideNumber)
        {
            GetComponent<Image>().enabled = false;
            textNumber.enabled = false;
        }
    }

    // sets the correct position
    public void SetCorrectPosition()
    {
        correctPosition = GetComponent<RectTransform>().localPosition;    
    }

    // 
    public void OnPointerClick(PointerEventData eventData)
    {
        board.IsMoveTile(this);
    }

    // moves tile
    public void OnMoveTo(Vector3 end) 
    {
        StartCoroutine("MoveTo", end);
    }

    // moves the tile to the empty position
    private IEnumerator MoveTo(Vector3 end)
    {
        float current = 0;
        float percent = 0;
        float moveTime = 0.1f;
        Vector3 start = GetComponent<RectTransform>().localPosition;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            GetComponent<RectTransform>().localPosition = Vector3.Lerp(start, end, percent); // moves image

            yield return null;
        }

        // checks if the position is correct
        IsCorrected = correctPosition == GetComponent<RectTransform>().localPosition ? true: false;
        board.IsGameOver();
    }
}
