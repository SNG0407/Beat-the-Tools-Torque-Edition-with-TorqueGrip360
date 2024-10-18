//Motor A : 1 left
//Motor B : 2 center
//Motor C : right
const int bufferSize = 64;
char buffer[bufferSize];

int Motor_A_Speed = 5;

int Motor_B_Speed = 6; //PWM


int Motor_C_Speed = 9;

bool GameStartPlag = false;
int normalPWN = 10;
int durationV = 100;

int motorA, motorB, motorC;
int MotorDuration;

bool parseData(char* data, int &motorA, int &motorB, int &motorC, int &MotorDuration) {
    char* token = strtok(data, "#");
    if (token != NULL) {
        motorA = atoi(token);
        token = strtok(NULL, "#");
        if (token != NULL) {
            motorB = atoi(token);
            token = strtok(NULL, "#");
            if (token != NULL) {
                motorC = atoi(token);
                token = strtok(NULL, "#");
                //return true;
                if (token != NULL) {
                MotorDuration = atoi(token);
                return true;
                }
            }
        }
    }
    return false;
}
void setup() {
  Serial.begin(9600);
}

void pullingString(int motorA, int motorB, int motorC, int duration){
    /*모터A설정*/
    analogWrite(Motor_A_Speed, motorA);       // Motor A 속도조절 (0~255)

    /*모터B설정*/
    analogWrite(Motor_B_Speed, motorB);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    analogWrite(Motor_C_Speed, motorC);        // Motor B 속도조절 (0~255)
    
    delay(duration);
    normalState(normalPWN);
}

void normalState(int data){
  /*모터A설정*/
    analogWrite(Motor_A_Speed, 10);       // Motor A 속도조절 (0~255)

    /*모터B설정*/
    analogWrite(Motor_B_Speed, 20);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    analogWrite(Motor_C_Speed, 10);        // Motor B 속도조절 (0~255)
    
    if(data >0){
      //delay(1);
    }
}

void loop() {
  if (Serial.available() > 0) {
        int bytesRead = Serial.readBytesUntil('\n', buffer, bufferSize - 1);
        buffer[bytesRead] = '\0'; // Null-terminate the string

        if( bytesRead ==1){ //0
          pullingString(0, 255, 127, 50);
        }
        else if( bytesRead ==2){ //45
          pullingString(0, 184, 251, 50);
        }
        else if( bytesRead ==3){ //90
          pullingString(0, 0, 255, 200);
        }
        else if( bytesRead ==4){ //135
          pullingString(184, 0, 255, 50);
        }
        else if( bytesRead ==5){ //180
          pullingString(255, 0, 127, 50);
        }
        else if( bytesRead ==6){ // 225
          pullingString(255, 118, 68, 50);
        }
        else if( bytesRead ==7){ // 270
          pullingString(255, 255, 0, 50);
        }
        else if( bytesRead ==8){ // 315
          pullingString(118, 255, 68, 50);
        }
        else
        {
          char motorType = buffer[0];
          int motorA, motorB, motorC;
          if (parseData(buffer + 1, motorA, motorB, motorC, MotorDuration)) {
              pullingString(motorA, motorB, motorC, MotorDuration);
              Serial.println(String(motorType) + " Motors set to: " + motorA + ", " + motorB + ", " + motorC+ ", " + MotorDuration);
          } else {
              Serial.println("Failed to parse data");
          }
        }
    }
}





