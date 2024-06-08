using Godot;
using System;

public partial class levels_page : Node2D
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

	private void _on_1_pressed()
	{
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/level_1.tscn") ;
	}


	private void _on_2_pressed()
	{
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/level_2.tscn") ;
	}


	private void _on_3_pressed()
	{
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/level_3.tscn") ;
	}

	private void _on_4_pressed()
	{
		click.Play();
		GetTree().ChangeSceneToFile("res://Scenes/level_4.tscn") ;
	}

}








