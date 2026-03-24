namespace CW1.Drivers.Buses.SPI
{
    public class MockSPI : Bus
    {
        MockSPI() : base(AutoID()) { }

        static MockSPI? instance = null;

        public static MockSPI Instance()
        {
            if (null == instance) { instance = new MockSPI(); }
            return instance;
        }

        public override bool WriteByte(byte b)
        {
            Console.WriteLine($"{this} SPI cannot handle any errors Ahhhh poor register :<");
            return true;
        }
    }
}