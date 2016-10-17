import moment from '../../../lib/moment/moment';

export default class QuizCtrl {
    /** @ngInject */
    constructor(api, $stateParams, route, storageService, $interval) {
        this.api = api;
        this._route = route;
        this._storageService = storageService;
        this._$interval = $interval;

        this.id = $stateParams.id;
        this.quiz = {};
        this.results = [];
        this.currentQuestion = {};
        this.currentQuestionId = 0;
        this.leftoverTime = moment.duration(0);

        // Add source for saving quiz state
        this._initStorage();

        let isNewQuiz = (!storageService.settingsSaved || storageService.savedData.id !== this.id);
        if (!isNewQuiz) {
            this._restoreData(storageService.savedData);
        }

        this._startQuiz(isNewQuiz);
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

            this.api.acceptQuiz(sendData).then(res => {
                this._storageService.removeAllSources();
                this._route.go('quizResults', 'Results', {
                    result: res.data
                });
            })
        }
    }

    getLeftoverTime() {
        let formatted = ("0" + Math.floor(this.leftoverTime.asHours())).slice(-2) +
            moment.utc(this.leftoverTime.asMilliseconds()).format(":mm:ss")
        return formatted;
    }

    _startQuiz(isNewQuiz) {
        this.api.getQuiz(this.id).then(res => {
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

            if (isNewQuiz) {
                this.leftoverTime = moment.duration(res.data.time, 's');
                this.nextQuestion();
            }

            // Start countdown
            let countdown = this._$interval(() => {
                this.leftoverTime.subtract(1, 's');
                if (this.leftoverTime <= 0) {
                    this._$interval.cancel(countdown);
                    this._timeOver();
                }
            }, 1000)

            // Change toolbar heading
            this._route.currentPageTitle = this.quiz.name;
        })
        .catch(() => console.log('error'));
    }

    _restoreData(savedData) {
        this.id = savedData.id;
        this.results = savedData.results;
        this.currentQuestion = savedData.currentQuestion;
        this.currentQuestionId = savedData.currentQuestionId;
        this.leftoverTime = moment.duration(savedData.leftoverTime, 's');
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

    _initStorage() {
        this._storageService.removeAllSources();
        this._storageService.addSource(() => {
            return {
                id: this.id,
                results: this.results,
                currentQuestionId: this.currentQuestionId,
                currentQuestion: this.currentQuestion,
                leftoverTime: this.leftoverTime.asSeconds()
            }
        });
    }

    _timeOver() {
        this._route.go('timeover', 'Time is over', {
            id: this.quiz.id
        });
        console.log('time over');
    }
}