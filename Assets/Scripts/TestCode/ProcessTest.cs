using UnityEngine;
using System.Collections;

public class ProcessTest : MonoBehaviour
{

    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }

    public ArrayList pathArray;

    GameObject objStartCube, objEndCube;

    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f; //Interval time between path finding

    // Use this for initialization
    void Start()
    {
        objStartCube = GameObject.FindGameObjectWithTag("Start");
        objEndCube = GameObject.FindGameObjectWithTag("End");

        //AStar Calculated Path
        pathArray = new ArrayList();
        FindPath();
    }

    // Update is called once per frame
    void Update()
    {
//        elapsedTime += Time.deltaTime;
//
//        if (elapsedTime >= intervalTime)
//        {
//            elapsedTime = 0.0f;
//            FindPath();
//        }
    }

    void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        //Assign StartNode and Goal Node
        startNode =
            new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

        StartCoroutine(AStar.FindPath(startNode, goalNode,pathArray));
    }


    void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            FindPath();
        }
    }

    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach (Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node) pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.green);
                    index++;
                }
            }
            ;
        }

        if (AStar.openList != null)
        {
            Color tempColor = Gizmos.color;
            Gizmos.color = Color.green;
            for(int i=0;i< AStar.openList.Length;i++)
            {
                var node = AStar.openList.nodes[i] as Node;
                Gizmos.DrawSphere(node.position,0.5f);
            }
            Gizmos.color = tempColor;
        }

        if (AStar.closedList != null)
        {
            Color tempColor = Gizmos.color;
            Gizmos.color=Color.red;
            
            for (int i = 0; i < AStar.closedList.Length; i++)
            {
                var node = AStar.closedList.nodes[i] as Node;
                Gizmos.DrawSphere(node.position, 0.5f);
            }
            Gizmos.color = tempColor;
        }
    }
}
