using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightPulse : MonoBehaviour 
{
	internal bool black;
	public Color _light, blue;
	public int pulseStrengh = 1000;
	public Text lightValue, hapticForce;
	public GameObject canvas;

	void Start () 
	{
		StartCoroutine("Pulse");
		canvas.SetActive(false);
	}

	IEnumerator Pulse () 
	{
		while(true)
		{
			if (black)
			{
				GetComponent<Camera>().backgroundColor = _light;
				black = false;
			}
			else
			{
				GetComponent<Camera>().backgroundColor = Color.black;
				black = true;
			}

			SteamVR_Controller.Input(0).TriggerHapticPulse((ushort)pulseStrengh);
			SteamVR_Controller.Input(1).TriggerHapticPulse((ushort)pulseStrengh);
			SteamVR_Controller.Input(2).TriggerHapticPulse((ushort)pulseStrengh);
			SteamVR_Controller.Input(3).TriggerHapticPulse((ushort)pulseStrengh);
			SteamVR_Controller.Input(4).TriggerHapticPulse((ushort)pulseStrengh);
				
			yield return new WaitForSeconds(0.025f);
		}
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			pulseStrengh = Mathf.Clamp(pulseStrengh + 100, 0, 3999);
			ShowCanvas();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			pulseStrengh = Mathf.Clamp(pulseStrengh - 100, 0, 3999);
			ShowCanvas();
		}

		hapticForce.text = "Haptic Strengh : " + pulseStrengh.ToString() + "/ 3999";

		if (Input.GetKey(KeyCode.RightArrow))
		{
			_light = Color.Lerp(_light, blue, 1 * Time.deltaTime);
			ShowCanvas();
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			_light = Color.Lerp(_light, Color.black, 1 * Time.deltaTime);
			ShowCanvas();
		}

		string lightInfo = _light.grayscale.ToString();
		if (lightInfo.Length > 4)
		lightInfo = lightInfo.Substring(0, 4);
		lightValue.text = "Light Strength : " + lightInfo + "/ 1";
	}

	void ShowCanvas()
	{
		canvas.SetActive(true);
		if (!IsInvoking("HideText"))
			Invoke("HideText", 2);
	}

	void HideCanvas()
	{
		canvas.SetActive(false);
	}
}
