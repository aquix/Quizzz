export default class QuizzResultsCtrl {
    constructor($stateParams) {
        this.success = true;
        this.allQuestionsCount = 10;
        this.passedQuestionsCount = 10;
        this.percent = 100;
    }
}