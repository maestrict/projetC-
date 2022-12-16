using HeroesVSMonster;
using HeroesVSMonster.Game.Map;
using HeroesVSMonster.Models.Hero;
using System.Media;
using System.Reflection;
using System.Xml;

// //SoundPlayer player = new SoundPlayer();
// string fullPathToSound = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\flutemusicidea.wav");
// string pathToSound = fullPathToSound.Replace("bin\\Debug\\net6.0", "");
// player.SoundLocation = pathToSound;
// //player.SoundLocation = "C:\\Users\\bster\\Desktop\\Perso\\Jeux C#\\HvsM\\Ifosup_Jeu-master\\Ifosup_Jeu-master\\HeroesVSMonster\\Audio\\flutemusicidea.wav";
// player.Play();

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Bienvenue dans la forêt de « Shorewood », forêt enchantée du pays de « Stormwall »");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine();

string? heroChoise = "";
while (heroChoise != "H" && heroChoise != "N" && heroChoise != "E")
{
    Console.WriteLine("Choisissez un personnage");
    Console.Write("Humain = H ou Nain = N ou Elfe = E: ");
    heroChoise = Console.ReadLine();
}
Console.WriteLine();

string? heroName = "";
bool isAssigne = false;
while (!isAssigne)  //assignation obligatoire d'une lettre au debut du nom
{
    Console.Write("Choisissez un nom : ");
    heroName = Console.ReadLine();
    if (!String.IsNullOrEmpty(heroName) && Char.IsLetter(heroName[0]))
    {
        isAssigne = true;
    }
}

Console.WriteLine();

bool isSizeGameValid = false;
int sizeGame = 0;
while (!isSizeGameValid) 
{
    Console.Write("Choisissez la taille du jeu entre 3 - 39 : ");
    isSizeGameValid = int.TryParse(Console.ReadLine(), out sizeGame);
    if (sizeGame > 39 || sizeGame < 3)
    {
        isSizeGameValid = false;
    }
}
Console.WriteLine();

bool isValidNumber = false;
int monstersQty = 0;
while (!isValidNumber || monstersQty > (sizeGame * sizeGame)-2 -sizeGame )
{
    Console.Write($"Choisissez le nombre de monstres a combattre maximum {((sizeGame * sizeGame) - 2 - sizeGame)} : ");
    isValidNumber = int.TryParse(Console.ReadLine(), out monstersQty);
}
Console.Clear();
Console.WriteLine();
Console.WriteLine();

StartGame toolsGame = new StartGame(monstersQty,heroChoise,heroName,sizeGame);


ConsoleKeyInfo keyinfo;
do
{
    keyinfo = Console.ReadKey();
    toolsGame.KeyDown(keyinfo);
}
while (keyinfo.Key != ConsoleKey.X);

