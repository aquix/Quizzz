export default class QuizResultsCtrl {
    constructor($stateParams) {
        this.success = true;
        this.allQuestionsCount = 10;
        this.passedQuestionsCount = 10;
        this.percent = 100;
    }
}