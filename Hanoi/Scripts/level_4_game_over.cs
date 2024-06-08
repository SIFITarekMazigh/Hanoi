using Godot;
using System;

public partial class level_4_game_over : Node2D
{
	AudioStreamPlayer click  ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		click = GetNode<AudioStreamPlayer>("click");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_play_again_pressed()
	{	
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/levels_page.tscn");
	}
}



