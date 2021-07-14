namespace DelegateEventsLambdas
{
    class CarEvents: System.EventArgs
    {
        public CarEvents(string message)
        {
            this.Message = message;
        }
        public string Message {get; private set;}
    }

    class MicrosoftRecommendedEventsWithArgs
    {
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public string PetName { get; set; }

        private bool carIsDead = false;
        public MicrosoftRecommendedEventsWithArgs(int CurrentSpeed, int MaxSpeed, string PetName)
        {
            this.CurrentSpeed = CurrentSpeed;
            this.MaxSpeed = MaxSpeed;
            this.PetName = PetName;
        }     
        
        public event System.EventHandler<CarEvents> Exploded;
        public event System.EventHandler<CarEvents> AboutToBlow;
        public void Accelerate(int delta)
        {
            if(carIsDead){

                Exploded?.Invoke(this, new CarEvents("The Car is dead"));
            }
            else{
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed){
                    carIsDead = true;
                    Exploded?.Invoke(this, new CarEvents("The Car is dead"));
                    return;
                }
                if (MaxSpeed - CurrentSpeed <= 50){
                    AboutToBlow?.Invoke(this, new CarEvents($"Careful its close current speed: {CurrentSpeed}, maxSpeed: {MaxSpeed}"));   
                    return;
                }
                System.Console.WriteLine($"CurrentSpeed: {CurrentSpeed}");
            }
        }
    }
    
}