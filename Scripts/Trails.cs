using System.Collections.Generic;
using Godot;

public partial class Trails : Line2D {
    [Export]
    int MAX_LENGTH = 10;
    [Export]
    SubViewport subViewport;
    [Export]
    Node2D parent;

    [Export]
    float distanceAtLargestWidth = 16 * 6;
    [Export]
    float smallestTipWidth;
    [Export]
    float largestTipWidth;

    float length;
    Queue<Vector2> queue = new();
    Vector2I offset;

    public override void _Ready() {
        offset = new Vector2I(subViewport.Size.X / 2, subViewport.Size.Y / 2);
    }

    public override void _Process(double delta) {
        length = 0;

        Vector2 pos = parent.GlobalPosition + offset;
        queue.Enqueue(pos);
        if (queue.Count > MAX_LENGTH && queue.Count > 2) {
            queue.Dequeue();
        }

        ClearPoints();

        Vector2[] queueArray = queue.ToArray();
        for (int i = 0; i < queue.Count - 1; i++) {
            length += queueArray[i].DistanceTo(queueArray[i + 1]);
            AddPoint(parent.ToLocal(queueArray[i]));
        }
        AddPoint(parent.ToLocal(queueArray[queue.Count - 1]));

        float widthValue = Mathf.Lerp(smallestTipWidth, largestTipWidth, Mathf.InverseLerp(0, distanceAtLargestWidth, length));
        WidthCurve.SetPointValue(0, widthValue);
    }

    public void ResetLine() {
        ClearPoints();
        queue.Clear();
    }
}
