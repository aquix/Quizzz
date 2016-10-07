export default class QuizResultsCtrl {
    constructor($stateParams) {
        this.success = $stateParams.result.success;
        this.allQuestionsCount = $stateParams.result.allQuestionsCount;
        this.passedQuestionsCount = $stateParams.result.passedQuestionsCount;
        this.percent = this.passedQuestionsCount * 100 / this.allQuestionsCount;
    }
}