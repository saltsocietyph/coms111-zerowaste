using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceGenerator : MonoBehaviour {

    public Texture2D nodeMap;
    public GameObject parentMap;
    public ColorToNode[] colorMappings;

    private Vector3[] worldCorners;
    private List<ColorToNode> nodeMappingList;
    private ColorToNode[] nodeMapping;
    private List<Vector2> nodePositionList;
    private Vector2[] positions;

    private List<GameObject> nodeList;
    private GameObject[] nodes;

    private int frame = 0;
    private int counter = 0;

    private bool isFinished = false;

	void Start () {

        worldCorners = new Vector3[4];
        parentMap.GetComponent<RectTransform>().GetWorldCorners(worldCorners);

        nodePositionList = new List<Vector2>();
        nodeMappingList = new List<ColorToNode>();
        nodeList = new List<GameObject>();

        GenerateInstance();
	}

    void Update()
    {
        frame++;

        if (counter < nodeMappingList.Count)
        {
            if (frame >= 10)
            {
                InstantiateNode(nodeMapping[counter], positions[counter]);
                frame = 0;
                counter++;
            }
        }
        else
            isFinished = true;
    }

    void GenerateInstance()
    {
        for (int x = 0; x < nodeMap.width; x++)
        {
            for (int y = 0; y < nodeMap.height; y++)
            {
                GetNodePosition(x, y);
            }
        }

        nodeMapping = nodeMappingList.ToArray();
        positions = nodePositionList.ToArray();
    }

    void GetNodePosition(int x, int y)
    {
        Color pixelColor = nodeMap.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach (ColorToNode colorMapping in colorMappings)
        {
            if (colorMapping.nodeColor.Equals(pixelColor))
            {
                Vector2 pixelPos = new Vector2(x, y);
                Vector2 worldPos = new Vector2();

                worldPos.x = worldCorners[0].x + pixelPos.x;
                worldPos.y = worldCorners[0].y + pixelPos.y;

                nodeMappingList.Add(colorMapping);
                nodePositionList.Add(worldPos);
            }
        }
        
    }

    void InstantiateNode(ColorToNode colorMapping, Vector2 worldPos)
    {
        GameObject node = (GameObject) Instantiate(colorMapping.prefab, worldPos, Quaternion.identity, parentMap.transform);
        nodeList.Add(node);

        Debug.Log(worldPos.x + ", " + worldPos.y + ": Node Instantiated.");
    }

    void SetNodeDetails()
    {

    }

    public Vector2[] GetNodePositionArray()
    {
        return positions;
    }

    public ColorToNode[] GetNodesArray()
    {
        return nodeMapping;
    }

    public bool isNodeGenerationFinished()
    {
        return isFinished;
    }
}
