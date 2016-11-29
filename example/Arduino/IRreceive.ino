#include <boarddefs.h>
#include <IRremote.h>
#include <IRremoteInt.h>
#include <ir_Lego_PF_BitStreamEncoder.h>

int RECV_PIN = 11;
IRrecv irrecv(RECV_PIN); //初始化紅外線訊號輸入

decode_results results; //儲存信號結構

void setup() {
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
}

int codeType = -1;

void storeCode(decode_results *results){
  int count = results->rawlen;
  codeType = results->decode_type;
  if(codeType == UNKNOWN){
    Serial.print("Unknown encoding: ");
  }
  else if(codeType == NEC){
    Serial.print("Decoded NEC: ");
  }
  else if(codeType == SONY){
    Serial.print("Decoded SONY: ");
  }
  else if(codeType == RC5){
    Serial.print("Decoded RC5: ");
  }
  else if(codeType == RC6){
    Serial.print("Decoded RC6: ");
  }
  else if(codeType == PANASONIC){
    Serial.print("Decoded PANASONIC: ");
  }
  else if(codeType == LG){
    Serial.print("Decoded JVC: ");
  }
  else if(codeType == JVC){
    Serial.print("Decoded JVC: ");
  }
  Serial.print(results->value,HEX);
  Serial.print("(");
  Serial.print(results->bits,DEC);
  Serial.print("bits)");
  Serial.print("unsigned int YourVariableName[");
  Serial.print(count-1,DEC);
  Serial.print("]={");
  
  for(int i = 1; i < count; i++){
    if( (i%2) == 1 ){
      Serial.print(results->rawbuf[i]*USECPERTICK,DEC);
    }
    else{
      Serial.print(abs(-(int)results->rawbuf[i]*USECPERTICK),DEC);
    }
    if(i<count-1){
      Serial.print(",");
    }
  }
  Serial.println("};");
}

void loop() {
  if(irrecv.decode(&results)){
    Serial.println(results.value,HEX);
    storeCode(&results);
    irrecv.resume();
  }
  delay(200);
}
