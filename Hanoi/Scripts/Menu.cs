using Godot;
using System;

public partial class Menu : Node2D
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
	private void _on_play_pressed()
	{
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/welcome.tscn") ;
	}
	private void _on_quit_pressed()
	{
		click.Play();
		GetTree().Quit() ;
	}
}





