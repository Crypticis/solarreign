using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScienceMinigameManager : MonoBehaviour
{
    public static ScienceMinigameManager instance;

    public ScienceMinigame minigame;

    public GameObject minigameUI;
    public Inventory playerInventory;
    public GameObject cellPrefab;
    public Transform Root;
    public GameObject failUI;
    public GameObject successUI;

    public GridLayoutGroup gridLayout;
    public GameObject interactText;

    public List<GameObject> cells = new List<GameObject>();

    GameObject previousInteraction;

    private void Awake()
    {
        instance = this;
    }

    public void ShowMinigame(ScienceMinigame.DifficultyLevel _difficulty, Item _reward, int _rewardAmount, GameObject interactionObject)
    {
        previousInteraction = interactionObject;

        NewMinigame(_difficulty, _reward, _rewardAmount);

        for (int i = 0; i < minigame.cells.GetLength(0); i++)
        {
            for (int j = 0; j < minigame.cells.GetLength(1); j++)
            {
                GameObject go = Instantiate(cellPrefab, Root);

                cells.Add(go);

                go.GetComponent<ScienceMinigameCell>().manager = this;
                go.GetComponent<ScienceMinigameCell>().hasBomb = minigame.cells[i, j].hasBomb;
                go.GetComponent<ScienceMinigameCell>().bombsNearby = minigame.cells[i, j].bombsNearby;
            }
        }

        interactText.SetActive(false);

        Time.timeScale = 0;

        minigameUI.SetActive(true);
    }

    public void CheckCompletion()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if(!cells[i].GetComponent<ScienceMinigameCell>().hasBomb && !cells[i].GetComponent<ScienceMinigameCell>().isSelected)
            {
                return;
            }
        }

        successUI.SetActive(true);

        previousInteraction.GetComponent<ScienceInteraction>().completed = true;
        previousInteraction.GetComponent<Target>().enabled = false;

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Button>().interactable = false;
        }
    }

    public void Fail()
    {
        //resets minigame or closes it or something/

        failUI.SetActive(true);

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].GetComponent<Button>().interactable = false;
        }
    }

    public void Close()
    {
        failUI.SetActive(false);
        successUI.SetActive(false);
        minigameUI.SetActive(false);

        for (int i = 0; i < Root.childCount; i++)
        {
            Destroy(Root.GetChild(i).gameObject);
        }

        cells.Clear();

        Time.timeScale = 1;
    }

    public void GiveReward()
    {
        playerInventory.AddItem(minigame.reward, minigame.rewardAmount);

        Ticker.Ticker.AddItem(minigame.rewardAmount + " " + minigame.reward.Name + " added to inventory.");
    }

    public void InitializeCells()
    {
        for (int i = 0; i < minigame.cells.GetLength(0); i++)
        {
            for (int j = 0; j < minigame.cells.GetLength(1); j++)
            {
                minigame.cells[i, j] = new ScienceMinigameCell();
            }
        }
    }

    public void NewMinigame(ScienceMinigame.DifficultyLevel _difficulty, Item _reward, int _rewardAmount)
    {
        minigame = new ScienceMinigame
        {
            difficulty = _difficulty,
            reward = _reward,
            rewardAmount = _rewardAmount,
        };

        switch (minigame.difficulty)
        {
            case ScienceMinigame.DifficultyLevel.easy:

                minigame.cells = new ScienceMinigameCell[4, 4];

                InitializeCells();

                minigame.bombNumber = 1;

                for (int i = 0; i < minigame.bombNumber; i++)
                {
                    int row = Random.Range(0, minigame.cells.GetLength(0));
                    int column = Random.Range(0, minigame.cells.GetLength(1));

                    minigame.cells[row, column].hasBomb = true;

                    //Horizontal and Vertical
                    if (row - 1 >= 0)
                    {
                        minigame.cells[row - 1, column].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0))
                    {
                        minigame.cells[row + 1, column].bombsNearby++;
                    }
                    if (column - 1 >= 0)
                    {
                        minigame.cells[row, column - 1].bombsNearby++;
                    }
                    if (column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row, column + 1].bombsNearby++;
                    }

                    //Diagonals
                    if (row + 1 < minigame.cells.GetLength(0) && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row + 1, column + 1].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0) && column - 1 >= 0)
                    {
                        minigame.cells[row + 1, column - 1].bombsNearby++;
                    }
                    if (row - 1 >= 0 && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row - 1, column + 1].bombsNearby++;
                    }
                    if (row - 1 >= 0 && column - 1 >= 0)
                    {
                        minigame.cells[row - 1, column - 1].bombsNearby++;
                    }
                }

                break;

            case ScienceMinigame.DifficultyLevel.medium:

                minigame.cells = new ScienceMinigameCell[5, 5];

                InitializeCells();

                minigame.bombNumber = 2;

                for (int i = 0; i < minigame.bombNumber; i++)
                {
                    int row = Random.Range(0, minigame.cells.GetLength(0));
                    int column = Random.Range(0, minigame.cells.GetLength(1));

                    minigame.cells[row, column].hasBomb = true;

                    //Horizontal and Vertical
                    if (row - 1 > 0)
                    {
                        minigame.cells[row - 1, column].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0))
                    {
                        minigame.cells[row + 1, column].bombsNearby++;
                    }
                    if (column - 1 > 0)
                    {
                        minigame.cells[row, column - 1].bombsNearby++;
                    }
                    if (column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row, column + 1].bombsNearby++;
                    }

                    //Diagonals
                    if (row + 1 < minigame.cells.GetLength(0) && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row + 1, column + 1].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0) && column - 1 > 0)
                    {
                        minigame.cells[row + 1, column - 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row - 1, column + 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column - 1 > 0)
                    {
                        minigame.cells[row - 1, column - 1].bombsNearby++;
                    }
                }

                break;

            case ScienceMinigame.DifficultyLevel.hard:

                minigame.cells = new ScienceMinigameCell[6, 6];

                InitializeCells();

                minigame.bombNumber = 3;

                for (int i = 0; i < minigame.bombNumber; i++)
                {
                    int row = Random.Range(0, minigame.cells.GetLength(0));
                    int column = Random.Range(0, minigame.cells.GetLength(1));

                    minigame.cells[row, column].hasBomb = true;

                    //Horizontal and Vertical
                    if (row - 1 > 0)
                    {
                        minigame.cells[row - 1, column].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0))
                    {
                        minigame.cells[row + 1, column].bombsNearby++;
                    }
                    if (column - 1 > 0)
                    {
                        minigame.cells[row, column - 1].bombsNearby++;
                    }
                    if (column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row, column + 1].bombsNearby++;
                    }

                    //Diagonals
                    if (row + 1 < minigame.cells.GetLength(0) && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row + 1, column + 1].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0) && column - 1 > 0)
                    {
                        minigame.cells[row + 1, column - 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row - 1, column + 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column - 1 > 0)
                    {
                        minigame.cells[row - 1, column - 1].bombsNearby++;
                    }
                }

                break;

            case ScienceMinigame.DifficultyLevel.veryhard:

                minigame.cells = new ScienceMinigameCell[7, 7];

                InitializeCells();

                minigame.bombNumber = 4;

                for (int i = 0; i < minigame.bombNumber; i++)
                {
                    int row = Random.Range(0, minigame.cells.GetLength(0));
                    int column = Random.Range(0, minigame.cells.GetLength(1));

                    minigame.cells[row, column].hasBomb = true;

                    //Horizontal and Vertical
                    if (row - 1 > 0)
                    {
                        minigame.cells[row - 1, column].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0))
                    {
                        minigame.cells[row + 1, column].bombsNearby++;
                    }
                    if (column - 1 > 0)
                    {
                        minigame.cells[row, column - 1].bombsNearby++;
                    }
                    if (column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row, column + 1].bombsNearby++;
                    }

                    //Diagonals
                    if (row + 1 < minigame.cells.GetLength(0) && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row + 1, column + 1].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0) && column - 1 > 0)
                    {
                        minigame.cells[row + 1, column - 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row - 1, column + 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column - 1 > 0)
                    {
                        minigame.cells[row - 1, column - 1].bombsNearby++;
                    }
                }

                break;

            case ScienceMinigame.DifficultyLevel.insane:

                minigame.cells = new ScienceMinigameCell[8, 8];

                InitializeCells();

                minigame.bombNumber = 5;

                for (int i = 0; i < minigame.bombNumber; i++)
                {
                    int row = Random.Range(0, minigame.cells.GetLength(0));
                    int column = Random.Range(0, minigame.cells.GetLength(1));

                    minigame.cells[row, column].hasBomb = true;

                    //Horizontal and Vertical
                    if (row - 1 > 0)
                    {
                        minigame.cells[row - 1, column].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0))
                    {
                        minigame.cells[row + 1, column].bombsNearby++;
                    }
                    if (column - 1 > 0)
                    {
                        minigame.cells[row, column - 1].bombsNearby++;
                    }
                    if (column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row, column + 1].bombsNearby++;
                    }

                    //Diagonals
                    if (row + 1 < minigame.cells.GetLength(0) && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row + 1, column + 1].bombsNearby++;
                    }
                    if (row + 1 < minigame.cells.GetLength(0) && column - 1 > 0)
                    {
                        minigame.cells[row + 1, column - 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column + 1 < minigame.cells.GetLength(1))
                    {
                        minigame.cells[row - 1, column + 1].bombsNearby++;
                    }
                    if (row - 1 > 0 && column - 1 > 0)
                    {
                        minigame.cells[row - 1, column - 1].bombsNearby++;
                    }
                }

                break;
        }

        gridLayout.constraintCount = minigame.cells.GetLength(0);
    }

    [System.Serializable]
    public class ScienceMinigame
    {
        public ScienceMinigameCell[,] cells;
        public DifficultyLevel difficulty;

        public Item reward;
        public int rewardAmount;
        public int bombNumber;

        public enum DifficultyLevel
        {
            easy,
            medium,
            hard,
            veryhard,
            insane,
        }
    }
}
