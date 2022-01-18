using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyView : MonoBehaviour
{
    public TextMeshProUGUI idText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;

    public void InitView(Enemy enemy)
    {
        idText.text = enemy.id.ToString();
        nameText.text = enemy.name.ToString();
        healthText.text = enemy.health.ToString();
        attackText.text = enemy.attack.ToString();
    }
}
