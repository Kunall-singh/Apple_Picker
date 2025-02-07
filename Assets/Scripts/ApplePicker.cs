using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ApplePicker : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -10f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public Text roundText; // Assign in Unity Inspector
    private int round = 1;

    void Start()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
        UpdateRoundText();
    }

    public void AppleMissed()
    {
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        int basketIndex = basketList.Count - 1;
        if (basketIndex >= 0)
        {
            GameObject basketGO = basketList[basketIndex];
            basketList.RemoveAt(basketIndex);
            Destroy(basketGO);
        }

        if (basketList.Count == 0) // All baskets lost → Transition to Game Over Scene
        {
            GameOver();
        }
        else
        {
            round++;
            UpdateRoundText();
        }
    }

    void UpdateRoundText()
    {
        if (basketList.Count > 0)
        {
            roundText.text = "Round " + round;
        }
    }

    public void GameOver()
    {
        roundText.text = "Game Over";
        Invoke("LoadGameOverScene", 0.2f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(2); // Ensure this scene exists in Build Settings
    }
}
