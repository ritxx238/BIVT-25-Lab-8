namespace CW1.Drivers.Buses.I2C
{
    public class MockI2C : Bus
    {
        public bool enabled = false;

        public MockI2C() : base(AutoID()) { }


        public override bool WriteByte(byte b)
        {
            if (!enabled)
            {
                Console.WriteLine($"{this} is not enabled");
                return false;
            }

            return false;
        }

    }
}