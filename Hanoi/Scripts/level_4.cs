using Godot;
using System;
using System.Linq;

public partial class level_4 : Node2D
{
	Label nbDeplacementLabel ;
	Boolean draged = false;
	int numeroDisqueDraged = 0;
	Vector2 ancienePosition = new Vector2();
	int valeur = 0 ;
	Vector2 mouse = new Vector2();
	AudioStreamPlayer mov ;
	AudioStreamPlayer erreur ;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nbDeplacementLabel = GetNode<Label>("nbDeplacement") ;
		levelGenerator niveau = new levelGenerator(4) ;
		for (int i = 0; i < levelGenerator.nbDisqueTotal; i++)
		{
			levelGenerator.disque.Add(GetNode<Sprite2D>("disque" + (i+1))) ;
		}
		mov = GetNode<AudioStreamPlayer>("mov") ;
		erreur = GetNode<AudioStreamPlayer>("erreur");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mouse = GetGlobalMousePosition() ;
		levelGenerator.Drag_and_Drop(ref draged ,ref valeur , ref numeroDisqueDraged , ancienePosition , mouse , mov, erreur) ;
		//Drag_and_Drop();

		nbDeplacementLabel.Text = "" + levelGenerator.nbDeplacement ;


		if(levelGenerator.nbDisqueC == levelGenerator.nbDisqueTotal)
		{
			levelGenerator.Supprimer();
			GD.Print("Win !!!!!");
			GetTree().ChangeSceneToFile("res://Scenes/level_4_complete.tscn") ;
		}
		else if (levelGenerator.nbDeplacement == 0)
			{
				levelGenerator.Supprimer() ;
				GetTree().ChangeSceneToFile("res://Scenes/level_4_game_over.tscn");
			}
	}

	private void _on_a_vers_b_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautA ,ref levelGenerator.disqueHautB ,ref levelGenerator.nbDisqueA
									,ref levelGenerator.nbDisqueB ,levelGenerator.piquetA ,levelGenerator.piquetB 
									,levelGenerator.piquetC,565,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_a_vers_c_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautA ,ref levelGenerator.disqueHautC ,ref levelGenerator.nbDisqueA
									,ref levelGenerator.nbDisqueC ,levelGenerator.piquetA ,levelGenerator.piquetC 
									,levelGenerator.piquetB,860,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_b_vers_a_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautB ,ref levelGenerator.disqueHautA ,ref levelGenerator.nbDisqueB
									,ref levelGenerator.nbDisqueA ,levelGenerator.piquetB ,levelGenerator.piquetA 
									,levelGenerator.piquetC,260,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_b_vers_c_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautB ,ref levelGenerator.disqueHautC ,ref levelGenerator.nbDisqueB
									,ref levelGenerator.nbDisqueC ,levelGenerator.piquetB ,levelGenerator.piquetC 
									,levelGenerator.piquetA,860,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_c_vers_a_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautC ,ref levelGenerator.disqueHautA ,ref levelGenerator.nbDisqueC
									,ref levelGenerator.nbDisqueA ,levelGenerator.piquetC ,levelGenerator.piquetA 
									,levelGenerator.piquetB,260,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_c_vers_b_pressed()
	{
		levelGenerator.movDisque(ref levelGenerator.disqueHautC ,ref levelGenerator.disqueHautB ,ref levelGenerator.nbDisqueC
									,ref levelGenerator.nbDisqueB ,levelGenerator.piquetC ,levelGenerator.piquetB 
									,levelGenerator.piquetA,565,mov,erreur);
		levelGenerator.Afficher();
	}


	private void _on_button_button1_down()
	{
		GD.Print("peek ",levelGenerator.isPeek(1)) ;
		if(levelGenerator.isPeek(1) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 0 ;
			ancienePosition = levelGenerator.disque.ElementAt(0).Position ;
		}
	}


	private void _on_button_button1_up()
	{
		draged = false;
	}


	private void _on_button_button2_down()
	{
		if(levelGenerator.isPeek(2) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 1 ;
			ancienePosition = levelGenerator.disque.ElementAt(1).Position ;
		}
	}


	private void _on_button_button2_up()
	{
		draged = false;
	}


	private void _on_button_button3_down()
	{
		if(levelGenerator.isPeek(3) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 2 ;
			ancienePosition = levelGenerator.disque.ElementAt(2).Position ;
		}
	}


	private void _on_button_button3_up()
	{
		draged = false;
	}


	private void _on_button_button4_down()
	{
		if(levelGenerator.isPeek(4) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 3 ;
			ancienePosition = levelGenerator.disque.ElementAt(3).Position ;
		}
	}

	private void _on_button_button4_up()
	{
		draged = false;
	}


	private void _on_button_button5_down()
	{
		if(levelGenerator.isPeek(5) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 4 ;
			ancienePosition = levelGenerator.disque.ElementAt(4).Position ;
		}
	}

	private void _on_button_button5_up()
	{
		draged = false;
	}

	private void _on_button_button6_down()
	{
		if(levelGenerator.isPeek(6) == 2)
		{
			draged = true ;
			numeroDisqueDraged = 5 ;
			ancienePosition = levelGenerator.disque.ElementAt(5).Position ;
		}
	}

	private void _on_button_button6_up()
	{
		draged = false;
	}


	private void Afficher()
	{
		GD.Print("A : (", levelGenerator.nbDisqueA , "," , levelGenerator.disqueHautA , ")");
		for(int i = 0; i < levelGenerator.piquetA.Count ; i++)
		{
			GD.Print(levelGenerator.piquetA.ElementAt(i));
		}
		GD.Print("B : (", levelGenerator.nbDisqueB , "," , levelGenerator.disqueHautB , ")");
		for(int i = 0; i < levelGenerator.piquetB.Count ; i++)
		{
			GD.Print(levelGenerator.piquetB.ElementAt(i));
		}
		GD.Print("C : (", levelGenerator.nbDisqueC , "," , levelGenerator.disqueHautC , ")");
		for(int i = 0; i < levelGenerator.piquetC.Count ; i++)
		{
			GD.Print(levelGenerator.piquetC.ElementAt(i));
		}
		GD.Print("=============");
	}

	private void Drag_and_Drop()
	{
		if(draged)
		{
			valeur = 1 ;
			levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = GetGlobalMousePosition() ;
		}
		if(!draged && valeur == 1)
		{
			valeur = -1 ;
			if(levelGenerator.canDrop(levelGenerator.disque.ElementAt(numeroDisqueDraged).Position.X , levelGenerator.disque.ElementAt(numeroDisqueDraged).Position.Y))
			{
				int hash = (3*levelGenerator.trouverFrom(ancienePosition.X) + levelGenerator.trouverTo(levelGenerator.disque.ElementAt(numeroDisqueDraged).Position.X)) * levelGenerator.canMov(numeroDisqueDraged) ;
				// if hash == 0 ==> we can't mov 
				GD.Print("hash = ", hash);
				switch (hash)
				{
					case 7 : _on_a_vers_b_pressed();
						break;
					case 9 : _on_a_vers_c_pressed();
							break;
					case 11 : _on_b_vers_a_pressed();
							break;
					case 15 : _on_b_vers_c_pressed();
							break;
					case 17 : _on_c_vers_a_pressed();
							break;
					case 19 : _on_c_vers_b_pressed();
							break;
					default : levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = ancienePosition ;
							break;
				}
				
			}
			else
			{
				levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = ancienePosition ;
			}
		}
	}

}

