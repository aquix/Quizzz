export default class QuizzCtrl {
    /** @ngInject */
    constructor(api, $stateParams) {
        this.id = $stateParams.id;
        this.quizz = {};

        this.currentQuestion = {};
        this.currentQuestionId = 0;

        api.getQuizz(this.id)
            .then(res => {
                this.quizz = res.data;
                this.nextQuestion();

            })
            .catch(() => console.log('error'));
    }

    nextQuestion() {
        this.currentQuestionId++;
        this.currentQuestion = this.quizz.questions[this.currentQuestionId - 1];
    }
}