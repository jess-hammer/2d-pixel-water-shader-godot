using Godot;

public partial class FollowCursor : Node2D {
    public override void _PhysicsProcess(double delta) {
        // Get the current cursor position.
        Vector2 cursorPosition = GetGlobalMousePosition();

        // Move the sprite to the cursor position.
        Position = cursorPosition;
    }
}