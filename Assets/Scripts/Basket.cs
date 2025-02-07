using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
     Vector3 mousePos2D = Input.mousePosition;
     mousePos2D.z = -Camera.main.transform.position.z;
     Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

     Vector3 pos = this.transform.position;
     pos.x = mousePos3D.x;
     this.transform.position = pos;   
    }

    void  OnCollisionEnter(Collision coll){
        GameObject collidedWith = coll.gameObject;
        Debug.Log("Collided with: " + collidedWith.name + " | Tag: " + collidedWith.tag);
        if(collidedWith.CompareTag("Apple")){
            Destroy(collidedWith);
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
        }
        else if (collidedWith.CompareTag("Branch")) // If a branch is caught, trigger Game Over
        {
            Debug.Log("Branch caught!");
            Destroy(collidedWith);
            ApplePicker ap = FindAnyObjectByType<ApplePicker>(); // Find ApplePicker in scene
            if (ap != null)
            {
                ap.GameOver(); // Call GameOver() in ApplePicker
            }
        }
    }
}
