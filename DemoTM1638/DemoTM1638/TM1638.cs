using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

//Lighting API!
namespace DemoTM1638
{
    public class TM1638
    {
        GpioPin m_dataPin, m_clockPin, m_strobePin;
        bool m_initialized = false;
        public TM1638(byte dataPin, byte clockPin, byte strobePin)
        {
            setup(dataPin, clockPin, strobePin);
        }
        private async void setup(byte dataPin, byte clockPin, byte strobePin)
        {
            var gpio = await GpioController.GetDefaultAsync();
            m_dataPin = gpio.OpenPin(dataPin);
            m_clockPin = gpio.OpenPin(clockPin);
            m_strobePin = gpio.OpenPin(strobePin);
            m_dataPin.SetDriveMode(GpioPinDriveMode.Output);
            m_clockPin.SetDriveMode(GpioPinDriveMode.Output);
            m_strobePin.SetDriveMode(GpioPinDriveMode.Output);

            sendCommand(0x40);
            sendCommand(0x80 | 8 | 7);

            m_strobePin.Write(GpioPinValue.Low);
            sendByte(0xC0);
            for (int i = 0; i < 16; i++)
            {
                sendByte(0x00);
            }
            m_strobePin.Write(GpioPinValue.High);
            m_initialized = true;

        }

        public async Task<int> WaitForInitialiation()
        {
            while (!m_initialized) await Task.Delay(100);//Waiting
            return 0;
        }

        void sendCommand(byte cmd)
        {
            m_strobePin.Write(GpioPinValue.Low);
            sendByte(cmd);
            m_strobePin.Write(GpioPinValue.High);
        }

        void sendData(byte address, byte data)
        {
            sendCommand(0x44);
            m_strobePin.Write(GpioPinValue.Low);
            sendByte((byte)(0xC0 | address));
            sendByte(data);
            m_strobePin.Write(GpioPinValue.High);
        }

        void sendByte(byte data)
        {
            for (int i = 0; i < 8; i++)
            {
                m_clockPin.Write(GpioPinValue.Low);
                if ((data & 1) == 0) m_dataPin.Write(GpioPinValue.Low); else m_dataPin.Write(GpioPinValue.High);
                data >>= 1;
                m_clockPin.Write(GpioPinValue.High);
            }
        }
    }
}
