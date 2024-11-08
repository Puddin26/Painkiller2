using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Room1;
using Room2;

namespace Room2
{
    public class CharacterMover1 : MonoBehaviour
    {
        public Animator anim;
        public Room2.LineDrawer1 lineDrawer;
        bool isMoving;
        Vector3[] positions;
        int moveIndex;
        private float speed = 10f;
        public GameObject[] texter;


        private void Start()
        {
            foreach (var item in texter)
            {
                item.SetActive(false);
            }
        }

        private void OnMouseDown()
        {
            lineDrawer.StartLine(transform.position);
        }

        private void OnMouseDrag()
        {
           lineDrawer.UpdateLine();
        }

        private void OnMouseUp()
        {
            positions = new Vector3[lineDrawer.line.positionCount];
            lineDrawer.line.GetPositions(positions);
            isMoving = true;
            moveIndex = 0;
        }

        private void Update()
        {
            if (isMoving)
            {
                Vector2 currentPos = positions[moveIndex];
                transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
                anim.SetBool("IsWalking", true);
                

                float distance = Vector2.Distance(currentPos, transform.position);
                if (distance <= 0.05f)
                {
                    moveIndex++;
                }
                if (moveIndex > positions.Length - 1)
                {
                    isMoving = false;
                    lineDrawer.StartLine(transform.position);
                    anim.SetBool("IsWalking", false);
                }
                if(transform.position.x > 6f)
                {
                    isMoving = false;
                    lineDrawer.StartLine(transform.position);
                    anim.SetBool("IsWalking", false);
                    transform.position = new Vector2(5.8f, transform.position.y);
                }
                if (transform.position.x < -2.5f)
                {
                    isMoving = false;
                    lineDrawer.StartLine(transform.position);
                    anim.SetBool("IsWalking", false);
                    transform.position = new Vector2(-2.3f, transform.position.y);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "mathew_table")
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                texter[0].SetActive(true);
            }

            if (other.tag == "mathew_calender")
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                lineDrawer.follower.nextPage = true;
                texter[1].SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "mathew_table")
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                texter[0].SetActive(false);
            }
            if (other.tag == "mathew_calender")
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                texter[1].SetActive(false);
            }
        }


    }
}
