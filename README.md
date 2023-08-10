# Unity_Learning
유니티 연습 리포지토리

## 유니티 MySQL 8.0버전 보안 이슈 해결 방법

MySQL 서버 설정을 수정하려면 다음과 같은 단계를 따르세요:

MySQL 서버를 중지

MySQL 설치 디렉토리 내에서 'my.ini' 파일을 찾기

'my.ini' 파일 내에서 인증 방식을 수정. 

'caching_sha2_password'를 'mysql_native_password'로 변경하면 Unity의 MySQL Connector/NET과 더욱 호환성이 좋아질 수 있습니다. 해당 부분을 찾아서 다음과 같이 수정해 주세요:

default_authentication_plugin=mysql_native_password

파일을 저장한 후 MySQL 서버를 다시 시작.

이제 MySQL 서버는 'mysql_native_password' 인증 방식을 사용하도록 설정되어 Unity의 MySQL Connector/NET과 원활하게 통신할 수 있어야 함. 

이렇게 함으로써 'caching_sha2_password' 인증 방식 오류를 해결
