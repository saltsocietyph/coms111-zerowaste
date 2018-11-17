using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour {

    public InstanceGenerator instanceGenerator;
    private Vector2[] positions;
    private ColorToNode[] nodes;

    private DrawPath drawPath;

    private int frame = 0;
    private int counter = 0;

    private int startOfLine = 0;
    private int endOfLine = 1;

	void Start () {
        drawPath = GetComponent<DrawPath>();
	}

    void Update()
    {
        if (!instanceGenerator.isNodeGenerationFinished())
            return;

        positions = instanceGenerator.GetNodePositionArray();

        frame++;
        if (counter < positions.Length - 1)
        {
            if (frame >= 10)
            {
                drawPath.DrawLine(positions[counter], positions[counter + 1], positions.Length, startOfLine, endOfLine);
                drawPath.EnableDraw(true);

                frame = 0;
                counter++;

                startOfLine++;
                endOfLine++;
            }
        }
    }
}
