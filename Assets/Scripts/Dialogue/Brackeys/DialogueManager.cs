using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public static DialogueManager instance;

	public TMP_Text nameText;
	public TMP_Text dialogueText;

	public GameObject dialogueUI;
	public Transform responseUI;

	public GameObject buttonPrefab;
	//public Button attackButton;
	//public Button surrenderButton;

	public bool hasResponses;

	private Queue<string> sentences;

    private void Awake()
    {
		instance = this;
    }

    void Start () {
		sentences = new Queue<string>();
	}

    public void Update()
    {
        if (Input.GetButtonDown("Dialogue Interact"))
        {
			DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue, GameObject sender)
	{
		dialogueUI.SetActive(true);
		Time.timeScale = 0f;

		if (dialogue.relation == Dialogue.Relation.Enemy)
		{
			hasResponses = true;

			//Debug.Log("1");

			if (responseUI.childCount < 2)
			{
				GameObject attackButton = Instantiate(buttonPrefab, responseUI);
				GameObject surrenderButton = Instantiate(buttonPrefab, responseUI);

				attackButton.GetComponentInChildren<TMP_Text>().text = "Attack";
				attackButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
				attackButton.GetComponent<Button>().onClick.AddListener(() => sender.GetComponent<GetGameManager>().LoadBattle());

				surrenderButton.GetComponentInChildren<TMP_Text>().text = "Surrender";
				surrenderButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
				surrenderButton.GetComponent<Button>().onClick.AddListener(() => sender.GetComponent<GetGameManager>().Surrender());
			}
		}

		if (dialogue.relation == Dialogue.Relation.Neutral)
		{
			Debug.Log("1");

			if (sender.GetComponent<FleetFaction>().faction == FactionManager.instance.factions[0] || sender.GetComponent<FleetFaction>().faction == FactionManager.instance.factions[1])
            {
				hasResponses = true;

				if (responseUI.childCount < 2)
				{
					GameObject joinButton = Instantiate(buttonPrefab, responseUI);
					
					joinButton.GetComponentInChildren<TMP_Text>().text = "Join Faction";
					joinButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
					joinButton.GetComponent<Button>().onClick.AddListener(() => sender.GetComponent<JoinFaction>().FactionJoin());

					GameObject leaveButton = Instantiate(buttonPrefab, responseUI);

					leaveButton.GetComponentInChildren<TMP_Text>().text = "Leave";
					leaveButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
				}
			}

				//if (responseUI.childCount < 2)
				//{
				//	GameObject joinButton = Instantiate(buttonPrefab, responseUI);

				//	joinButton.GetComponentInChildren<TMP_Text>().text = "Join Faction";
				//	joinButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
				//	joinButton.GetComponent<Button>().onClick.AddListener(() => sender.GetComponent<JoinFaction>().FactionJoin());

				//	GameObject leaveButton = Instantiate(buttonPrefab, responseUI);

				//	joinButton.GetComponentInChildren<TMP_Text>().text = "Leave";
				//	joinButton.GetComponent<Button>().onClick.AddListener(() => EndDialogue());
				//}
		}

		nameText.text = sender.GetComponent<UniqueNPC>().npc.Name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		//Debug.Log("Displaying next");

		if (sentences.Count == 0 && hasResponses == false)
		{
			//Debug.Log("No sentences left");

			EndDialogue();
			return;
        }
        else if(sentences.Count == 0 && hasResponses == true)
        {
			return;
        }

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{
		dialogueUI.SetActive(false);
		Time.timeScale = GameManager.instance.speedMultiplier;

        for (int i = 0; i < responseUI.childCount; i++)
        {
			Destroy(responseUI.GetChild(i).gameObject);
        }
	}

}
