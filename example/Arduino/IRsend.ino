#include <boarddefs.h>
#include <IRremote.h>
#include <IRremoteInt.h>
#include <ir_Lego_PF_BitStreamEncoder.h>

IRsend irsend;

void setup() {
  Serial.begin(9600);
}

  unsigned int Power_ON[] = {3250,3400, 750,2550, 800,2550, 800,2550, 750,2550, 800,2550, 750,2550, 800,2550, 750,900, 750,900, 800,900, 800,850, 750,900, 800,900, 750,900, 750,900, 750,900, 800,850, 800,900, 800,850, 800,2550, 750,2550, 800,2550, 800,2550, 750,2550, 750};
  unsigned int Switch_UP[] = {3300,3350, 800,2500, 800,2550, 800,2500, 850,2500, 800,2550, 750,2550, 800,2550, 800,2500, 800,850, 800,900, 750,2550, 750,950, 800,850, 800,850, 800,850, 850,850, 800,850, 800,850, 800,900, 800,850, 750,2550, 800,2550, 800,850, 750,2600, 800};
  unsigned int Switch_DOWN[] = {3300,3350, 800,2550, 800,2500, 850,2500, 800,2500, 850,2500, 800,2500, 850,850, 800,850, 850,2500, 800,850, 800,2500, 850,850, 800,850, 800,850, 850,850, 800,850, 800,850, 800,850, 850,2500, 800,2500, 850,850, 800,2500, 800,900, 800,2500, 800};
  int d;
  
void loop() {
  if( (d = Serial.read())!= -1 ){
    switch(d){
      case '1':
      for(int i = 0; i < 5; i++){
        irsend.sendRaw(Power_ON, sizeof(Power_ON) / sizeof(Power_ON[0]), 38);
        delay(30);
      }
      Serial.println("powON/OFF");
      break;
      case '2':
      for(int i = 0; i < 5; i++){
        irsend.sendRaw(Switch_UP, sizeof(Switch_UP) / sizeof(Switch_UP[0]), 38);
        delay(30);
      }
      Serial.println("NEXT send");
      break;
      case '3':
      for(int i = 0; i < 5; i++){
        irsend.sendRaw(Switch_DOWN, sizeof(Switch_DOWN) / sizeof(Switch_DOWN[0]), 38);
        delay(30);
      }
      Serial.println("PREV send");
      break;
    }
  }
}
