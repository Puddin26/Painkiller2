using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Room1;

namespace Room2
{
    public class LineDrawer1 : MonoBehaviour
    {
        //Line
        public LineRenderer line;
        private Vector3 previousPosition;
        [SerializeField] float minDistance = 0.1f;
        public Follower follower;
        public float yvalue;


        // Start is called before the first frame update
        void Start()
        {
            line = GetComponent<LineRenderer>();
            line.positionCount = 1;
            previousPosition = transform.position;
        }

        public void StartLine(Vector2 position)
        {
            line.positionCount = 1;
            line.SetPosition(0, position);
        }

        // Update is called once per frame
        public void UpdateLine()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPosition.z = -3;
                currentPosition.y = yvalue;

                if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
                {
                    if (previousPosition == transform.position)
                    {
                        line.SetPosition(0, currentPosition);
                    }
                    else
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, currentPosition);
                    }

                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPosition);
                    previousPosition = currentPosition;
                }
            }
        }
    }
}


