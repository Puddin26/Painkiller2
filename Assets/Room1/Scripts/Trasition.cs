using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trasition : MonoBehaviour
{
    public bool can_transition;
    public GameObject transition_square;
    public string scene_name;
    SpriteRenderer transition_sprite;

    // Start is called before the first frame update
    void Start()
    {
        transition_sprite = transition_square.GetComponent<SpriteRenderer>();
        transition_sprite.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //print(transition_sprite.color.a);


        if (Input.GetMouseButtonDown(0))
        {
            can_transition = true;
        }

        if(can_transition)
        {
            transition_sprite.color += new Color(0f, 0f, 0f, 0.5f * Time.deltaTime);
        }
        if(transition_sprite.color.a >= 1) 
        {
            SceneManager.LoadScene(scene_name);
            print("lOADED");
        }
    }
}
