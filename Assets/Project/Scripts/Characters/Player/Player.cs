namespace Project.Scripts.Characters.Player
{
    public class Player
    {
        public Character Character { get; private set; }

        public Player(Character character)
        {
            Character = character;
        }
    }
}