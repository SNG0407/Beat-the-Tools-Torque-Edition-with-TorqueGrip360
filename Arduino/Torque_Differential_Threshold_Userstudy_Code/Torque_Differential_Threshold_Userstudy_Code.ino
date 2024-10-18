//Motor A : Right
//Motor B : Left
//Motor C : Center

int Motor_A_Speed = 5; //PWM

int Motor_B_Speed = 6;

int Motor_C_Speed = 9;

int normalPWN = 0;
int durationV = 500;
void setup() {
  Serial.begin(9600);

}
void pullingString_A_B(int data, int duration){
    /*모터A설정*/
    analogWrite(Motor_A_Speed, data);       // Motor A 속도조절 (0~255)
    /*모터B설정*/
    analogWrite(Motor_B_Speed, data);       // Motor A 속도조절 (0~255)

    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}
void pullingString_B_C(int data, int duration){
    /*모터C설정*/
    analogWrite(Motor_C_Speed, data);       // Motor A 속도조절 (0~255)
    /*모터B설정*/
    analogWrite(Motor_B_Speed, data);       // Motor A 속도조절 (0~255)

    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}
void pullingString_C_A(int data, int duration){
    /*모터A설정*/
    analogWrite(Motor_A_Speed, data);       // Motor A 속도조절 (0~255)
    /*모터C설정*/
    analogWrite(Motor_C_Speed, data);       // Motor A 속도조절 (0~255)
    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}
void pullingString_C(int data, int duration){
    /*모터A설정*/
    analogWrite(Motor_C_Speed, data);       // Motor A 속도조절 (0~255)

    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}
void pullingString_B(int data, int duration){
    /*모터B설정*/
    analogWrite(Motor_B_Speed, data);       // Motor A 속도조절 (0~255)

    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}
void pullingString_A(int data, int duration){
    /*모터A설정*/
    analogWrite(Motor_A_Speed, data);       // Motor A 속도조절 (0~255)

    if(data >0){
      delay(duration);
    }
    normalState(normalPWN);
}

void pullingString(int data, int duration){
    /*모터A설정*/
    //analogWrite(Motor_A_Speed, data);       // Motor A 속도조절 (0~255)

    /*모터B설정*/
    //analogWrite(Motor_B_Speed, data);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    analogWrite(Motor_B_Speed, data);        // Motor B 속도조절 (0~255)
    
    delay(duration);
    normalState(normalPWN);
    //Serial.println("SSSS");
    //analogWrite(Motor_B_Speed, data);
}
void pullingString_all(int data, int duration){
    /*모터A설정*/
    analogWrite(Motor_A_Speed, data);       // Motor A 속도조절 (0~255)

    /*모터B설정*/
    analogWrite(Motor_B_Speed, data);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    analogWrite(Motor_C_Speed, data);        // Motor B 속도조절 (0~255)
    
    if(data >0){
      delay(duration);
    }
    
    //normalState(normalPWN);
}

void normalState(int data){
  /*모터A설정*/
    //analogWrite(Motor_A_Speed, 10);       // Motor A 속도조절 (0~255)

    /*모터B설정*/
    analogWrite(Motor_B_Speed, 10);        // Motor B 속도조절 (0~255)

    /*모터C설정*/
    //analogWrite(Motor_C_Speed, 10);        // Motor B 속도조절 (0~255)
}
int value = 255;
int loopNum =0;
void loop() {
  if (Serial.available()>0){                 // 만약 시리얼 통신이 온다면
    
    int data = Serial.parseInt();  // 시리얼 통신 값을 문자형변수 data에 저장
    if( data > 0){
      Serial.println(data);                     // 그 값을 출력
      pullingString(data, durationV);
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