//https://create.arduino.cc/editor/id4ni10/b90d2a74-7893-4b01-a639-3e24acff5815
#include "DHT.h"
 
#define DHTPIN A3 // pino que estamos conectado
#define DHTTYPE DHT11 // DHT 11

DHT dht(DHTPIN, DHTTYPE);

int pin1 = 10;
int pin2 = 11;
int pin3 = 12; //    __6__
int pin4 = 9;  // 5 |__4__| 7
int pin5 = 6;  // 1 |__2__| 3
int pin6 = 5;
int pin7 = 4;
int gnd1 = 2; // gnd1 is display 1's gnd
int gnd2 = 3; // gnd2 is display 2's gnd
int timer = 25;

int temperatura;

void setup(){
  Serial.begin(9600);
  
  pinMode(pin1, OUTPUT);
  pinMode(pin2, OUTPUT);
  pinMode(pin3, OUTPUT);
  pinMode(pin4, OUTPUT);
  pinMode(pin5, OUTPUT);
  pinMode(pin6, OUTPUT);
  pinMode(pin7, OUTPUT);
  pinMode(gnd1, OUTPUT);
  pinMode(gnd2, OUTPUT);
  
  dht.begin();
}

void loop() {
  // testa se retorno é valido, caso contrário algo está errado.
  //float h = dht.readHumidity();
  temperatura = dht.readTemperature();
  
  if (!isnan(temperatura))
    printNumbers(temperatura / 10, temperatura % 10);
  
  Serial.println(temperatura);
}

void printAllNumbers(){
  for(int x = 0; x < 10; x++){
    for(int y = 0; y < 10; y++){
      printNumbers(x, y);
    }
  }
}

void zero(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, LOW);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, HIGH);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void um(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, HIGH);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, HIGH);
   digitalWrite(pin5, HIGH);
   digitalWrite(pin6, HIGH);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void dois(int pin){
  digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, LOW);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, HIGH);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, HIGH);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void tres(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, HIGH);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void quatro(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, HIGH);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, HIGH);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void cinco(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, HIGH);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void seis(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, LOW);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, HIGH);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void sete(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, HIGH);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, HIGH);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void oito(int pin){
  digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, LOW);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void nove(int pin){
   digitalWrite(pin, HIGH);
   
   digitalWrite(pin1, HIGH);
   digitalWrite(pin2, LOW);
   digitalWrite(pin3, LOW);
   digitalWrite(pin4, LOW);
   digitalWrite(pin5, LOW);
   digitalWrite(pin6, LOW);
   digitalWrite(pin7, LOW);
   
   delay(3);
   digitalWrite(pin, LOW);
}

void printNumbers(int left, int right){
  for (int i = 0; i < timer; i++){
    resolve(left, gnd1);
    resolve(right, gnd2);
  }
}

void resolve(int number, int pin){
  switch(number){
     case 1:
      um(pin);
     break;
     case 2:
      dois(pin);
     break;
     case 3:
      tres(pin);
     break;
     case 4:
      quatro(pin);
     break;
     case 5:
      cinco(pin);
     break;
     case 6:
      seis(pin);
     break;
     case 7:
      sete(pin);
     break;
     case 8:
      oito(pin);
     break;
     case 9:
      nove(pin);
     break;
     default:
      zero(pin);
     break;
  }
}
