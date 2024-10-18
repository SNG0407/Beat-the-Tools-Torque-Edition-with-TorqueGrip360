//Motor A : Right
//Motor B : Left
//Motor C : Center

int Motor_A_Speed = 5; //PWM

int Motor_B_Speed = 6;

int Motor_C_Speed = 9;

int normalPWN = 0;
int durationV = 200;
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
    analogWrite(Motor_B_Speed, 12);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    analogWrite(Motor_C_Speed, 10);        // Motor B 속도조절 (0~255)
}
int value = 255;
int loopNum =0;
void loop() {
  if (Serial.available()>0){                 // 만약 시리얼 통신이 온다면
    
    int data = Serial.parseInt();  // 시리얼 통신 값을 문자형변수 data에 저장
    if( data > 0){
      Serial.println(data);                     // 그 값을 출력
      if( data ==1){ //30
          pullingString(0, 255, 255, 500);
          Serial.println("Direction: 30-degrees");
        }
        else if( data ==2){ //90
          pullingString(0, 255, 0, 500);
          Serial.println("Direction: 90-degrees");
        }
        else if( data ==3){ //150
          pullingString(255, 255, 0, 500);
          Serial.println("Direction: 150-degrees");
        }
        else if( data ==4){ //210
          pullingString(255, 0, 0, 500);
          Serial.println("Direction: 210-degrees");
        }
        else if( data ==5){ //270
          pullingString(255, 0, 255, 500);
          Serial.println("Direction: 270-degrees");
        }
        else if( data ==6){ //330
          pullingString(0, 0, 255, 500);
          Serial.println("Direction: 330-degrees");
        }
        else if( data ==7){ //45
          pullingString(0, 251, 184, 500);
          Serial.println("Direction: 45-degrees");
        }
        else if( data ==8){ //135
          pullingString(184, 255, 0, 500);
          Serial.println("Direction: 135-degrees");
        }
       else{
        normalState(normalPWN);
      }
      data=20;
    } 
    
    
    //delayMicroseconds(1);                   // 3초 유지
    // /*모터A설정*/
    // digitalWrite(7, LOW);      // Motor A 방향설정1
    // digitalWrite(8, HIGH);     // Motor A 방향설정2
    // analogWrite(9, 30);      // Motor A 속도조절 (0~255)
    // /*모터B설정*/
    // //digitalWrite(4, HIGH);    // Motor B 방향설정1
    // //digitalWrite(5, LOW);     // Motor B 방향설정2
    // //analogWrite(3, 150);      // Motor B 속도조절 (0~255)
    // delay(3000);    
  }
}