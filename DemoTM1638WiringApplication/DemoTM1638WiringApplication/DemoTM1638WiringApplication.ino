#include <TM1638.h>

#define MODULES 4

// define a modules
TM1638 *module;



// Use GPIO pin 5
const unsigned int LED_PIN = GPIO5;
//Do not forget to enable Lighting Driver  = Direct Memory Mapped Driver (faster, less secure)
//http://minwinpc:8080/devicemanager.htm
//
//Arduino library: https://github.com/rjbatista/tm1638-library, copy to 

void setup()
{
    // put your setup code here, to run once:

    pinMode(LED_PIN, OUTPUT);

	module = new TM1638(GPIO19, GPIO13, GPIO6,1,7);

	//module->setDisplayToHexNumber(0x1234ABCD, 0xF0);
	module->setLEDs(0xFF);
}
byte b = 0;
void loop()
{
    // put your main code here, to run repeatedly:

    digitalWrite(LED_PIN, LOW);
    delay(100);
    digitalWrite(LED_PIN, HIGH);
    delay(100);
	module->setLEDs(b);
	b += 1;
	//byte keys = module->getButtons();
	module->setDisplayToHexNumber(b, 0xF0);
	//module->setLED(TM1638_COLOR_RED, 0);

	// light the first 4 red LEDs and the last 4 green LEDs as the buttons are pressed
	//module->setLEDs(((keys & 0xF0) << 8) | (keys & 0xF));

}
