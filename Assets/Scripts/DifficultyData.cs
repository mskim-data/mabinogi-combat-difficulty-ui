public class DifficultyData
{
    public string difficultyName;
    public int requiredPower;
    public string rewardText;
    public int expectedClearTime;
    public int rewardScore;

    public DifficultyData(
        string name,
        int power,
        string reward,
        int clearTime,
        int score
    )
    {
        difficultyName = name;
        requiredPower = power;
        rewardText = reward;
        expectedClearTime = clearTime;
        rewardScore = score;
    }

    // 보상 점수와 예상 클리어 시간을 기준으로 보상 효율을 계산합니다.
    public float GetEfficiencyScore()
    {
        return (float)rewardScore / expectedClearTime;
    }

    // 보상 효율 계산 기준을 UI에 표시하기 위한 설명 문구를 반환합니다.
    public string GetEfficiencyDescription()
    {
        return $"보상 점수 {rewardScore} / 예상 클리어 시간 {expectedClearTime}분";
    }
}
