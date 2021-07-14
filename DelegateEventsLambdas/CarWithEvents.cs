using System;

namespace DelegateEventsLambdas
{
    public class CarWithEvents
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        private bool carIsDead = false;
        public CarWithEvents(int CurrentSpeed, int MaxSpeed, string PetName)
        {
            this.CurrentSpeed = CurrentSpeed;
            this.MaxSpeed = MaxSpeed;
            this.PetName = PetName;
        }     
        
        public delegate void CarEngineHandler(string msgForCaller);
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;
        public void Accelerate(int delta)
        {
            if(carIsDead){

                Exploded?.Invoke("The Car is dead");
            }
            else{
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed){
                    carIsDead = true;
                    Exploded?.Invoke("The Car is dead");
                    return;
                }
                if (MaxSpeed - CurrentSpeed <= 10){
                    AboutToBlow?.Invoke($"Careful its close current speed: {CurrentSpeed}, maxSpeed: {MaxSpeed}");   
                    return;
                }
                System.Console.WriteLine($"CurrentSpeed: {CurrentSpeed}");
            }
        }
    }
}