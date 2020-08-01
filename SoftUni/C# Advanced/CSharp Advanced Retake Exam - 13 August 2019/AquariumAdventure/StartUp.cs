namespace AquariumAdventure
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium("Ocean", 5, 15);

            Fish fish = new Fish("Goldy", "gold", 4);

            System.Console.WriteLine(fish.ToString());

            aquarium.Add(fish);

            System.Console.WriteLine(aquarium.Remove("Goldy"));

            Fish secondFish = new Fish("Dory", "blue", 2);
            Fish thirdFish = new Fish("Nemo", "orange", 5);

            aquarium.Add(secondFish);
            aquarium.Add(thirdFish);

            System.Console.WriteLine(aquarium.Report());
        }
    }
}
