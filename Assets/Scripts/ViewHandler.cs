using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewHandler : MonoBehaviour {
	public GameObject Viewer;
	public GameObject region1, region2, region3, region4, region5, region6;
	public float height;
	//Minimum Height Level for Visbility
	private float r1Base = 7, r2Base = 10, r3Base = 12, r4Base = 14, r5Base = 18, r6Base = 21;
	//private float r1Base = 3, r2Base = 6, r3Base = 8, r4Base = 10, r5Base = 14, r6Base = 17;

	// Use this for initialization
	void Start () {
		height = Viewer.transform.position.y;

		region1.SetActive(false);
		region2.SetActive(false);
		region3.SetActive(false);
		region4.SetActive(false);
		region5.SetActive(false);
		region6.SetActive(false);

		//yMod = Viewer.GetComponent<CameraScript>().yMod;
	
	}
	
	// Update is called once per frame
	void Update () {
		//Check for Height Change
		if (Viewer.transform.position.y != height) {
			height = Viewer.transform.position.y;

			//Change Visibility
			if (height > r1Base) {
				//Region1 Visibile
				region1.SetActive(true);

				if (height > r2Base) {
					//Region2 Visible
					region2.SetActive(true);
					//print ("2nd Viewable");

					if (height > r3Base) {
						//Region3 Visible
						region3.SetActive(true);
						//print ("3rd Viewable");

						if (height > r4Base) {
							//Region4 Visible
							region4.SetActive(true);
							//print ("4th Viewable");

							if (height > r5Base) {
								//Region5 Visible
								region5.SetActive(true);
								//print ("5th Viewable");

								if (height > r6Base) {
									//Region6 Visible
									region6.SetActive(true);
									//print ("6th Viewable");
									
								}
								
								else {
									region6.SetActive(false);
									//print ("6th NOT Viewable");
								}
							}
							
							else {
								region5.SetActive(false);
								//print ("5th NOT Viewable");
							}
						}
						
						else {
							region4.SetActive(false);
							//print ("4th NOT Viewable");
						}
					}
					
					else {
						region3.SetActive(false);
						//print ("3rd NOT Viewable");
					}
				}

				else {
					//print ("2nd NOT Viewable");
					region2.SetActive(false);
				}
			}
		}
		
	
	}
}
