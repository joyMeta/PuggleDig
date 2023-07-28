using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerBoard {
    public string playerName;
    public List<Transform> diamondPositions = new List<Transform>();
    public Transform player;
    public int currentPlayerPosition=0;
}

public class GameManager : MonoBehaviour {
    public FractionDice fractionDice;
    public ActionDice actionDice;

    [SerializeField]
    [Range(2,4)]
    int playerCount;

    public List<PuggleBoard> puggleBoards = new();
    public List<Button> uiButtons=new();
    public Button houndButton;

    public List<PlayerBoard> boards = new();

    public int currentPlayerIndex;

    void Start() {
        for(int i = 0; i < playerCount; i++) {
            puggleBoards[i].gameObject.SetActive(true);
        }
    }

    public void Turn() {
        if (currentPlayerIndex < playerCount)
            currentPlayerIndex++;
        else
            currentPlayerIndex = 0;
        int fractionRandom = Random.Range(0, 3);
        float actionRandom = Random.Range(0f, 1f);
        fractionDice = (FractionDice)fractionRandom;
        if (actionRandom < 0.6f)
            actionDice = ActionDice.PLACE;
        else
            actionDice = (ActionDice)Random.Range(0, 4);

        if (actionDice == ActionDice.DIAMOND) {
            if (boards[currentPlayerIndex].currentPlayerPosition < 4)
                boards[currentPlayerIndex].currentPlayerPosition++;
            else
                boards[currentPlayerIndex].currentPlayerPosition = 0;
        }
    }

    void Update() {
        for (int i = 0; i < playerCount; i++)
            boards[i].player.position = Vector3.Lerp(boards[i].player.position, boards[i].diamondPositions[boards[i].currentPlayerPosition].position,Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.F))
            Turn();
        for (int i = 0; i < uiButtons.Count; i++)
            uiButtons[i].interactable = fractionDice == (FractionDice)i && actionDice == ActionDice.PLACE;
        houndButton.interactable = actionDice == ActionDice.HOUND;
    }
}