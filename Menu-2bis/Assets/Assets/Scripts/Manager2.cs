using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager2 : MonoBehaviour {
	
	public Slider mSlider;
	public Slider rotationSlider;
	public GameObject boutonModifierTaille;


	public Transform canvas;
	public Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it
	public Transform PanelDimension;
	public Transform PanelRotation;
	bool isPaused; //Used to determine paused state

	RaycastHit hit;
	Ray ray;
	GameObject cube;
	Vector3 initialScale;


	void Start()
	{
		
		isPaused = false; //make sure isPaused is always false when our scene opens
		UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
		PanelDimension.gameObject.SetActive(false);
		PanelRotation.gameObject.SetActive(false);

	}
		

	void Update()
	{
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//an object is selected by the user with its mouse, the game is paused
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0) && !isPaused) {
			cube = hit.collider.gameObject;
			pause ();
		} 
	}
		

	/* pause() pauses the game and turn on the object interface */
	public void pause()
	{	
		isPaused = true;
		setPositionPanel();
		UIPanel.gameObject.SetActive(true); 
		Time.timeScale = 0f; 
	}


	/* unPause() resumes the game*/ 
	public void unPause()
	{	
		isPaused = false;
		UIPanel.gameObject.SetActive(false);
		Time.timeScale = 1f; 

	}


	/* set the position of the panel above the selected object */
	private void setPositionPanel(){
		float height = cube.GetComponent<Collider> ().bounds.size.y;
		Debug.Log (height);
		Vector3 nouvellePosition = new Vector3 (cube.transform.position.x, cube.transform.position.y + height, cube.transform.position.z);
		canvas.transform.position = nouvellePosition;

	}
		
  	

	/* resize() changes the size of the selected object */
	public void resize(){
		cube.transform.localScale = (mSlider.value)*(Vector3.one);
	}
		
	/* resizePanel() displays the resize panel*/ 
	public void resizePanel(){
		UIPanel.gameObject.SetActive(false);
		PanelDimension.gameObject.SetActive(true);
	}



	/* delete() deletes the selected object and closes the interface */
	public void delete(){
		Destroy(cube); 
		unPause ();
	}



	/* rotatePanel() displays rotation panel */
	public void rotatePanel(){
		UIPanel.gameObject.SetActive(false);
		PanelRotation.gameObject.SetActive(true);
	}


	/* rotate() rotates the object with a slider */
	public void rotate(){
		Vector3 tourner = (-rotationSlider.value) * (Vector3.up);
		cube.transform.rotation = Quaternion.Euler(0, rotationSlider.value, 0);
	}



	/* changeColor() changes the color of the selected object and color it with a random color */
	public void changeColor() {
		// Find a random color
		float red = Random.Range(0f,1f);
		float green = Random.Range(0f,1f);
		float blue = Random.Range(0f,1f);
		Color newColor = new Color( red, green, blue );

		// Apply color
		cube.GetComponent<MeshRenderer>().material.color = newColor;

		if (mSlider.gameObject.activeSelf) {
			mSlider.gameObject.SetActive (false);
			boutonModifierTaille.SetActive (true);
		}

	}


	public void closeInterface(){
		if (isPaused) {
			unPause();
		}
	}


	/* closeResize() closes the resize Panel */
	public void closeResize(){
		UIPanel.gameObject.SetActive(true);
		PanelDimension.gameObject.SetActive(false);
	}


	/* closeRotate() closes the rotate Panel */
	public void closeRotate(){
		UIPanel.gameObject.SetActive(true);
		PanelRotation.gameObject.SetActive(false);

	}
		

}