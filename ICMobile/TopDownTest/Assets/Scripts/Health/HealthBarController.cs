using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    private Health enemyScript;

    public Canvas canvas;

    public float healthPanelOffset = 10f;
    public GameObject healthPanel;
    private Slider healthSlider;
    private MeshRenderer rendi;

	// Use this for initialization
	void Start () {
		enemyScript = GetComponent<Health>();
        enemyScript.enemieHealthBar = this;
        healthPanel.transform.SetParent(MenuManager.instance.gameHUD.transform, false);

        //enemyName = healthPanel.GetComponentInChildren<Text>();
        //enemyName.text = enemyScript.gameObject.name;

        healthSlider = healthPanel.GetComponentInChildren<Slider>();

        rendi = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        if (rendi.isVisible)
        {
            healthPanel.SetActive(true);
        }
        else
        {
            healthPanel.SetActive(false);
        }
        healthSlider.value = enemyScript.health/ (float)enemyScript.maxHealth;

        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);


	}
}
