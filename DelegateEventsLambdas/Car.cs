using System;

namespace DelegateEventsLambdas
{
    public class Car
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        private bool carIsDead = false;
        public Car(int CurrentSpeed, int MaxSpeed, string PetName)
        {
            this.CurrentSpeed = CurrentSpeed;
            this.MaxSpeed = MaxSpeed;
            this.PetName = PetName;
        }     
        
        public delegate void CarEngineHandler(string msgForCaller);

        private CarEngineHandler listOfHandlers;

        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }

        public void Accelerate(int delta)
        {
            if(carIsDead){
                if (listOfHandlers!=null){
                    listOfHandlers("The car is dead");
                }
            }
            else{
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed){
                    carIsDead = true;
                    listOfHandlers("You Blew it");
                    return;
                }
                if (MaxSpeed - CurrentSpeed <= 10 && listOfHandlers!= null){
                    listOfHandlers($"Careful its close current speed: {CurrentSpeed}, maxSpeed: {MaxSpeed}");   
                    return;
                }
                System.Console.WriteLine($"CurrentSpeed: {CurrentSpeed}");
            }
        }
    }
}