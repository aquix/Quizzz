export default class QuizCtrl {
    /** @ngInject */
    constructor(api, $stateParams, $state) {
        this.api = api;
        this.$state = $state;

        this.id = $stateParams.id;
        this.quiz = {};
        this.results = [];

        this.currentQuestion = {};
        this.currentQuestionId = 0;

        this.api.getQuiz(this.id)
            .then(res => {
                this.quiz = res.data;
                this.quiz.questions = this.quiz.questions.map(q => {
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

        if (this.currentQuestionId <= this.quiz.questions.length) {
            this.currentQuestion = this.quiz.questions[this.currentQuestionId - 1];
        } else {
            let sendData = {
                QuizId: this.quiz.id,
                Answers: this.results
            };

            this.api.acceptQuiz(sendData)
                .then(res => {
                    console.log(res.data);
                    this.$state.go('quizResults', {
                        result: res.data
                    });
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