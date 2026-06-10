## 2026-05-24 1일차

1. Unity 첫 설치

    1-1. Unity Hub 설치  
    1-1-1. Unity Editor 설치  
    1-1-2. 6000.3.16f1 LTS 설치  

2. 설치 모듈 선택
2-1. Microsoft Visual Studio Community
+ Game development with Unity 워크로드 추가
2-2. Documentation
2-3. Windows Build Support

3. Unity 기본 설정
3-1. 설치 경로 C:\UnityProjects 설정 완료

4. Unity 프로젝트 생성해보기
4-1. 프로젝트 - 새 프로젝트 - 2D 템플릿 - 'MabinogiCombatPrototype' 생성

5. Unity Editor 기본 화면 이해
- Hierarchy: 현재 Scene에 있는 오브젝트 목록
- Scene: 오브젝트를 배치하는 작업 화면
- Game: 실제 실행 화면
- Inspector: 선택한 오브젝트의 속성
- Project: 파일/폴더 목록
- Console: 에러와 로그 확인

6. TextMeshPro 준비
6-1. Import TMP Essentials 활성화

7. UI 생성
7-1. 텍스트 만들기(Text - TextMeshPro)
7-2. 버튼 만들기(Button - TextMeshPro)

8. font 지정
8-1. C:\Windows\fonts\malgun.ttf 사용
8-2. 해당 파일을 Unity에서 Project - Assets - Fonts안에 파일 붙여넣기
8-3. 그리고 Window - TextMeshPro - FontAssetCreator에서 Source Font에 해당 폰트 적용 / Auto Sizing, 4096x4096, Custom Characters 적용
8-4. 주로 사용할 단어 미리 입력 후 Generate Font Atlas 클릭

9. C# Script 생성
9-1. Hierarchy에 DifficultyManager 생성
9-2. Project - Assets - Scripts안에 Empty C# Script 생성
9-3. DifficultyUIManager 이름 설정 및 내부 파일의 클래스명도 동일하게 설정
9-4. 저장 후 DifficultyManager의 Inspector에서 ResultText에 Hierarchy에 있는 ResultText 드래그 후 삽입

## 2026-05-25 2일차
1. Assets - Fonts - malgun SDF의 Inspector에서 Static > Dynamics로 바꾸게 되면 없는 글자를 추가하여 한글 오류 해결 가능

2. TMPro: TextMeshPro 관련 기능을 사용하기 위한 네임스페이스

3. System.Collections.Generic: List<T>와 같은 자료구조를 사용하기 위해 필요한 코드

4. MonoBehaviour: Unity에서 스크립트를 게임 오브젝트에 붙일 수 있게 해주는 기본 클래스

5. TMP_Text: 게임 화면/UI에 글자를 표시할 때 필요한 타입. 즉, 유저에게 보여줄 UI 텍스트

6. $"Text" Text를 출력할 수 있게 해주는 문자열
5-1. 만약 특정 코드를 출력하고 싶다면 $"난이도: {data.difficultyName}"

7. N0: 소수점 0 자리까지 콤마를 포함하여 
출력 ex. N0 = 30000 > 30,000 출력 / N2 = 30000 > 30,000.00

8.  private List<DifficultyData> difficultyList;

    void Start()
    {
        difficultyList = new List<DifficultyData>();

        difficultyList.Add(new DifficultyData(
            "보통",
            30000,
            "기본 보상",
            5
        ));
        ...} 에 대한 해석
    8-1. private List<DifficultyData> difficultyList;는 List<DifficultyData>라는 아이스크림 진열대를 difficultyList라는 이름으로 명명한 것. (아직 빈 리스트조차 만들어지지 않음. 그냥 이름만 선언한 상태)  
    8-2. difficultyList = new List<DifficultyData>();는 List<DifficultyData>라는 빈 진열대를 만듦. (이제 변수를 담을 수 있는 리스트를 만듦.)  
    8-3. new DifficultyData(...)는 보통 난이도 아이스크림을 새로 만듦.(어려움, 지옥 난이도도 동일함.)  
    8-4. difficultyList.Add(...);는 만든 아이스크림들을 진열대에 순서에 맞게 올림.([0]: 보통, [1]: 어려움, [2]: 지옥 순으로 쌓임.)  

9. UnityEngine.Debug.Log(...)로 Console 내용 출력 가능

## 2026-05-26 3일차

1. string.IsNullOrEmpty(inputText)가 가능한 이유
1-1. 여기서 string은 문자열 값이 아닌 C#에서 문자열을 다루는 *타입/클래스* 이름임.
1-2. 즉, 문자열 변수를 만들 때는 쓰는 string이 있고 타입/클래스형 string이 있다.
1-3. string 타입 자체가 제공하는 기능 중 하나가 IsNullOrEmpty()

2. !int.TryParse(inputText, out int userPower)와 out int의 의미
2-1. int.TryParse()는 문자열을 정수로 바꿀 수 있는지 확인하는 메서드
ex. int.TryParse(inputText, out int userPower)의 의미
1) inputText를 int로 변환해본다.
2) 성공하면 true를 반환하고 변환된 숫자를 userPower에 저장한다.
3) 실패하면 false를 반환하고 userPower에는 기본값 0이 들어간다.
2-2. 여기서 out int userPower의 의미는 변환에 성공했을 때 결과값을 userPower라는 변수에 담아서 밖으로 내보내 달라는 뜻
2-3. 그렇다면 !int.TryParse(inputText, out int userPower)의 의미는
1) !는 반대의 의미를 가지고 있다.
2) 만약 userPower = 60000이라면 true이므로 false를 반환하기 때문에 if문이 실행되지 않고 userPower에 60000 값을 저장한다.
3) 반대로 userPower = abc라면 false이므로 true를 반환하기 때문에 if문을 실행하고 userPower에는 기본값인 0을 저장한다.

## 2026-05-27 4일차
1. float bestEfficiency = -1f;의 의미
1-1. bestEfficiency = -1.0으로 두어 처음 시작할 때 어떠한 값이 들어와도 추천 후보로 지정한다.
1-2. 만약 0.0f로 실행했다면? > 보상 효율이 0인 경우 문제가 발생되므로 안전하게 -1f로 설정.

## 2026-05-28 5일차
1. UI 정리 및 테스트 케이스 실행
1) UI 영역 정리
2) Hierarchy 이름 정리
3) InputField 숫자 입력 설정
4) 코드 주석 추가
5) 테스트 케이스 8개 확인 ![alt text](image.png)
6) Console 에러 없음
7) Scene 저장 완료

## 2026-05-29, 05-30 6, 7일차
1. 전체 복습 및 점검

## 2026-05-31 8일차
1. ctrl+d = object 복제
1-1. 복제 후 각 Object의 Inspector에서 Onclick() 조정
2. foreach, List 형태로 되어 있어, 난이도를 추가했을 때 손쉽게 추가 가능했다.

## 2026-06-01 9일차
1. new Color(0.8f, 0.9f, 1f): 연한 하늘색 계열
2. 선택된 난이도 버튼을 시각적으로 구분하기 위해 Button 컴포넌트를 코드에 연결하고, 클릭한 버튼의 색상을 변경하는 기능을 구현했다.  
3. 모든 버튼을 기본 색상으로 초기화한 뒤 선택한 버튼만 강조하도록 `ResetButtonColors()`와 `HighlightButton()` 함수를 분리했다.  
4. 이를 통해 난이도 정보 출력뿐 아니라 현재 선택 상태를 UI에서 확인할 수 있도록 개선했다.

## 2026-06-02 10일차
1. 추천 난이도 결과를 단순 표시에서 상세 사유 표시 방식으로 개선했다.  
2. 현재 전투력과 추천 난이도의 권장 전투력을 함께 보여주고, 클리어 가능한 난이도 중 보상 효율이 가장 높다는 근거를 출력했다.  
3. 또한 현재 전투력으로 도달하지 못한 다음 난이도를 찾아 필요한 전투력 차이를 표시하도록 수정했다.  
4. 추천 가능한 난이도가 없는 경우에는 가장 낮은 난이도까지 필요한 전투력을 안내하도록 예외 메시지를 보완했다.
5. foreach (DifficultyData difficulty in difficultyList)의 의미: difficultyList 안의 데이터를 하나씩 꺼낼 때, 현재 꺼낸 데이터를 임시로 difficulty라고 부른다.

## 2026-06-03 11일차
- Reset 버튼을 추가해 입력값과 결과 UI를 초기 상태로 되돌리는 기능을 구현했다.  
- `ResetUI()` 함수에서 전투력 입력값, 난이도 정보, 판단 결과, 추천 결과를 기본 문구로 초기화했고, 9일차에 작성한 `ResetButtonColors()`를 재사용해 선택된 버튼 강조 상태도 함께 초기화했다.  
- Reset 이후 다시 전투력을 입력하고 난이도를 선택했을 때 기존 기능이 정상 동작하는지 확인했다.

## 2026-06-04 12일차
- 보상 효율 계산 기준을 유저가 이해할 수 있도록 UI 표시를 개선했다.  
- 기존에는 보상 효율 점수만 표시했지만, `GetEfficiencyDescription()` 함수를 추가해 보상 점수와 예상 클리어 시간을 함께 보여주도록 수정했다.  
- 선택한 난이도 정보와 추천 사유에 효율 계산 기준을 표시했다.

## 2026-06-05 13일차
- 2주차 기능 추가로 길어진 `DifficultyUIManager.cs`의 구조를 정리했다.  
- public 필드를 Text UI, Input UI, Difficulty Buttons 영역으로 구분하기 위해 `[Header]`를 추가했고, 함수 순서를 초기화, 버튼 이벤트, UI 출력, 입력값 검증, 전투력 판단, 추천 난이도 계산, 버튼 상태 관리, Reset 순서로 재배치했다.  
- 불필요한 주석과 테스트용 로그를 정리하고, Console 로그 형식을 `[Initialize]`, `[SelectDifficulty]`, `[PowerCheck]`, `[Recommend]`, `[Reset]`, `[InputError]` 기준으로 통일했다.  
- 정리 이후 6개 난이도 선택, 추천 난이도, 예외 처리, Reset 기능이 모두 정상 동작하는지 재검증했다.

## 2026-06-06, 06-07 14, 15일차
- 기능 구현 중심이었던 UI를 시연 가능한 형태로 정리했다.  
- 난이도 선택, 선택한 난이도 정보, 전투력 입력, 판단 결과, 추천 결과를 각각 별도 영역으로 구분했다.  
- 6개 난이도 버튼의 크기와 간격을 맞추고, 추천 사유가 잘리지 않도록 RecommendText 영역과 폰트 크기를 조정했다.  
- Canvas Scaler를 1920x1080 기준으로 설정해 시연 화면 기준을 통일했다.

## 2026-06-08 16일차
- 프로젝트의 기능 요구사항과 판단 기준을 문서화하기 위해 `docs/feature_spec.md`를 작성했다.  
- 문제 정의, 프로젝트 목표, 사용자 시나리오, 기능 요구사항, 난이도 데이터, 입력값 검증 기준, 클리어 가능성 판단 기준, 추천 난이도 산정 기준, 예외 처리, 테스트 케이스를 정리했다.  
- 기능 명세서의 내용이 실제 코드와 일치하는지 확인했고, README에서 기능 명세서 문서로 연결할 수 있도록 링크를 추가했다.

## 2026-06-09, 06-10, 06-11 17, 18, 19일차
- 시연 영상 제작 및 전체적인 코드, 주석 점검
