namespace Models
{
    public struct TestResult
    {
        public TestResult(int totalQuestions, int rightAnswers)
        {
            this.totalQuestions = totalQuestions;
            this.rightAnswers = rightAnswers;
        }

        public int totalQuestions;
        public int rightAnswers;
    }
}