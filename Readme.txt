# Ifosup_Jeu
IArgent 
  valeur
IPotion
  nombre
Icuir
  nombre
IArme
  nom
  force
IArmure
  nom
  resistacne
  
Class Abstract Personnage 
  nom
  ptVie
  force   // bonus humain / centor
  resitance // bonus nain / orque
  vitesse  // bonus elfe / loup
  SeDeplace()
  Frappe()
  
Class Abstract Hero : Personnage, IArgent, IPotion, ICuir, IArmure, IArme
  SeSoigne()
  SeSauve()
  Achete()
  Vend()
  
Class Humain : Hero
Class Nain : Hero
Class Elfe : Hero
  
Class Abstract Monstre : Personnage, IArgent, IPotion

Class Loup : Monstre, Icuir
Class Orque : Monstre, IArmure 
Class Centor : Monstre, IArme

Class Marcher : ICuir, IArmure, IArme, IPotion, IArgent

List<IArme> armes
List<IArmure> armures
List<IPotion> potions
List<Icuir> cuirs

// dans le constructeur remplir les listes grace a une fonction dans la class Outils

Class Static Outils
// ici nous feront tout les fonctions tels celle qui gere les combats, gere les valeur des proprité lors de l'instanciation des class