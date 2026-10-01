using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] public Image totalhealthBar;
    [SerializeField] public Image currenthealthBar;


    private void Start()
    {
        // establishes max health valu. Its divided by 10 as the Canvas sprite linked to the health bar is out of 10 allowing for future drops of health ups. 
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        // establises current health value based on the value at the start and any damage taken. Divided by 10 as stated above. 
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}