using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DifficultyUIManager : MonoBehaviour
{
    [Header("Text UI")]
    public TMP_Text resultText;
    public TMP_Text statusText;
    public TMP_Text recommendText;

    [Header("Input UI")]
    public TMP_InputField powerInputField;

    [Header("Difficulty Buttons")]
    public Button normalButton;
    public Button hardButton;
    public Button veryHardButton;
    public Button hellButton;
    public Button nightmareButton;
    public Button kiholeButton;

    private List<DifficultyData> difficultyList;

    private Color defaultButtonColor = Color.white;
    private Color selectedButtonColor = new Color(0.8f, 0.9f, 1f);

    void Start()
    {
        // 난이도별 기준 데이터를 초기화합니다.
        difficultyList = new List<DifficultyData>();

        difficultyList.Add(new DifficultyData("보통", 30000, "기본 보상", 5, 50));
        difficultyList.Add(new DifficultyData("어려움", 45000, "중급 보상", 7, 75));
        difficultyList.Add(new DifficultyData("매우 어려움", 55000, "상급 보상", 9, 95));
        difficultyList.Add(new DifficultyData("지옥", 60000, "고급 보상", 10, 110));
        difficultyList.Add(new DifficultyData("악몽", 68000, "희귀 재료 보상", 13, 145));
        difficultyList.Add(new DifficultyData("키홀", 75000, "최상급 보상", 16, 180));

        resultText.text = "선택한 난이도 정보가 표시됩니다.";
        statusText.text = "전투력을 입력하고 난이도를 선택하면 판단 결과가 표시됩니다.";
        recommendText.text = "추천 난이도: 전투력을 입력하면 표시됩니다.";

        ResetButtonColors();

        UnityEngine.Debug.Log("[Initialize] Difficulty data loaded");
    }

    public void SelectNormal()
    {
        HighlightButton(normalButton);
        DisplayDifficulty(difficultyList[0]);
    }

    public void SelectHard()
    {
        HighlightButton(hardButton);
        DisplayDifficulty(difficultyList[1]);
    }

    public void SelectVeryHard()
    {
        HighlightButton(veryHardButton);
        DisplayDifficulty(difficultyList[2]);
    }

    public void SelectHell()
    {
        HighlightButton(hellButton);
        DisplayDifficulty(difficultyList[3]);
    }

    public void SelectNightmare()
    {
        HighlightButton(nightmareButton);
        DisplayDifficulty(difficultyList[4]);
    }

    public void SelectKihole()
    {
        HighlightButton(kiholeButton);
        DisplayDifficulty(difficultyList[5]);
    }

    private void DisplayDifficulty(DifficultyData data) // 선택한 난이도의 상세 정보를 UI에 출력합니다.
    {
        resultText.text =
            $"난이도: {data.difficultyName}\n" +
            $"권장 전투력: {data.requiredPower:N0}\n" +
            $"예상 보상: {data.rewardText}\n" +
            $"예상 클리어 시간: {data.expectedClearTime}분\n" +
            $"보상 효율: {data.GetEfficiencyScore():F1}점\n" +
            $"효율 기준: {data.GetEfficiencyDescription()}";

        CheckPower(data);
        RecommendDifficulty();

        UnityEngine.Debug.Log($"[SelectDifficulty] {data.difficultyName} selected");
    }

    private bool TryGetUserPower(out int userPower)  // 입력된 전투력을 검증하고 숫자로 변환
    {
        string inputText = powerInputField.text;

        if (string.IsNullOrEmpty(inputText))
        {
            userPower = 0;
            statusText.text = "내 전투력을 입력해주세요.";
            recommendText.text = "추천 난이도: 전투력을 입력하면 표시됩니다.";
            UnityEngine.Debug.LogWarning("[InputError] User power input is empty");
            return false;
        }

        if (!int.TryParse(inputText, out userPower))
        {
            statusText.text = "전투력은 숫자만 입력해주세요.";
            recommendText.text = "추천 난이도: 숫자 입력 후 표시됩니다.";
            UnityEngine.Debug.LogWarning($"[InputError] Invalid power input: {inputText}");
            return false;
        }

        return true;
    }

    private void CheckPower(DifficultyData data) // 선택한 난이도와 유저 전투력을 비교해 클리어 가능성을 판단
    {
        if (!TryGetUserPower(out int userPower))
        {
            return;
        }

        int powerGap = userPower - data.requiredPower;

        if (powerGap >= 5000)
        {
            statusText.text =
                $"내 전투력: {userPower:N0}\n" +
                "판단 결과: 안정적으로 클리어 가능한 난이도입니다.";
        }
        else if (powerGap >= 0)
        {
            statusText.text =
                $"내 전투력: {userPower:N0}\n" +
                "판단 결과: 적정 난이도입니다.";
        }
        else if (powerGap >= -5000)
        {
            statusText.text =
                $"내 전투력: {userPower:N0}\n" +
                "판단 결과: 도전 가능하지만 파티 플레이를 권장합니다.";
        }
        else
        {
            statusText.text =
                $"내 전투력: {userPower:N0}\n" +
                "판단 결과: 전투력이 부족합니다.";
        }

        UnityEngine.Debug.Log(
            $"[PowerCheck] UserPower: {userPower}, RequiredPower: {data.requiredPower}, Gap: {powerGap}"
        );
    }

    private void RecommendDifficulty() // 클리어 가능한 난이도 중 보상 효율이 가장 높은 난이도를 추천
    {
        if (!TryGetUserPower(out int userPower))
        {
            return;
        }

        DifficultyData bestDifficulty = null;
        DifficultyData nextDifficulty = null;
        float bestEfficiency = -1f;

        foreach (DifficultyData difficulty in difficultyList)
        {
            if (userPower >= difficulty.requiredPower)
            {
                float efficiency = difficulty.GetEfficiencyScore();

                if (efficiency > bestEfficiency)
                {
                    bestEfficiency = efficiency;
                    bestDifficulty = difficulty;
                }
            }
            else
            {
                if (nextDifficulty == null || difficulty.requiredPower < nextDifficulty.requiredPower)
                {
                    nextDifficulty = difficulty;
                }
            }
        }

        if (bestDifficulty == null)
        {
            DifficultyData lowestDifficulty = difficultyList[0];
            int needPower = lowestDifficulty.requiredPower - userPower;

            recommendText.text =
                "추천 난이도: 없음\n\n" +
                "추천 사유:\n" +
                $"- 현재 전투력 {userPower:N0}은 가장 낮은 난이도인 {lowestDifficulty.difficultyName} 권장 전투력 {lowestDifficulty.requiredPower:N0}보다 낮습니다.\n" +
                $"- {lowestDifficulty.difficultyName} 난이도에 도전하려면 전투력이 {needPower:N0} 더 필요합니다.\n" +
                "- 전투력을 올린 뒤 가장 낮은 난이도부터 도전하는 것을 권장합니다.";

            UnityEngine.Debug.Log("[Recommend] No available difficulty");
            return;
        }

        string nextDifficultyMessage = "";

        if (nextDifficulty != null)
        {
            int gapToNext = nextDifficulty.requiredPower - userPower;

            nextDifficultyMessage =
                $"\n- 다음 난이도 {nextDifficulty.difficultyName}은 권장 전투력 {nextDifficulty.requiredPower:N0}으로 현재보다 {gapToNext:N0} 높습니다.";
        }
        else
        {
            nextDifficultyMessage =
                "\n- 현재 전투력으로 모든 난이도의 권장 전투력을 충족합니다.";
        }

        recommendText.text =
            $"추천 난이도: {bestDifficulty.difficultyName}\n\n" +
            "추천 사유:\n" +
            $"- 현재 전투력 {userPower:N0} 기준 {bestDifficulty.difficultyName} 권장 전투력 {bestDifficulty.requiredPower:N0}을 충족합니다.\n" +
            $"- {bestDifficulty.GetEfficiencyDescription()} 기준 보상 효율 {bestEfficiency:F1}점입니다.\n" +
            "- 클리어 가능한 난이도 중 보상 효율이 가장 높습니다." +
            nextDifficultyMessage;

        UnityEngine.Debug.Log(
            $"[Recommend] Recommended: {bestDifficulty.difficultyName}, Efficiency: {bestEfficiency:F1}"
        );
    }

    private void ResetButtonColors() // 모든 난이도 버튼을 기본 색상으로 되돌립니다.
    {
        normalButton.image.color = defaultButtonColor;
        hardButton.image.color = defaultButtonColor;
        veryHardButton.image.color = defaultButtonColor;
        hellButton.image.color = defaultButtonColor;
        nightmareButton.image.color = defaultButtonColor;
        kiholeButton.image.color = defaultButtonColor;
    }

    private void HighlightButton(Button selectedButton) // 선택한 난이도 버튼만 강조합니다.
    {
        ResetButtonColors();
        selectedButton.image.color = selectedButtonColor;
    }

    public void ResetUI()
    {
        powerInputField.text = "";

        resultText.text = "선택한 난이도 정보가 표시됩니다.";
        statusText.text = "전투력을 입력하고 난이도를 선택하면 판단 결과가 표시됩니다.";
        recommendText.text = "추천 난이도: 전투력을 입력하면 표시됩니다.";

        ResetButtonColors();

        UnityEngine.Debug.Log("[Reset] UI reset completed");
    }


}
