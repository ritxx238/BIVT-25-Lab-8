namespace CW1.Drivers.Buses
{
    public abstract class Bus(ushort id)
    {
        static ushort busses_created = 0;
        readonly ushort id = id;
        public ushort ID => this.id;

        public static ushort AutoID() => busses_created++;

        public override string ToString() => $"{this.GetType().FullName}{{id: {this.id},}}";
    
        public abstract bool WriteByte(byte b);
    }
}