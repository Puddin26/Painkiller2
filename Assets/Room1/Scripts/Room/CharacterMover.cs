using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Room1;

namespace Room1
{
    public class CharacterMover : MonoBehaviour
    {
        public Animator anim;
        public Room1.LineDrawer lineDrawer;
        bool isMoving;
        Vector3[] positions;
        int moveIndex;
        private float speed = 10f;
        public int tableDone = 0;
        public GameObject Minigame2;
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
            if(tableDone != 1)
            {
                lineDrawer.UpdateLine();
            }
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
                if(transform.position.x > 4.5f)
                {
                    isMoving = false;
                    lineDrawer.StartLine(transform.position);
                    anim.SetBool("IsWalking", false);
                    transform.position = new Vector2(4, transform.position.y);
                }
                if (transform.position.x < -3.5f)
                {
                    isMoving = false;
                    lineDrawer.StartLine(transform.position);
                    anim.SetBool("IsWalking", false);
                    transform.position = new Vector2(-3, transform.position.y);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "mathew_table" && tableDone != 2)
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                Minigame2.SetActive(true);
                tableDone = 1;
                isMoving = false;
                lineDrawer.StartLine(transform.position);
                anim.SetBool("IsWalking", false);
                transform.position = transform.position;
            }

            if (other.tag == "mathew_calender")
            {
                SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
                lineDrawer.follower.nextPage = true;
                texter[1].SetActive(true);
                texter[0].SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "mathew_table" && tableDone == 2)
            {
                Minigame2.SetActive(false);
                texter[0].SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "mathew_table")
            {
                texter[0].SetActive(false);
            }
            if (other.tag == "mathew_calender")
            {
                texter[1].SetActive(false);
            }
        }


    }
}
