using System;
using System.Collections.Generic;

namespace ThreeDoors
{
    public enum Outcome { poop, prize }
    public class Program
    {

        //monty hall problem sim
        //Suppose you're on a game show, and you're given the choice of three doors:
        //Behind one door is a prize; behind the others, poop. You pick a door, say No. 1, and the host,
        //who knows what's behind the doors, opens another door, say No. 3, which has a poop. He then says to you,
        //"Do you want to pick door No. 2?" Is it to your advantage to switch your choice?
        static void Main(string[] args)
        {
            //counting prizes for commiting to first choice 
            int poops = 0;
            int prizes = 0;
            //counting prizes for fliping choices after third door is removed 
            int secondChoicePoop = 0;
            int secondChoicePrizes = 0;

            for (int i = 0; i < 10000000; i++)
            {
                var stage = new Stage();
                stage.ChooseDoor();
                if (stage.ChosenDoor.Outcome == Outcome.poop)
                    poops++;
                else
                    prizes++;
            }

            for (int i = 0; i < 10000000; i++)
            {
                var stage = new Stage();
                stage.ChooseDoorAndChange();
                if (stage.ChosenDoor.Outcome == Outcome.poop)
                    secondChoicePoop++;
                else
                    secondChoicePrizes++;
            }

            Console.WriteLine("commiting to first choice:\n"+"poops: \n" + poops + "\nprizes: \n" + prizes);

            Console.WriteLine("choosing again after one poop door is revealed:\n"+"Second choice poops: \n" + secondChoicePoop + "\nSecond choice prizes: \n" + secondChoicePrizes);

        }
        
    }

    public static class Util
    {
        public static Random rand = new Random();
    }
    public class Door
    {
        public Outcome Outcome { get; set; }
        public bool isOpen { get; set; }
        public bool isChosen { get; set; }
        public Door()
        {
            isChosen = false;
            isOpen = false;
        }
        public void Choose()
        {
            isChosen = true;
        }

        public void Open()
        {
            isOpen = true;
        }
    }
    public class Stage
    {
        private List<Door> Doors;
        public Door ChosenDoor { get; private set; }

        public Stage()
        {
            Doors = new List<Door>();
            for(int i = 0;i < 3;i++)
            {
                Doors.Add(new Door());
                    Doors[i].Outcome = Outcome.poop;
            }
            Doors[Util.rand.Next(3)].Outcome= Outcome.prize;
        }

        public void ChooseDoor()
        {
            ChosenDoor=Doors[Util.rand.Next(3)];
        }

        public void ChooseDoorAndChange()
        {
            //simming first choice
            var firstChoice = Util.rand.Next(3);
            ChosenDoor = Doors[firstChoice];
            ChosenDoor.Choose();

            Reveal();
            //switching
            ChosenDoor = Doors.Find(door => !door.isChosen && !door.isOpen);

         
        }
        public void Reveal()
        {
            Doors.Find(door => !door.isChosen && door.Outcome == Outcome.poop).Open();
        }
    }


}
