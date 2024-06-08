using Godot ;
using System ;
using System.Collections.Generic;
using System.Linq;


public class levelGenerator
{
    public static int xA = 260 , xB = 565 , xC = 860 ;
    public static int nbDisqueTotal , nbDisqueA , nbDisqueB , nbDisqueC , disqueHautA , disqueHautB , disqueHautC , nbDeplacement;
    public static List<Sprite2D> disque ;
    public static Stack<int> piquetA ;
    public static Stack<int> piquetB ;
    public static Stack<int> piquetC ;
    public levelGenerator(int level)
    {
        disque = new List<Sprite2D>();
        piquetA = new Stack<int>();
        piquetB = new Stack<int>();
        piquetC = new Stack<int>();
        nbDisqueTotal = level + 2 ;
        nbDisqueA = level + 2 ;
        nbDisqueB = 0 ;
        nbDisqueC = 0 ;
        disqueHautA = 1 ; 
        disqueHautB = nbDisqueTotal + 1 ;
        disqueHautC = nbDisqueTotal + 1 ;
        nbDeplacement = (int)(2*(Math.Pow(2,nbDisqueTotal) - 1));
        for (int i = nbDisqueTotal; i >= 1 ; i--)
        {
            piquetA.Push(i) ;
        }
    }

    public static void movDisque(ref int disqueHautfrom ,ref int disqueHautTO ,ref int nbDisquefrom ,ref int nbDisqueTO ,Stack<int> from ,Stack<int> TO ,Stack<int> aux ,int x, AudioStreamPlayer mov , AudioStreamPlayer erreur)
    {
        if(disqueHautfrom < disqueHautTO)
        {
            from.Pop() ;
            TO.Push(disqueHautfrom) ;
            disqueHautTO = disqueHautfrom ;
            nbDisquefrom -- ;
            if (nbDisquefrom == 0)
            {
                disqueHautfrom = nbDisqueTotal + 1 ;
            }
            else
            {
                disqueHautfrom = from.Peek() ;
            }
            disque.ElementAt(disqueHautTO - 1).Position = new Vector2(x , 480 - nbDisqueTO * 30) ;
            nbDisqueTO ++ ;
            nbDeplacement -- ;
            mov.Play() ;
        }
        else
        {
            erreur.Play();
            GD.Print("Error !!!"); 
        }
    }

    public static void Supprimer()
	{
		disque.Clear() ;
		piquetA.Clear() ;
		piquetB.Clear() ;
		piquetC.Clear() ;
	}

    public static int isPeek(int i)
    {
        if(piquetA.Count != 0 && i == piquetA.Peek())
            return 2 ;
        if(piquetB.Count != 0 && i == piquetB.Peek())
            return 2 ;
        if(piquetC.Count != 0 && i == piquetC.Peek())
            return 2 ;
        
        return 1 ;
        // 0 : false pro max (piquet vide)
        // 1 : false (not Peek)
        // 2 : true
    }
    
    public static int trouverFrom(float x)
    {
        if(x == xA)
            return 1 ;
        if(x == xB)
            return 3 ;
        return 5 ; // x == xC
    }

    public static int trouverTo(float x)
    {
        if(xA-40 <= x && x <= xA+40)
            return 2 ;
        if(xB-40 <= x && x <= xB+40)
            return 4 ;
        //if(xC-40 <= x && x <= xC+40)
        return 6 ; // xC-40 <= x && x <= xC+40
    }

    public static Boolean canDrop(float x, float y)
    {
        if(xA-40 <= x && x <= xA+40)
            return true ;
        if(xB-40 <= x && x <= xB+40)
            return true ;
        if(xC-40 <= x && x <= xC+40)
            return true ;
        return false ;
    }

    public static int canMov(int from)
    {
        int i = trouverTo(disque.ElementAt(from).Position.X) ;
        switch(i)
        {
            case 2 : if(from+1 < disqueHautA) // i = 2 ==> mov disque To A 
                        return 1 ;
                    else
                        return 0 ;
            case 4 : if(from+1 < disqueHautB)
                        return 1 ;
                    else
                        return 0 ;
            case 6 : if(from+1 < disqueHautC)
                        return 1 ;
                    else
                        return 0 ;
        }
        return 0 ;
    }


    public static void Afficher()
	{
		GD.Print("A : (", nbDisqueA , "," , disqueHautA , ")");
		for(int i = 0; i < piquetA.Count ; i++)
		{
			GD.Print(piquetA.ElementAt(i));
		}
		GD.Print("B : (", nbDisqueB , "," , disqueHautB , ")");
		for(int i = 0; i < piquetB.Count ; i++)
		{
			GD.Print(piquetB.ElementAt(i));
		}
		GD.Print("C : (", nbDisqueC , "," , disqueHautC , ")");
		for(int i = 0; i < piquetC.Count ; i++)
		{
			GD.Print(piquetC.ElementAt(i));
		}
		GD.Print("=============");
	}


    public static void Drag_and_Drop(ref Boolean draged , ref int valeur , ref int numeroDisqueDraged , Vector2 ancienePosition , Vector2 MousePosition ,AudioStreamPlayer mov , AudioStreamPlayer erreur)
	{
		if(draged)
		{
			valeur = 1 ;
			levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = MousePosition/*GetGlobalMousePosition()*/ ;
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
					case 7 : levelGenerator.movDisque(ref levelGenerator.disqueHautA ,ref levelGenerator.disqueHautB ,ref levelGenerator.nbDisqueA
									,ref levelGenerator.nbDisqueB ,levelGenerator.piquetA ,levelGenerator.piquetB 
									,levelGenerator.piquetC,565,mov,erreur);
						break;
					case 9 : levelGenerator.movDisque(ref levelGenerator.disqueHautA ,ref levelGenerator.disqueHautC ,ref levelGenerator.nbDisqueA
									,ref levelGenerator.nbDisqueC ,levelGenerator.piquetA ,levelGenerator.piquetC 
									,levelGenerator.piquetB,860,mov,erreur);
							break;
					case 11 : levelGenerator.movDisque(ref levelGenerator.disqueHautB ,ref levelGenerator.disqueHautA ,ref levelGenerator.nbDisqueB
									,ref levelGenerator.nbDisqueA ,levelGenerator.piquetB ,levelGenerator.piquetA 
									,levelGenerator.piquetC,260,mov,erreur);
							break;
					case 15 : levelGenerator.movDisque(ref levelGenerator.disqueHautB ,ref levelGenerator.disqueHautC ,ref levelGenerator.nbDisqueB
									,ref levelGenerator.nbDisqueC ,levelGenerator.piquetB ,levelGenerator.piquetC 
									,levelGenerator.piquetA,860,mov,erreur);
							break;
					case 17 : levelGenerator.movDisque(ref levelGenerator.disqueHautC ,ref levelGenerator.disqueHautA ,ref levelGenerator.nbDisqueC
									,ref levelGenerator.nbDisqueA ,levelGenerator.piquetC ,levelGenerator.piquetA 
									,levelGenerator.piquetB,260,mov,erreur);
							break;
					case 19 : levelGenerator.movDisque(ref levelGenerator.disqueHautC ,ref levelGenerator.disqueHautB ,ref levelGenerator.nbDisqueC
									,ref levelGenerator.nbDisqueB ,levelGenerator.piquetC ,levelGenerator.piquetB 
									,levelGenerator.piquetA,565,mov,erreur);
							break;
					default : levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = ancienePosition ;
                              erreur.Play();
							break;
				}
				
			}
			else
			{
				levelGenerator.disque.ElementAt(numeroDisqueDraged).Position = ancienePosition ;
                erreur.Play();
			}
		}
	}


}



/*
================ Hash Table================================================================================
for the hash methode :
        A   B   C
from :  1   3   5
to   :  2   4   6

hash = 3*from + to

exemple : from A to B : hash = 3*1 + 4 = 7 so call the method _on_a_vers_b_pressed()

*/