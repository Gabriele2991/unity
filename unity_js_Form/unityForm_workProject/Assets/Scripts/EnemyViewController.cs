using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyViewController : MonoBehaviour
{
    public Text idText;
    public Text nameText;
    public Text healthText;
    public Text attackText;


    public void displayEnemyData(string id, string name, string health, string attack)
    {
        idText.text = id;
        nameText.text = name;
        healthText.text = health;
        attackText.text = attack;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
