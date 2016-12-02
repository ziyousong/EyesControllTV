# EyesControllTV
This repository contains a .NET library and a Arduino library that can be used to control TV by eyes.

The .NET library of classes make it easy to connect to an arduino via usb, and get the real-time gaze coordinate simply.

What you need to know before you begin using this library:

1. Prepare 3 devices: Tobii EyeX, IR Transmitter, IR Receiver.
2. This repository includes IRremote Arduino library on github(https://github.com/z3t0/Arduino-IRremote)

##Getting Started
##Arduino
Reference link: https://github.com/z3t0/Arduino-IRremote
###Installation
1. Download ZIP from <a href="https://github.com/ziyousong/EyesControllTV">Homepage</a>.
2. Extract the ZIP file.
3. Move the "IRremote" folder that has been extracted to your libraries directory.
4. Make sure to delete Arduino_Root/libraries/RobotIRremote. Arduino_Root refers to the install directory of Arduino. Because the library RobotIRremote has similar definitions to IRremote, it might cause errors.

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
2. Extract the ZIP file.
3. Create a WPF project.
4. Right-click on the project node and click Add Reference.
5. Select the component DotNETLibrary/EyesControllTV/bin/Debug/EyesControllTV.dll, then click OK.

###Code example: Connect to Arduino
```
using EyesControllTV;

ArduinoConnection a;

a = new ArduinoConnection("COM3", 9600);
```
###Code example: Catch gaze point and send params
In this example the PowerON、NextChannel、PrevChannel function include millisecond parameter that is used to delay an event trigger time.
```
using EyesControllTV;

ArduinoConnection a;
GazeDataStream stream;
double eyeX, eyeY;

a = new ArduinoConnection("COM3", 9600);
stream = new GazeDataStream();
stream.startEyeTrack();

//↓Most programming in EventHandler

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

//↑Most programming in EventHandler
```
###Why does the TV turn on when eyeX and eyeY between 9 ~ 10
The various screen sizes all have been divided by 10 * 10   using EyesControllTV library, then the results are as follows:<p>
<img height="400px" width="700px" src="http://i.imgur.com/YWY4PBH.png"></img>
<p>
Therefore, according to example, the results will be the case:<p>
<img height="400px" width="700px" src="http://i.imgur.com/ingGlI5.png"></img>
<p>
When you gaze at X 9-10 & Y 9-10 block, the TV will be turned on in 2 seconds. And this trigger event was able to arbitrarily change to any block if you want.
