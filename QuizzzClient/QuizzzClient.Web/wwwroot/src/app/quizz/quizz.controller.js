export default class QuizzCtrl {
    /** @ngInject */
    constructor(api, $stateParams, $state) {
        this.api = api;
        this.$state = $state;

        this.id = $stateParams.id;
        this.quizz = {};
        this.results = [];

        this.currentQuestion = {};
        this.currentQuestionId = 0;

        this.api.getQuizz(this.id)
            .then(res => {
                this.quizz = res.data;
                this.quizz.questions = this.quizz.questions.map(q => {
                    q.answers = q.answers.map(a => {
                        return {
                            answerBody: a,
                            isChecked: false
                        };
                    });
                    return q;
                })
                this.nextQuestion();

            })
            .catch(() => console.log('error'));
    }

    nextQuestion() {
        if (this.currentQuestionId != 0) {
            this._saveQuestionAnswers();
        }

        this.currentQuestionId++;

        if (this.currentQuestionId <= this.quizz.questions.length) {
            this.currentQuestion = this.quizz.questions[this.currentQuestionId - 1];
        } else {
            let sendData = {
                id: this.quizz.id,
                results: this.results
            };

            this.api.acceptQuizz(sendData)
                .then(res => {
                    this.$state.go('quizzResults');
                })
        }
    }

    _saveQuestionAnswers() {
        this.results[this.currentQuestion.id - 1] = [];
        let questionResults = this.results[this.currentQuestion.id - 1];

        this.currentQuestion.answers.forEach((answer, i) => {
            if (answer.isChecked) {
                questionResults.push(i);
            }
        });
    }
}