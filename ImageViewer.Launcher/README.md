# ADC.Client.Wpf.ImageViewer.Launcher

CLI로 실행되는 Image Viewer 애플리케이션입니다.

## 사용법

### 기본 실행 (기본 경로 사용)
```bash
dotnet run
```

### 특정 경로 지정하여 실행
```bash
dotnet run "C:\Users\YourName\Pictures\Screenshots"
```

### 빌드된 실행 파일 사용
```bash
# 기본 경로로 실행
ADC.Client.Wpf.ImageViewer.Launcher.exe

# 특정 경로로 실행
ADC.Client.Wpf.ImageViewer.Launcher.exe "D:\MyImages"
```

## 파라미터

- **첫 번째 인수**: 이미지 폴더 경로 (선택사항)
  - 제공하지 않으면 기본 경로 사용: `C:\Users\jh.park\Pictures\Screenshots`
  - 경로가 존재하지 않으면 기본 경로로 폴백

## 예시

```bash
# 현재 사용자의 Screenshots 폴더 사용
dotnet run

# 특정 폴더 지정
dotnet run "D:\Photos\Vacation"

# 공백이 포함된 경로 (따옴표 사용)
dotnet run "C:\Users\My Name\Pictures\My Photos"
```

## 특징

- **CLI 파라미터 지원**: 명령줄에서 경로 지정 가능
- **경로 검증**: 존재하지 않는 경로는 기본 경로로 대체
- **따옴표 처리**: 경로의 따옴표 자동 제거
- **콘솔 피드백**: 사용된 경로 정보 출력 