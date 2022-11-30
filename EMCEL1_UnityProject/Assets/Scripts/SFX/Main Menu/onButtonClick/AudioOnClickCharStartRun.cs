using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioOnClickCharStartRun : MonoBehaviour
{
	public Button ButtonsUI;

	void Start()
	{
		Button btn = ButtonsUI.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Sound Plays");
		FindObjectOfType<ButtonSFXManager>().Play("clickStartRun");//sfx
	}
}