
#include <Arduino.h>
// the setup routine runs once when you press reset:
extern volatile unsigned long timer0_millis;
unsigned long start_time; 
unsigned long timed_event;
unsigned long current_time;
void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  timed_event = 300; // after 300 ms trigger the event this is response time
	current_time = millis();
	start_time = current_time; 
}
unsigned int last_value_A0=0;
unsigned int last_value_A1=0;
unsigned int last_value_A2=0;
unsigned int last_value_A3=0;
unsigned int last_value_A4=0;
unsigned int last_value_A5=0;
unsigned int latest_value=0;
// the loop routine runs over and over again forever:
void loop() {
	current_time = millis(); // update the timer every cycle
	if (current_time - start_time >= timed_event) {
	//	Serial.println("Timer expired, resetting"); // the event to trigger
		start_time = current_time;  // reset the timer
    nonblockingloop();
	}
  if(millis()>100000){// reset 
        noInterrupts ();
        timer0_millis = 0;
        interrupts ();
  }
}
void SerialWriter(const char *variable,const int value){
  int len = strlen(variable)+strlen("=")+strlen(value)+strlen(",");
  char str[len]; 

  strcpy(str, variable);
  strcat (str,"=");
  char temp[10]; 
  itoa(value, temp, 10);//value temp redix 10 is decimal 8 is octal 2 is binary
  strcat (str, temp); 
  strcat (str,",");
  strcat (str,"\n");
  Serial.write(str);
}
void nonblockingloop(){
    // read the input on analog pin 0:
  int sensorValue = analogRead(A0)/8;
  int sensorValue1 = analogRead(A1)/8;
  int sensorValue2 = analogRead(A2)/8;
  int sensorValue3 = analogRead(A3)/8;
  int sensorValue4 = analogRead(A4)/8;
  int sensorValue5 = analogRead(A5)/8;

  if(last_value_A0!=sensorValue){
    last_value_A0=sensorValue;
    latest_value=sensorValue;
    SerialWriter("X0",sensorValue);
   // Serial.println("A0");
  }
  if(last_value_A1!=sensorValue1){
    last_value_A1=sensorValue1;
    latest_value=sensorValue1;
    SerialWriter("X1",sensorValue1);
    //Serial.println("A1");
  
  }
  if(last_value_A2!=sensorValue2){
    last_value_A2=sensorValue2;
    latest_value=sensorValue2;
    SerialWriter("X2",sensorValue2);
    //Serial.println("A2");
  }
   if(last_value_A3!=sensorValue3){
    last_value_A3=sensorValue3;
    latest_value=sensorValue3;
    SerialWriter("X3",sensorValue3);
    //Serial.println("A3");
  }
  if(last_value_A4!=sensorValue4){
    last_value_A4=sensorValue4;
    latest_value=sensorValue4;
    SerialWriter("X4",sensorValue4);
  }
   if(last_value_A5!=sensorValue5){
    last_value_A5=sensorValue5;
    latest_value=sensorValue5;
    SerialWriter("X5",sensorValue5);
  }

}
