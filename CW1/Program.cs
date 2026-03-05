using CW1.Drivers.Buses;
using CW1.Drivers.Buses.I2C;
using CW1.Drivers.Buses.SPI;

var mock_i2c = new MockI2C();
var mock_spi = MockSPI.Instance();

var some_kind_driver_btw = (Bus)new MockI2C();

Console.WriteLine(mock_i2c);
Console.WriteLine(mock_spi);

mock_i2c.WriteByte(0x67);
mock_i2c.enabled = true;
mock_i2c.WriteByte(0x67);

mock_spi.WriteByte(0x42);
mock_spi.WriteByte(0x42);

some_kind_driver_btw.WriteByte(0x12);
some_kind_driver_btw.WriteByte(0x34);
some_kind_driver_btw.WriteByte(0x56);