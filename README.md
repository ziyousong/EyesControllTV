# EyesControllTV
This repository contains a .NET library and a Arduino library that can be used to switching to a channel by eyes.

The .NET library of classes for programmers to make it easy to connect to an arduino via usb, and can catch gaze point data simply.

What you need to know before you begin using this library:

1. Prepare 3 device: Tobii EyeX, IR Transmitter, IR Receiver.
2. This repository includes IRremote Arduino library on github(https://github.com/z3t0/Arduino-IRremote)

##Getting Started
##Arduino
Reference link: https://github.com/z3t0/Arduino-IRremote
###Installation
1. Download ZIP from <a href="https://github.com/ziyousong/EyesControllTV">Homepage</a>.
2. Extract the zip file.
3. Move the "IRremote" folder that has been extracted to your libraries directory.
4. Make sure to delete Arduino_Root/libraries/RobotIRremote. Where Arduino_Root refers to the install directory of Arduino. The library RobotIRremote has similar definitions to IRremote and causes errors.

###Code example: Receive infrared signals
```
#include <IRremote.h>

int RECV_PIN = 11;
IRrecv irrecv(RECV_PIN); // Initialize
decode_results results; // Store infrared signals

void setup() {
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
}

void loop() {
  if(irrecv.decode(&results)){
    Serial.println(results.value,HEX);
    irrecv.resume(); // Prepare to receive next signals
  }
  delay(200);
}
```

###Code example: Send infrared signals
An IR Transmitter must be connected to Arduino PWM pin 3.
```
#include <IRremote.h>

IRsend irsend;

void setup() {
  Serial.begin(9600);
}

void loop() {
  if( (d = Serial.read())!= -1 ){
    switch(d){
      case '1': // case 1 PowerON/OFF
      for (int i = 0; i < 3; i++) {
	irsend.sendSony(0xa90, 12); // Send an infrared signals '0xa90' and bit '12'
	delay(40);
      }
      break;
      case '2': // case 2 Switching next channel
      for (int i = 0; i < 3; i++) {
	irsend.sendSony(0xHEX, bit); // Hex is an infrared signals you received
	delay(40);
      }
      break;
      case '3': case 3 Switching prev channel
      for (int i = 0; i < 3; i++) {
	irsend.sendSony(0xHEX, bit);
	delay(40);
      }
      break;
    }
  }
}
```

##.NET C Sharp
###Installation
1. Download ZIP from <a href="https://github.com/ziyousong/EyesControllTV">Homepage</a>.
2. Extract the zip file.
3. Create a WPF project.
4. Right-click on the project node and click Add Reference.
5. Select the components "EyesControllTV.dll" in DotNETLibrary/EyesControllTV/bin/Debug/EyesControllTV.dll, then click OK.

###code example: Connect to Arduino
```
using EyesControllTV;

ArduinoConnection a;

a = new ArduinoConnection("COM3", 9600);
```
###code example: Catch gaze point and send params
In this example the 
```
using EyesControllTV;

ArduinoConnection a;
GazeDataStream stream;
double eyeX, eyeY;

a = new ArduinoConnection("COM3", 9600);
stream = new GazeDataStream();
stream.startEyeTrack();

//most doing in EventHandler
eyeX = stream.getEyeX();
eyeY = stream.getEyeY();

if(eyeX >= 9 && eyeX <= 10 && eyeY >=9 && eyeY <=10){
  a.PowerON(1000);
}
if(eyeX >= 8 && eyeX <= 9 && eyeY >=9 && eyeY <=10){
  a.NextChannel(1000);
}
if(eyeX >= 7 && eyeX <= 8 && eyeY >=9 && eyeY <=10){
  a.PrevChannel(1000);
}
```
###Why does the TV turn on when eyeX on number 10 and eyeY on number 9
EyesControllTV library 
